/**************************************************************************
 *************************** Include Files ********************************
 **************************************************************************/

/* Standard Include Files. */
#include <stdint.h>
#include <stdlib.h>
#include <stddef.h>
#include <string.h>
#include <stdio.h>
#include <math.h>
/* mmWave SDK Include Files: */
#include <ti/common/sys_common.h>
#include <ti/common/mmwave_sdk_version.h>
#include <ti/drivers/uart/UART.h>
#include <ti/control/mmwavelink/mmwavelink.h>
#include <ti/utils/cli/cli.h>
#include <ti/utils/mathutils/mathutils.h>

/* Demo Include Files */
#include "mss_mmw.h"
#include "mmw_messages.h"

#include "user_parameters.h"
#include "configuration.h"
#include "mss_functions.h"
#include "communication1843.h"
#include "sleep.h"


/**************************************************************************
 *************************** Local Definitions ****************************
 **************************************************************************/
static int32_t MMWaveFlushCfg ();

static int32_t set_MMWaveDataOutputMode();
static int32_t set_MMWaveChannelCfg();
static int32_t set_MMWaveADCCfg();
static int32_t set_ADCBufCfg();
static int32_t set_MMWaveProfileCfg ();
static int32_t set_MMWaveChirpCfg();
static int32_t set_MMWaveFrameCfg();
static int32_t set_MMWaveLowPowerCfg();

static int32_t set_CLIvitalSignsParamsCfg (int32_t argc, char* argv[]);
static int32_t set_CLIGuiMonSel (int32_t argc, char* argv[]);



static int32_t CLI_help_mod (int32_t argc, char* argv[]);
static int32_t CLI_open_mod (CLI_Cfg* ptrCLICfg);
static void MmwDemo_mssCfgUpdate(void *srcPtr, uint32_t offset, uint32_t size, int8_t subFrameNum);

extern MmwDemo_MCB    gMmwMssMCB;

extern int32_t CLI_MMWaveExtensionInit(CLI_Cfg* ptrCLICfg);

/**
 * @brief
 *  CLI Master control block
 *
 * @details
 *  This is the MCB which tracks the CLI module
 */
typedef struct CLI_MCB_t
{
    /**
     * @brief   Configuration which was used to configure the CLI module
     */
    CLI_Cfg         cfg;

    /**
     * @brief   This is the number of CLI commands which have been added to the module
     */
    uint32_t        numCLICommands;

    /**
     * @brief   CLI Task Handle:
     */
    //Task_Handle     cliTaskHandle;
}MmwDemo_CliMCB;



MmwDemo_CliMCB      gCLI;


extern int32_t startProcedure();
extern int32_t stopProcedure();


void MmwDemo_CLIInit (void)
{
    CLI_Cfg     cliCfg;
    char        demoBanner[256];
    uint32_t    cnt;

    /* Create Demo Banner to be printed out by CLI */
    sprintf(&demoBanner[0],
                       "******************************************\n" \
                       "xWR18xx MMW Demo %02d.%02d.%02d.%02d\n"  \
                       "******************************************\n",
                        MMWAVE_SDK_VERSION_MAJOR,
                        MMWAVE_SDK_VERSION_MINOR,
                        MMWAVE_SDK_VERSION_BUGFIX,
                        MMWAVE_SDK_VERSION_BUILD
            );

    /* Initialize the CLI configuration: */
    memset ((void *)&cliCfg, 0, sizeof(CLI_Cfg));

    /* Populate the CLI configuration: */
    cliCfg.cliPrompt                    = "mmwDemo:/>";
    cliCfg.cliBanner                    = demoBanner;
    cliCfg.cliUartHandle                = gMmwMssMCB.commandUartHandle;
    cliCfg.taskPriority                 = 3;
    cliCfg.enableMMWaveExtension        = 1U;
    cliCfg.usePolledMode                = true;
    cnt=0;

    cliCfg.tableEntry[cnt].cmd            = "sensorStart";
    cliCfg.tableEntry[cnt].helpString     = "[doReconfig(optional, default:enabled)]";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = startProcedure;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "sensorStop";
    cliCfg.tableEntry[cnt].helpString     = "No arguments";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = stopProcedure;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "flushCfg";
    cliCfg.tableEntry[cnt].helpString     = "No arguments";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = MMWaveFlushCfg;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "dfeDataOutputMode";
    cliCfg.tableEntry[cnt].helpString     = "<Mode>";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_MMWaveDataOutputMode;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "channelCfg";
    cliCfg.tableEntry[cnt].helpString     = "";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_MMWaveChannelCfg;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "adcCfg";
    cliCfg.tableEntry[cnt].helpString     = "";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_MMWaveADCCfg;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "adcbufCfg";
    cliCfg.tableEntry[cnt].helpString     = "<subFrameIdx> <adcOutputFmt> <SampleSwap> <ChanInterleave> <ChirpThreshold>";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_ADCBufCfg;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "profileCfg";
    cliCfg.tableEntry[cnt].helpString     = "";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_MMWaveProfileCfg;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "chirpCfg";
    cliCfg.tableEntry[cnt].helpString     = "";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_MMWaveChirpCfg;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "frameCfg";
    cliCfg.tableEntry[cnt].helpString     = "";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_MMWaveFrameCfg;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "lowPower";
    cliCfg.tableEntry[cnt].helpString     = "";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_MMWaveLowPowerCfg;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "vitalSignsCfg";
    cliCfg.tableEntry[cnt].helpString     = "<subFrameIdx> <enabled> <negativeBinIdx> <positiveBinIdx> <numAvgFrames>";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_CLIvitalSignsParamsCfg;
    cnt++;

    cliCfg.tableEntry[cnt].cmd            = "guiMonSel";
    cliCfg.tableEntry[cnt].helpString     = "<sysInfo> <vitalSignsStats> <rangeProfile> <adcData>";
    cliCfg.tableEntry[cnt].cmdHandlerFxn  = set_CLIGuiMonSel;
    cnt++;
    /* Open the CLI: */
    if (CLI_open_mod (&cliCfg) < 0)
    {
        printf ("Error: Unable to open the CLI\n");
        return;
    }
    printf ("Debug: CLI is operational\n");
    return;
}


int32_t CLI_task_mod(void)
{
    int32_t BytesRcvd;
    uint32_t i;

    char*                   tokenizedArgs[CLI_MAX_ARGS];
    char*                   ptrCLICommand;
    char                    delimiter[] = " \r\n";
    uint32_t                argIndex;
    CLI_CmdTableEntry*      ptrCLICommandEntry;
    int32_t                 cliStatus;
    uint32_t                index;

    
    if (gMmwMssMCB.isInStop == false){

        BytesRcvd = UART_read(gMmwMssMCB.commandUartHandle, &gMmwMssMCB.commandUartDataBuffer[0], N_BYTES_CONFIG_COMMAND);
        if (BytesRcvd >0){

            UART_write(gMmwMssMCB.commandUartHandle, &gMmwMssMCB.commandUartDataBuffer[0], BytesRcvd);

            printf("MSS has received something from the uart: stopping the sensor.\n");
            gMmwMssMCB.stopRequired = true;
            return(STOP_CMD);
        }

    }else{

        BytesRcvd = UART_read(gMmwMssMCB.commandUartHandle, &gMmwMssMCB.commandUartDataBuffer[0], N_BYTES_CONFIG_COMMAND);
        if (BytesRcvd>0){

            UART_write(gMmwMssMCB.commandUartHandle, &gMmwMssMCB.commandUartDataBuffer[0], BytesRcvd);

            printf("MSS has received a command\n");

            for(i=0;i<1;i++)
                sleep_ms(500);

            gMmwMssMCB.realTimeError = false;

            memset ((void *)&tokenizedArgs, 0, sizeof(tokenizedArgs));
            argIndex      = 0;
            ptrCLICommand = (char*)&gMmwMssMCB.commandUartDataBuffer[0];

            // comment lines found - ignore the whole line
            if (gMmwMssMCB.commandUartDataBuffer[0]=='%') {
                printf("uart command skipped\n");
                return(-1);
            }

            // The command has been entered we now tokenize the command message
            while (1)
            {
                // Tokenize the arguments:
                tokenizedArgs[argIndex] = strtok(ptrCLICommand, delimiter);
                if (tokenizedArgs[argIndex] == NULL)
                    break;

                // Increment the argument index:
                argIndex++;
                if (argIndex >= CLI_MAX_ARGS)
                    break;

                //Reset the command string
                ptrCLICommand = NULL;
            }

            // Were we able to tokenize the CLI command?
            if (argIndex == 0){
                CLI_write ("command from uart not found\n");
                return(-1);
            }

            // Cycle through all the registered CLI commands:
            for (index = 0; index < gCLI.numCLICommands; index++)
            {
                ptrCLICommandEntry = &gCLI.cfg.tableEntry[index];

                // Do we have a match?
                if (strcmp(ptrCLICommandEntry->cmd, tokenizedArgs[0]) == 0)
                {
                    // YES: Pass this to the CLI registered function
                    cliStatus = ptrCLICommandEntry->cmdHandlerFxn (argIndex, tokenizedArgs);
                    //cliStatus = 0;
                    if (cliStatus == 0)
                    {
                        printf("%s done\n", tokenizedArgs[0]);
                        CLI_write ("Done\n");
                    }
                    else
                    {
                        //CLI_write ("Error %d\n", cliStatus);
                        printf("%s undone\n", tokenizedArgs[0]);
                    }
                    break;

                };
            }


            return(-1);

        }
    }

    return(-1);

}


static int32_t CLI_open_mod (CLI_Cfg* ptrCLICfg)
{
    uint32_t        index;

    /* Sanity Check: Validate the arguments */
    if (ptrCLICfg == NULL)
        return -1;

    /* Initialize the CLI MCB: */
    memset ((void*)&gCLI, 0, sizeof(MmwDemo_CliMCB));

    /* Copy over the configuration: */
    memcpy ((void *)&gCLI.cfg, (void *)ptrCLICfg, sizeof(CLI_Cfg));

    /* Cycle through and determine the number of supported CLI commands: */
    for (index = 0; index < CLI_MAX_CMD; index++)
    {
        /* Do we have a valid entry? */
        if (gCLI.cfg.tableEntry[index].cmd == NULL)
        {
            /* NO: This is the last entry */
            break;
        }
        else
        {
            /* YES: Increment the number of CLI commands */
            gCLI.numCLICommands = gCLI.numCLICommands + 1;
        }
    }

    /* Do we have a CLI Prompt specified?  */
    if (gCLI.cfg.cliPrompt == NULL)
        gCLI.cfg.cliPrompt = "CLI:/>";

    /* The CLI provides a help command by default:
     * - Since we are adding this at the end of the table; a user of this module can also
     *   override this to provide its own implementation. */
    gCLI.cfg.tableEntry[gCLI.numCLICommands].cmd           = "help";
    gCLI.cfg.tableEntry[gCLI.numCLICommands].helpString    = NULL;
    gCLI.cfg.tableEntry[gCLI.numCLICommands].cmdHandlerFxn = CLI_help_mod;

    /* Increment the number of CLI commands: */
    gCLI.numCLICommands++;

    /* Do we have a banner to be displayed? */
    if (gCLI.cfg.cliBanner != NULL)
    {
        /* YES: Display the banner */
        CLI_write (gCLI.cfg.cliBanner);
    }

    /* Demo Prompt: */
    CLI_write (gCLI.cfg.cliPrompt);

    /* Reset the command string: */
    memset ((void *)&gMmwMssMCB.commandUartDataBuffer[0], 0, sizeof(gMmwMssMCB.commandUartDataBuffer));

    return 0;
}



MmwDemo_message      message;               // mbox message
MMWave_CtrlCfg       gCLIMMWaveControlCfg;   // Global MMWave configuration tracked by the module.
MMWave_OpenCfg       gCLIMMWaveOpenCfg;      // Global MMWave open configuration tracked by the module.

//float compensation_table[2*SYS_COMMON_NUM_TX_ANTENNAS*SYS_COMMON_NUM_RX_CHANNEL]={0.8732333, 0.0000000, 0.8928758, -0.1218727, 0.7809589, -0.4987252, 0.4641488, -0.7917042, -0.8693785, 0.3468235, -0.6569176, 0.6718205, -0.2312504, 0.5869673, -0.1934010, 0.7165093, -0.1827554, -0.7168737, -0.1623303, -0.9867365, -0.5116334, -0.6556087, -0.6472150, -0.2717953};
float compensation_table[2*SYS_COMMON_NUM_TX_ANTENNAS*SYS_COMMON_NUM_RX_CHANNEL]={0.59140, 0.00000, -0.47000, 0.88267, -0.45253, -0.17615, 0.04698, -0.44567, -0.40144, -0.27533, 0.17009, -0.40812, 0.36589, 0.13266, 0.09981, 0.36347, 0.45514, -0.01677, 0.18763, 0.33529, -0.70364, 0.33829, -0.18240, -0.38331};

/**************************************************************************
 ********************** CLI mmWave Extension Functions ********************
 **************************************************************************/
static int32_t CLI_help_mod (int32_t argc, char* argv[])
{
    uint32_t    index;

    /* Display the banner: */
    CLI_write ("Help: This will display the usage of the CLI commands\n");
    CLI_write ("Command: Help Description\n");

    /* Cycle through all the registered CLI commands: */
    for (index = 0; index < gCLI.numCLICommands; index++)
    {
        /* Display the help string*/
        CLI_write ("%s: %s\n",
                    gCLI.cfg.tableEntry[index].cmd,
                   (gCLI.cfg.tableEntry[index].helpString == NULL) ?
                    "No help available" :
                    gCLI.cfg.tableEntry[index].helpString);
    }

    /* Is the mmWave Extension enabled? */
#if 0
    if (gCLI.cfg.enableMMWaveExtension == 1U)
    {
        /* YES: Pass the control to the extension help handler. */
        CLI_MMWaveExtensionHelp ();
    }
#endif
    return 0;
}

static int32_t MMWaveFlushCfg (void)
{
    /* Reset the global configuration: */
    memset ((void*)&gCLIMMWaveControlCfg, 0, sizeof(MMWave_CtrlCfg));

    /* Reset the open configuration: */
    memset ((void*)&gCLIMMWaveOpenCfg, 0, sizeof(MMWave_OpenCfg));
    return 0;
}


static int32_t set_MMWaveDataOutputMode ()
{

    uint32_t cfgMode;

    cfgMode = DFE_DATA_OUTPUT_MODE;
    switch (cfgMode)
    {
        case 1U:
        {
            gCLIMMWaveControlCfg.dfeDataOutputMode = MMWave_DFEDataOutputMode_FRAME;
            break;
        }
        case 2U:
        {
            gCLIMMWaveControlCfg.dfeDataOutputMode = MMWave_DFEDataOutputMode_CONTINUOUS;
            break;
        }
        case 3U:
        {
            gCLIMMWaveControlCfg.dfeDataOutputMode = MMWave_DFEDataOutputMode_ADVANCED_FRAME;
            break;
        }
        default:
        {
            /* Error: Invalid argument. */
            printf ("Error: Invalid Data Output mode.\n");
            return -1;
        }
    }

    return 0;
}


static int32_t set_MMWaveChannelCfg ()
{
    rlChanCfg_t     chCfg;

    /* Initialize the channel configuration: */
    memset ((void *)&chCfg, 0, sizeof(rlChanCfg_t));

    /* Populate the channel configuration: */
    chCfg.rxChannelEn = RX_ANTENNA_MASK;
    chCfg.txChannelEn = TX_ANTENNA_MASK;
    chCfg.cascading   = 0; // n/a

    /* Save Configuration to use later */
    memcpy((void *)&gCLIMMWaveOpenCfg.chCfg, (void *)&chCfg, sizeof(rlChanCfg_t));
    return 0;
}


static int32_t set_MMWaveADCCfg()
{
    rlAdcOutCfg_t   adcOutCfg;
    int32_t         retVal = 0;

    /* Initialize the ADC Output configuration: */
    memset ((void *)&adcOutCfg, 0, sizeof(rlAdcOutCfg_t));

    /* Populate the ADC Output configuration: */
    adcOutCfg.fmt.b2AdcBits   = NUM_ADC_BITS;
    adcOutCfg.fmt.b2AdcOutFmt = ADC_OUT_FORMAT;

    /* Save Configuration to use later */
    memcpy((void *)&gCLIMMWaveOpenCfg.adcOutCfg, (void *)&adcOutCfg, sizeof(rlAdcOutCfg_t));
    return retVal;
}


static int32_t set_ADCBufCfg ()
{
    MmwDemo_ADCBufCfg   adcBufCfg;
    int8_t              subFrameNum = -1;

    memset((void *)&message, 0, sizeof(MmwDemo_message));

    /* Initialize the ADC Output configuration: */
    memset ((void *)&adcBufCfg, 0, sizeof(MmwDemo_ADCBufCfg));

    /* Populate configuration: */
    adcBufCfg.adcFmt          = ADCBUF_OUT_FORMAT;
    adcBufCfg.iqSwapSel       = ADCBUF_SAMPLE_SWAP;
    adcBufCfg.chInterleave    = ADCBUF_CH_INTERLEAVE;
    adcBufCfg.chirpThreshold  = ADCBUF_CHIRP_THRESH;

    /* Save Configuration to use later */
    MmwDemo_mssCfgUpdate((void *)&adcBufCfg, offsetof(MmwDemo_CliCfg_t, adcBufCfg),
        sizeof(MmwDemo_ADCBufCfg), subFrameNum);


    /* Send configuration to DSS */
    memset((void *)&message, 0, sizeof(MmwDemo_message));

    message.type = MMWDEMO_MSS2DSS_ADCBUFCFG;
    message.subFrameNum = subFrameNum;
    memcpy((void *)&message.body.adcBufCfg, (void *)&adcBufCfg, sizeof(MmwDemo_ADCBufCfg));

    if (MmwDemo_mboxWrite(&message) == 0)
        return 0;
    else
        return -1;
}


static int32_t set_MMWaveProfileCfg (int32_t argc, char* argv[])
{
    rlProfileCfg_t          profileCfg;
    memset((void *)&message, 0, sizeof(MmwDemo_message));

    /* Profile configuration is valid only for the Frame or Advanced Frame Mode: */
    if ((gCLIMMWaveControlCfg.dfeDataOutputMode != MMWave_DFEDataOutputMode_FRAME) &&
        (gCLIMMWaveControlCfg.dfeDataOutputMode != MMWave_DFEDataOutputMode_ADVANCED_FRAME))
    {
        printf ("Error: Configuration is valid only if the DFE Output Mode is Chirp\n");
        return -1;
    }

    /* Initialize the profile configuration: */
    memset ((void *)&profileCfg, 0, sizeof(rlProfileCfg_t));

    /* Populate the profile configuration: */
    if (argc == 0) {

        printf ("Using default values for profileCfg\n");

        profileCfg.profileId             = PROFILE_ID;
        profileCfg.startFreqConst        = (uint32_t)(START_FREQ_GHZ * (1U << 26) / 3.6);
        profileCfg.idleTimeConst         = (uint32_t)(IDLE_TIME_US * 1000 / 10);
        profileCfg.adcStartTimeConst     = (uint32_t)(ADC_START_TIME_US * 1000 / 10);
        profileCfg.rampEndTime           = (uint32_t)(RAMP_END_TIME_US * 1000 / 10);

        profileCfg.txOutPowerBackoffCode = (TX_OUT_POWER_BKOFF + (TX_OUT_POWER_BKOFF<<8) + (TX_OUT_POWER_BKOFF<<16));
        profileCfg.txPhaseShifter        = TX_PHASE_SHIFTER;
        profileCfg.freqSlopeConst        = (int16_t)(FREQ_SLOPE_MHZ_US * (1U << 26) / (3.6*1e3*900));
        profileCfg.txStartTime           = (int16_t)(TX_START_TIME_US * 1000 / 10);
        profileCfg.numAdcSamples         = (uint16_t)NUM_ADC_SAMPLES;
        profileCfg.digOutSampleRate      = (uint16_t)ADC_SAMP_FREQ;
        profileCfg.hpfCornerFreq1        =  HPF1_CORNER_FREQ;
        profileCfg.hpfCornerFreq2        =  HPF2_CORNER_FREQ;
        profileCfg.rxGain                =  (uint16_t)RX_GAIN;

    }else{

        /* Sanity Check: number of arguments check */
        if (argc != 15)
        {
            CLI_write ("Error: Invalid usage of the CLI command setProfileCfg\n");
            return -1;
        }

        /* Populate the profile configuration: */
        profileCfg.profileId             = atoi (argv[1]);

        /* Translate from GHz to [1 LSB = gCLI_mmwave_freq_scale_factor * 1e9 / 2^26 Hz] units
        * of mmwavelink format */
        profileCfg.startFreqConst        = (uint32_t) (atof(argv[2]) * (1U << 26) / 3.6f);

        /* Translate below times from us to [1 LSB = 10 ns] units of mmwavelink format */
        profileCfg.idleTimeConst         = (uint32_t)((float)atof(argv[3]) * 1000 / 10);
        profileCfg.adcStartTimeConst     = (uint32_t)((float)atof(argv[4]) * 1000 / 10);
        profileCfg.rampEndTime           = (uint32_t)((float)atof(argv[5]) * 1000 / 10);

        profileCfg.txOutPowerBackoffCode = (atoi (argv[6]) + (atoi (argv[6])<<8) + (atoi (argv[6])<<16));

        profileCfg.txPhaseShifter        = atoi (argv[7]);

        /* Translate from MHz/us to [1 LSB = (gCLI_mmwave_freq_scale_factor * 1e6 * 900) / 2^26 kHz/uS]
           * units of mmwavelink format */
        profileCfg.freqSlopeConst        = (int16_t)(atof(argv[8]) * (1U << 26) /
                                                       ((3.6f * 1e3) * 900.0));

        /* Translate from us to [1 LSB = 10 ns] units of mmwavelink format */
        profileCfg.txStartTime           = (int32_t)((float)atof(argv[9]) * 1000 / 10);

        profileCfg.numAdcSamples         = atoi (argv[10]);
        profileCfg.digOutSampleRate      = atoi (argv[11]);
        profileCfg.hpfCornerFreq1        = atoi (argv[12]);
        profileCfg.hpfCornerFreq2        = atoi (argv[13]);
        profileCfg.rxGain                = atoi (argv[14]);
    }

    /* Send configuration to DSS */
    memset((void *)&message, 0, sizeof(MmwDemo_message));

    message.type = MMWDEMO_MSS2DSS_PROFILE_CFG;
    memcpy((void *)&message.body.profileCfg, (void *)&profileCfg, sizeof(rlProfileCfg_t));

    if (MmwDemo_mboxWrite(&message) == 0)
        return 0;
    else
        return -1;
}

static int32_t set_MMWaveChirpCfg()
{
    rlChirpCfg_t            chirpCfg;
    memset((void *)&message, 0, sizeof(MmwDemo_message));

    /* Chirp configuration is valid only for the Frame or Advanced Frame Mode: */
    if ((gCLIMMWaveControlCfg.dfeDataOutputMode != MMWave_DFEDataOutputMode_FRAME) &&
        (gCLIMMWaveControlCfg.dfeDataOutputMode != MMWave_DFEDataOutputMode_ADVANCED_FRAME))
    {
        printf ("Error: Configuration is valid only if the DFE Output Mode is Chirp\n");
        return -1;
    }

    /* Initialize the chirp configuration: */
    memset ((void *)&chirpCfg, 0, sizeof(rlChirpCfg_t));


    /* Populate the chirp configuration: */
    chirpCfg.chirpStartIdx   = CHIRP_START_IDX;
    chirpCfg.chirpEndIdx     = CHIRP_END_IDX;
    chirpCfg.profileId       = CHIRP_PROFILE_ID;
    chirpCfg.startFreqVar    = (uint32_t)(START_FREQ_VAR * (1U << 26) / (3.6 * 1e3)); /* in MHz */
    chirpCfg.freqSlopeVar    = (uint16_t)(FREQ_SLOPE_VAR * (1U << 26) / (3.6*1e6*900)); /* in KHz/usec */
    chirpCfg.idleTimeVar     = (uint32_t)(IDLE_TIME_VAR_US * 1000 / 10);
    chirpCfg.adcStartTimeVar = (uint32_t)(ADC_START_TIME_VAR_US * 1000 / 10);
    chirpCfg.txEnable        = (1<<(TX_ENABLE-1));

    /* Send configuration to DSS */
    memset((void *)&message, 0, sizeof(MmwDemo_message));

    message.type = MMWDEMO_MSS2DSS_CHIRP_CFG;
    memcpy((void *)&message.body.chirpCfg, (void *)&chirpCfg, sizeof(rlChirpCfg_t));

    if (MmwDemo_mboxWrite(&message) == 0)
        return 0;
    else
        return -1;
}


static int32_t set_MMWaveFrameCfg (int32_t argc, char* argv[])
{
    rlFrameCfg_t    frameCfg;


    /* Frame configuration is valid only for the Frame or Advanced Frame Mode: */
    if (gCLIMMWaveControlCfg.dfeDataOutputMode != MMWave_DFEDataOutputMode_FRAME)
    {
        printf ("Error: Configuration is valid only if the DFE Output Mode is Chirp\n");
        return -1;
    }

    /* Initialize the frame configuration: */
    memset ((void *)&frameCfg, 0, sizeof(rlFrameCfg_t));

    if (argc == 0) {

        printf ("Using default values for profileCfg\n");

        /* Populate the frame configuration: */
        frameCfg.chirpStartIdx      = CHIRP_START_IDX;
        frameCfg.chirpEndIdx        = CHIRP_END_IDX;
        frameCfg.numLoops           = FRAME_NUM_LOOPS;
        frameCfg.numFrames          = NUMBER_OF_FRAMES;
        frameCfg.framePeriodicity   = (uint32_t)(FRAME_RATE_MS * 1000000 / 5);
        frameCfg.triggerSelect      = FRAME_TRIGGER_SELECT;
        frameCfg.frameTriggerDelay  = (uint32_t)(FRAME_TRIGGER_DELAY_MS * 1000000 / 5);

    }else{

        /* Populate the frame configuration: */
        frameCfg.chirpStartIdx      = atoi (argv[1]);
        frameCfg.chirpEndIdx        = atoi (argv[2]);
        frameCfg.numLoops           = atoi (argv[3]);
        frameCfg.numFrames          = atoi (argv[4]);
        frameCfg.framePeriodicity   = (uint32_t)((float)atof(argv[5]) * 1000000 / 5);
        frameCfg.triggerSelect      = atoi (argv[6]);
        frameCfg.frameTriggerDelay  = (uint32_t)((float)atof(argv[7]) * 1000000 / 5);
    }

    /* Save Configuration to use later */
    memcpy((void *)&gCLIMMWaveControlCfg.u.frameCfg.frameCfg, (void *)&frameCfg, sizeof(rlFrameCfg_t));
    return 0;
}


static int32_t set_MMWaveLowPowerCfg ()
{
    rlLowPowerModeCfg_t     lowPowerCfg;


    /* Initialize the channel configuration: */
    memset ((void *)&lowPowerCfg, 0, sizeof(rlLowPowerModeCfg_t));

    /* Populate the channel configuration: */
    lowPowerCfg.lpAdcMode     = ADC_POWER_MODE;

    /* Save Configuration to use later */
    memcpy((void *)&gCLIMMWaveOpenCfg.lowPowerMode, (void *)&lowPowerCfg, sizeof(rlLowPowerModeCfg_t));
    return 0;
}



int32_t CLI_MMWaveExtensionInit(CLI_Cfg* ptrCLICfg)
{
    /* Initialize the mmWave control configuration: */
    memset ((void *)&gCLIMMWaveControlCfg, 0, sizeof(MMWave_CtrlCfg));
    return 0;
}


void CLI_getMMWaveExtensionConfig(MMWave_CtrlCfg* ptrCtrlCfg)
{
    memcpy ((void*)ptrCtrlCfg, (void*)&gCLIMMWaveControlCfg, sizeof(MMWave_CtrlCfg));
    return;
}

/**
 *  @b Description
 *  @n
 *      This is an API provided by the CLI mmWave extension handler to get
 *      the mmWave control configuration.
 *
 *  @param[out]  ptrOpenCfg
 *      Pointer to the open configuration populated by the API
 *
 *  \ingroup CLI_UTIL_EXTERNAL_FUNCTION
 *
 *  @retval
 *      Not applicable
 */
void CLI_getMMWaveExtensionOpenConfig(MMWave_OpenCfg* ptrOpenCfg)
{
    memcpy ((void*)ptrOpenCfg, (void*)&gCLIMMWaveOpenCfg, sizeof(MMWave_OpenCfg));
    return;
}











static void MmwDemo_mssCfgUpdate(void *srcPtr, uint32_t offset, uint32_t size, int8_t subFrameNum)
{    
    /* if subFrameNum undefined, broadcast to all sub-frames */
    if(subFrameNum == MMWDEMO_SUBFRAME_NUM_FRAME_LEVEL_CONFIG)
    {
        uint8_t  indx;
        for(indx = 0; indx < RL_MAX_SUBFRAMES; indx++)
        {
            memcpy((void *)((uint32_t) &gMmwMssMCB.cliCfg[indx] + offset), srcPtr, size);
        }
        
    }
    else
    {
        /* Apply configuration to specific subframe (or to position zero for the legacy case
           where there is no advanced frame config) */
        memcpy((void *)((uint32_t) &gMmwMssMCB.cliCfg[subFrameNum] + offset), srcPtr, size);
    }
}

/*VITALSIGN FUNCTIONS*/
static int32_t set_CLIvitalSignsParamsCfg (int32_t argc, char* argv[])
{

    VitalSignsDemo_ParamsCfg   vitalSignsParamsCfg;
    MmwDemo_message     message;

    /* Sanity Check: Minimum argument check */
    if (argc != 4)
    {
        CLI_write ("Error: Invalid usage of the CLI command\n");
        return -1;
    }

    /* Initialize the ADC Output configuration: */
    memset ((void *)&vitalSignsParamsCfg, 0, sizeof(VitalSignsDemo_ParamsCfg));

    /* Populate configuration: */
    vitalSignsParamsCfg.startRange_m         = (float) atof (argv[1]);
    vitalSignsParamsCfg.endRange_m           = (float) atof (argv[2]);
    vitalSignsParamsCfg.rxAntennaProcess     = (uint8_t)   atof (argv[3]);

    /* Save Configuration to use later */
    MmwDemo_mssCfgUpdate((void *)&vitalSignsParamsCfg, offsetof(MmwDemo_CliCfg_t, vitalSignsParamsCfg),
        sizeof(vitalSignsParamsCfg), -1);

    /* Send configuration to DSS */
    memset((void *)&message, 0, sizeof(MmwDemo_message));
    message.type = MMWDEMO_MSS2DSS_VITALSIGNS_MEASUREMENT_PARAMS;
    memcpy((void *)&message.body.vitalSignsParamsCfg, (void *)&vitalSignsParamsCfg, sizeof(vitalSignsParamsCfg));

    if (MmwDemo_mboxWrite(&message) == 0)
        return 0;
    else
        return -1;

}
/**
 *  @b Description
 *  @n
 *      This is the CLI Handler for gui monitoring configuration
 *
 *  @param[in] argc
 *      Number of arguments
 *  @param[in] argv
 *      Arguments
 *
 *  @retval
 *      Success -   0
 *  @retval
 *      Error   -   <0
 */
static int32_t set_CLIGuiMonSel (int32_t argc, char* argv[])
{
    VitalSignsDemo_GuiMonSel   guiMonSel;
    MmwDemo_message     message;

    int8_t              subFrameNum;
    subFrameNum = -1;
    if(argc < 0)
    {
        CLI_write ("Error: Invalid usage of the CLI command\n");
        return -1;
    }

    /* Initialize the guiMonSel configuration: */
    memset ((void *)&guiMonSel, 0, sizeof(VitalSignsDemo_GuiMonSel));

    /* Populate configuration: */
    guiMonSel.sysInfo         = atoi(argv[1]);
    guiMonSel.vitalSignsStats = atoi(argv[2]);
    guiMonSel.rangeProfile    = atoi(argv[3]);
    guiMonSel.adcData         = atoi(argv[4]);

    MmwDemo_mssCfgUpdate((void *)&guiMonSel, offsetof(MmwDemo_CliCfg_t, vitalSignsGuiMonSel),
        sizeof(guiMonSel), subFrameNum);

    /* Send configuration to DSS */
    memset((void *)&message, 0, sizeof(MmwDemo_message));

    message.type = MMWDEMO_MSS2DSS_GUIMON_CFG;
    message.subFrameNum = subFrameNum;
    memcpy((void *)&message.body.vitalSigns_GuiMonSel, (void *)&guiMonSel, sizeof(guiMonSel));

    if (MmwDemo_mboxWrite(&message) == 0)
        return 0;
    else
        return -1;
}



/**
 *  @b Description
 *  @n
 *      This is the CLI Execution Task
 *
 *  \ingroup CLI_UTIL_INTERNAL_FUNCTION
 *
 *  @retval
 *      Not Applicable.
 */

/**
 *  @b Description
 *  @n
 *      function which can log the messages to the CLI console
 *
 *  @param[in]  format
 *      Format string
 *
 *  \ingroup CLI_UTIL_EXTERNAL_FUNCTION
 *
 *  @retval
 *      Not Applicable.
 */
void CLI_write (const char* format, ...)
{
    va_list     arg;
    char        logMessage[256];

    /* Format the message: */
    va_start (arg, format);
     vsnprintf (&logMessage[0], sizeof(logMessage), format, arg);
    va_end (arg);

}

void configureDSS(){

    int error;

    if (error != 0){
        gMmwMssMCB.commandUartDataBuffer[0] = CONFIG_ERROR;
        gMmwMssMCB.commandUartDataBuffer[1] = CONFIG_ERROR;
        gMmwMssMCB.commandUartDataBuffer[2] = CONFIG_ERROR;
        UART_write(gMmwMssMCB.commandUartHandle, &gMmwMssMCB.commandUartDataBuffer[0], N_BYTES_START_STOP_COMMAND);
    }

    sleep_ms(1000);
}


int send_configuration(){

   int errCode;
   char **paramVect;

   errCode = MMWaveFlushCfg();
   if (errCode<0)
       return -1;

   errCode = set_MMWaveDataOutputMode();
   if (errCode<0)
       return -1;

   errCode = set_MMWaveChannelCfg();
   if (errCode<0)
       return -1;

   errCode = set_MMWaveADCCfg();
   if (errCode<0)
       return -1;

   errCode = set_ADCBufCfg();
   if (errCode<0)
       return -1;

   errCode = set_MMWaveProfileCfg(0, paramVect);
   if (errCode<0)
       return -1;

   errCode = set_MMWaveChirpCfg();
   if (errCode<0)
      return -1;

    errCode = set_MMWaveFrameCfg(0, paramVect);
    if (errCode<0)
        return -1;

    errCode = set_MMWaveLowPowerCfg();
    if (errCode<0)
        return -1;

     return(0);


}
