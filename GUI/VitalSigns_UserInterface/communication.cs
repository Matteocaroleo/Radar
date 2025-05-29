using System;
using System.Collections.Generic;// For List
using System.IO.Ports;
using System.Linq; // For OrderBy

namespace VitalSignsGui
{
    public partial class communication
    {

        private static List<string> portList;
        public static SerialPort sp_data, sp_cmd; // Accessed by dataAcquire class
        private const int DATA_WAIT_MS = 5000;
        private const int CMD_WAIT_MS = 5000;  // Wait time for response from sensor

        public static List<string> InitializeComPort()
        {
            // Get list of all ports
            int num;
            portList = SerialPort.GetPortNames().OrderByDescending(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToList();
            return (portList);

        }
        public static int openPorts(int selectedIndex)
        {
            int retVal = 0;
            sp_data = new SerialPort(portList[selectedIndex], 921600);
            sp_data.ReadTimeout = DATA_WAIT_MS;

            sp_cmd = new SerialPort(portList[selectedIndex - 1], 115200);
            sp_cmd.ReadTimeout = CMD_WAIT_MS;
            
            try
            {
                sp_data.Open();
            }
            catch
            {
                retVal = -1;
            }

            try
            {
                sp_cmd.Open();
            }
            catch
            {
                retVal = -2;

            }

            return (retVal);
        }


        public static bool serialPort_sendCommand(string cmdString, byte nRetry)
        {

            bool msg_rcvd = false;
            byte cnt_try = 0;
            string reply;
            while (msg_rcvd == false && cnt_try < nRetry)
            {
                try
                {
                    reply = sp_cmd.ReadExisting();
                    sp_cmd.Write(cmdString);
                    reply = sp_cmd.ReadTo("\n");
                    reply = String.Concat(reply, "\n");

                    if (string.Equals(reply, cmdString) == true)
                        msg_rcvd = true;
                    else
                    {
                        cnt_try++;
                        sleep(1000);
                    }
                }

                catch
                {
                    msg_rcvd = false;
                    cnt_try++;
                }

            }
            if (msg_rcvd == true)
            {
               return (true);
            }

            return (false);

        }

        public static bool sendStopCommand()
        {
            bool isInStop = serialPort_sendCommand("sensorStop\n", 5);
            return (isInStop);

        }

        public static bool sendStartCommand()
        {
            bool isInStart = serialPort_sendCommand("sensorStart\n", 3);
            return (isInStart);

        }

        public static void updateParameters()
        {
            const int sleepTime = 600;
            string cmdString;
            string[] paramVal = ParamForm.paramVal;

            dataAcquire.NBYTES_PACKET_MIN = 0;
            if (paramVal[8] == "1")
                dataAcquire.NBYTES_PACKET_MIN += 20;
            if (paramVal[9] == "1")
                dataAcquire.NBYTES_PACKET_MIN += 24;
            if (paramVal[10] == "1")
                dataAcquire.NBYTES_PACKET_MIN += 128;
            if (paramVal[11] == "1")
                dataAcquire.NBYTES_PACKET_MIN += 1600;

            serialPort_sendCommand("flushCfg\n", 1);
            sleep(sleepTime);
            serialPort_sendCommand("dfeDataOutputMode 1\n", 1);
            sleep(sleepTime);
            serialPort_sendCommand("channelCfg 15 3 0\n", 1);
            sleep(sleepTime);
            serialPort_sendCommand("adcCfg 2 1\n", 1);
            sleep(sleepTime);
            serialPort_sendCommand("adcbufCfg -1 0 0 1 0\n", 1);
            sleep(sleepTime);

            // SaraDbg: rendere automatico
            cmdString = "profileCfg 0 " + paramVal[2] + " 7 6 " + paramVal[3] + " " +
                paramVal[4] + " 0 " + " " + paramVal[5] + " 1 100 2000 0 0 " +
                paramVal[6] + "\n" ;
            serialPort_sendCommand(cmdString, 1);
            sleep(sleepTime);

            serialPort_sendCommand("chirpCfg 0 0 0 0 0 0 0 1\n", 1);
            sleep(sleepTime);

            cmdString = "frameCfg 0 0 64 0 " + paramVal[7] + " 1 0\n";
            serialPort_sendCommand(cmdString, 1);
            sleep(sleepTime);

            serialPort_sendCommand("lowPower 0 1\n", 1);
            sleep(sleepTime);

            // vitalSigns, read from ParamForm
            cmdString = "vitalSignsCfg " + paramVal[0] + " " + paramVal[1] + " 4\n";
            serialPort_sendCommand(cmdString, 1);
            sleep(sleepTime);

            cmdString = "guiMonSel " + paramVal[8] + " " + paramVal[9] + " " +
                paramVal[10] + " " + paramVal[11] + "\n";
            serialPort_sendCommand(cmdString, 1);
            sleep(sleepTime);

        }
        private static void sleep(int n)
        {
            int i;
            for (i = 0; i < n * 1e6; i++) ;
        }

    }
}
