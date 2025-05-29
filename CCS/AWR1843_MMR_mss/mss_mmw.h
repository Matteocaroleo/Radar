/**
 *   @file  mss_mmw.h
 *
 *   @brief
 *      This is the main header file for the Millimeter Wave Demo
 *
 *  \par
 *  NOTE:
 *      (C) Copyright 2016 Texas Instruments, Inc.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *    Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 *
 *    Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the
 *    distribution.
 *
 *    Neither the name of Texas Instruments Incorporated nor the names of
 *    its contributors may be used to endorse or promote products derived
 *    from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 *  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 *  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 *  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 *  OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 *  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 *  LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 *  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 *  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 *  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 *  OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
#ifndef MSS_MMW_DEMO_H
#define MSS_MMW_DEMO_H

#include <ti/common/mmwave_error.h>
#include <ti/drivers/soc/soc.h>
#include <ti/drivers/crc/crc.h>
#include <ti/drivers/uart/UART.h>
#include <ti/drivers/pinmux/pinmux.h>
#include <ti/drivers/esm/esm.h>
#include <ti/drivers/soc/soc.h>
#include <ti/drivers/mailbox/mailbox.h>
#include <ti/control/mmwave/mmwave.h>
#include <ti/drivers/watchdog/Watchdog.h>

#include "osal_nonos/Event_nonos.h"

/* MMW Demo Include Files */
#include "mmw_config.h"
#include "user_parameters.h" // Definition of N_PARAM

#ifdef __cplusplus
extern "C" {
#endif


/**
 * @brief
 *  Millimeter Wave Demo MCB
 *
 * @details
 *  The structure is used to hold all the relevant information for the
 *  Millimeter Wave demo
 */
#define N_BYTES_CONFIG_COMMAND 256


typedef struct MmwDemo_MCB_t
{
    /*! @brief   Configuration which is used to execute the demo */
    MmwDemo_Cfg                 cfg;

    /*! @brief   CLI related configuration */    
    MmwDemo_CliCfg_t            cliCfg[RL_MAX_SUBFRAMES];

    /*! @brief   CLI related configuration common across all subframes */
    MmwDemo_CliCommonCfg_t      cliCommonCfg;
 
    /*! * @brief   Handle to the SOC Module */
    SOC_Handle                  socHandle;

    /*! * @brief   Handle to the ESM Module */
    ESM_Handle                  esmHandle;

    /*! @brief   UART Logging Handle */
    UART_Handle                 loggingUartHandle;
    uint16_t                    loggingUartDataSize;

    /*! @brief   UART Command Rx/Tx Handle */
    UART_Handle                 commandUartHandle;
    uint8_t                     commandUartDataBuffer[N_BYTES_CONFIG_COMMAND];

    /*! @brief   This is the mmWave control handle which is used
     * to configure the BSS. */
    MMWave_Handle               ctrlHandle;

    /*!@brief   Handle to the peer Mailbox */
    Mbox_Handle              peerMailbox;

    /*! @brief   Semaphore handle for the mailbox communication */
    SemaphoreP_Handle            mboxSemHandle;

    /*! @brief   MSS system event handle */
    Event_Handle                eventHandle;

    /*! @brief   MSS system event handle */
    Event_Handle                eventHandleNotify;

    /*! @brief   Handle to the SOC chirp interrupt listener Handle */
    SOC_SysIntListenerHandle    chirpIntHandle;

    /*! @brief   Handle to the SOC frame start interrupt listener Handle */
    SOC_SysIntListenerHandle    frameStartIntHandle;

    /*! @brief   Current status of the sensor */
    bool                         isSensorStarted;

    /*! @brief   Has the mmWave module been opened? */
    bool                        isMMWaveOpen;

    /*! @brief DSS to MSS Isr Info Address */
    uint32_t                   dss2mssIsrInfoAddress;

    bool isReadyForCli;     // Indicates that the mss is ready to receive commands from cli
    bool isInStop;          // Indicates that the system is in stop mode
    bool stopRequired;      // Indicates that a stop message has been received and the dss has to be stopped
    bool realTimeError;     // Indicates that the mss has received a dss real time error message

} MmwDemo_MCB;

/**************************************************************************
 *************************** Extern Definitions ***************************
 **************************************************************************/
extern int32_t MmwDemo_mssDataPathConfig(void);
extern int32_t MmwDemo_mssDataPathStart(void);
extern int32_t MmwDemo_mssDataPathStop(void);

/* Sensor Management Module Exported API */

extern int32_t MmwDemo_waitSensorStopComplete(void);





extern void _MmwDemo_mssAssert(int32_t expression, const char *file, int32_t line);
extern void MmwDemo_nonOsLoop(uint8_t count);
#define MmwDemo_mssAssert(expression) {                                      \
                                         _MmwDemo_mssAssert(expression,      \
                                                  __FILE__, __LINE__);         \
                                         DebugP_assert(expression);             \
                                        }


#ifdef __cplusplus
}
#endif

#endif /* MSS_MMW_DEMO_H */

