

using System;

using System.IO; // Required for reading/writing files
using System.IO.Ports;
using System.Windows.Forms; // Required for fileDialog
using System.Diagnostics; // Required for using watches 
using System.Numerics; // For complex numbers 

namespace VitalSignsGui
{
    internal class dataAcquire
    {
        public static bool dataFromBoard = true;

        private static FileStream fileIn;
        private static BinaryReader Br;
        private static string fileInName = " ";

        private const int NBYTES_PACKET_MAX = 16384;
        public static int NBYTES_PACKET_MIN = 1024;

        private static byte[] rcvdByteArray = new byte[NBYTES_PACKET_MAX];
        private static byte[] byteArrayTmp = new byte[NBYTES_PACKET_MAX];

        /* TLV types */
        public const int MMWDEMO_OUTPUT_MSG_STATS = 1; // Vital signs statistics
        public const int MMWDEMO_OUTPUT_MSG_RANGE_PROFILE = 2; // Range Profile
        public const int MMWDEMO_OUTPUT_MSG_ADC_DATA = 3; // Adc data
        public const int MMWDEMO_OUTPUT_MSG_SYSINFO = 4; // System info
        private const int NUM_TLV_MAX = 10;
        private static UInt32 frameCnt;
        public static UInt32[] tlvFrameCnt = new UInt32[NUM_TLV_MAX];

        private static SerialPort sp_data;

        /* RecData on file */
        public static bool isRecording, startRecording, stopRecording, recFileReady;
        public static string recFileName;
        private static Stream recFileOut;
        private static int nBytesWrittenToFile;
        public static BinaryWriter recFileBw;

        private static uint prevFrame = 0;

        public static byte adcDataFormat = 0;
        public const byte SHOW_MAGNITUDE = 0;
        public const byte SHOW_REAL = 1;
        public const byte SHOW_IMAG = 2;
        private const byte NUM_RX = 4;

        private static int frameIdx = 0;
        public static bool newDataAvailable = false;
        
        public static void readData()
        {
            isRecording = false;
            startRecording = false;
            stopRecording = false;
            nBytesWrittenToFile = 0;
            recFileReady = false;

            if (dataFromBoard == true)
            {
                sp_data = communication.sp_data;
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.ShowDialog();
                fileIn = File.OpenRead(ofd.FileName);
                Br = new BinaryReader(fileIn);
            }


            byte[] magicWord = { 2, 1, 4, 3, 6, 5, 8, 7 };
            int nBytesMagic = magicWord.Length;
            int nBytesTot;
            int offset;

            bool magicOk = false;
            int nBytesAvailable = 0;
            int newData = 0;
            uint tlvType = 0;
            int nOldBytes = 0;
            uint tlvLength = 0;
            int dataArrayOffset = 0;

            bool done = false;

            int pktCounter = 0;
            const int MAX_N_FRAMES = 128;

            if (dataFromBoard == true)
            {
                try
                {
                }
                catch
                {
                    done = true;
                }

            }

            while (done == false)
            {

                nBytesAvailable = 0; //SaraDbg
                //nBytesAvailable = nOldBytes;
                dataArrayOffset = nBytesAvailable;

                if (isRecording == false && startRecording == true)
                    startRec();
                else if (isRecording == true && stopRecording == true)
                    stopRec();
                do
                {

                    try
                    {

                        if (dataFromBoard == true)
                        {
                            newData = sp_data.Read(byteArrayTmp, 0, NBYTES_PACKET_MAX - nBytesAvailable);

                            if (isRecording == true)
                            {
                                recFileBw.Write(byteArrayTmp, 0, newData);
                                nBytesWrittenToFile += newData;
                            }

                            nBytesAvailable += newData;
                            Array.Copy(byteArrayTmp, 0, rcvdByteArray, dataArrayOffset, newData);

                            dataArrayOffset = nBytesAvailable;


                        }
                        else
                        {
                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            while ((watch.ElapsedTicks * 1e3 / Stopwatch.Frequency) < 20) ;

                            byte[] rcvdByteArrayTmp = Br.ReadBytes(NBYTES_PACKET_MIN);
                            nBytesTot = rcvdByteArrayTmp.Length;
                            nBytesAvailable += rcvdByteArrayTmp.Length;
                            if (nBytesAvailable >= NBYTES_PACKET_MIN)
                            {
                                Array.Copy(rcvdByteArrayTmp, 0, rcvdByteArray, nOldBytes, nBytesAvailable - nOldBytes);
                                nBytesAvailable += nOldBytes;
                            }
                            else
                            {
                                fileIn.Close();
                                fileIn = File.OpenRead(fileInName);
                                Br = new BinaryReader(fileIn);
                                nBytesAvailable = 0;

                                nOldBytes = 0;
                            }
                            watch = Stopwatch.StartNew();

                        }

                    }
                    catch
                    {

                    }
                } while (nBytesAvailable < NBYTES_PACKET_MIN && done == false);

                nBytesTot = nBytesAvailable;
                int idxMagic = 0;
                int idxMagicStart = 0;
                int idxRcv = 0;
                bool startFound = false;
                offset = 0;

                while (nBytesAvailable > 0)
                {
                    idxMagicStart = offset;
                    // Check for the preamble                   
                    while (magicOk == false && idxRcv <= nBytesTot - nBytesMagic)
                    {
                        idxRcv = idxMagicStart;
                        if (rcvdByteArray[idxRcv] == magicWord[0] && rcvdByteArray[idxRcv + 1] == magicWord[1])
                        {
                            startFound = true;
                            idxMagic = 2;
                            idxRcv += 2;
                            while (idxMagic < nBytesMagic && startFound == true && idxRcv<nBytesTot)
                            {
                                if (rcvdByteArray[idxRcv] != magicWord[idxMagic])
                                    startFound = false;
                                idxRcv++;
                                idxMagic++;
                            }
                            if (startFound == true)
                                magicOk = true;
                            else
                                idxMagicStart++;
                        }
                        else
                        {
                            idxMagicStart++;
                        }
                    }

                    nBytesAvailable = nBytesTot - idxRcv;
                    if (magicOk == true)
                    {

                        newDataAvailable = true;

                        offset = idxRcv;

                        UInt32 version = BitConverter.ToUInt32(rcvdByteArray, offset); offset += 4;
                        UInt32 totalPacketLen = BitConverter.ToUInt32(rcvdByteArray, offset); offset += 4;
                        UInt32 platform = BitConverter.ToUInt32(rcvdByteArray, offset); offset += 4;
                        frameCnt = BitConverter.ToUInt32(rcvdByteArray, offset); offset += 4;

                        NBYTES_PACKET_MIN = (int)totalPacketLen * 2;

                        chartsForm.frameCount = frameCnt;
                        if (prevFrame>0 && (Int16)(frameCnt - prevFrame) > 0 && (frameCnt - prevFrame) > 1)
                            chartsForm.missedCount += (frameCnt - prevFrame);

                        prevFrame = frameCnt;

                        UInt32 timeCpuCycles = BitConverter.ToUInt32(rcvdByteArray, offset); offset += 4;
                        UInt32 numDetectedObj = BitConverter.ToUInt32(rcvdByteArray, offset); offset += 4;
                        UInt32 numTLVs = BitConverter.ToUInt32(rcvdByteArray, offset); offset += 4;
                        UInt32 subFrameNumber = BitConverter.ToUInt32(rcvdByteArray, offset); offset += 4;

                        pktCounter++;
                        if (pktCounter >= MAX_N_FRAMES)
                            pktCounter = 0;

                        if (nBytesAvailable >= 8) // type and length
                        {

                            for (int tlvIdx = 0; tlvIdx < numTLVs; tlvIdx++)
                            {
                                getNextTLV(rcvdByteArray, ref offset, ref tlvType, ref tlvLength);

                                nBytesAvailable = nBytesTot - offset;
                                if (nBytesAvailable >= tlvLength)
                                {
                                    readTLV(rcvdByteArray, ref offset, ref tlvType, ref tlvLength);
                                    tlvFrameCnt[tlvType] = frameCnt;
                                }
                                else
                                {
                                    nOldBytes = nBytesAvailable;
                                    if (nOldBytes < 0)
                                        nOldBytes = 0; 
                                    Array.Copy(rcvdByteArray, offset, rcvdByteArray, 0, nOldBytes);
                                    Array.Clear(rcvdByteArray, nOldBytes, rcvdByteArray.Length - nOldBytes);
                                    nBytesAvailable = 0;
                                    dataArrayOffset = nOldBytes;
                                    break;
                                }
                            }

                            frameIdx++;
                            if (frameIdx >= chartsForm.N_FRAMES_ON_CHART)
                                frameIdx = 0;

                        }
                        else
                        {
                            nOldBytes = nBytesAvailable;
                            //Array.Copy(rcvdByteArray, 0, rcvdByteArray, offset, nOldBytes);
                            Array.Copy(rcvdByteArray, offset, rcvdByteArray, 0, nOldBytes);
                            Array.Clear(rcvdByteArray, nOldBytes, rcvdByteArray.Length - nOldBytes);
                            nBytesAvailable = 0;
                            dataArrayOffset = nOldBytes;
                            offset = 0;
                        }

                        magicOk = false;
                        if (nBytesAvailable < 0)
                            nBytesAvailable = 0;

                    }
                    else
                    {
                        // If there are only few bytes, keep them and process them in the next packet
                        nOldBytes = nBytesAvailable;
                        Array.Copy(rcvdByteArray, offset, rcvdByteArray, 0, nOldBytes);
                        offset = 0;
                        Array.Clear(rcvdByteArray, nOldBytes, rcvdByteArray.Length - nOldBytes);
                        dataArrayOffset = nOldBytes;
                        nBytesAvailable = 0;

                    }


                }
            }

            Application.Exit();


        }
        

        private static void getNextTLV(byte[] dataArray, ref int offset, ref uint tlvType, ref uint tlvLength)
        {
            tlvType = BitConverter.ToUInt32(dataArray, offset); offset += 4;
            tlvLength = BitConverter.ToUInt32(dataArray, offset); offset += 4;
            offset += 4; // shared memory address, not used
        }

        private static void readTLV(byte[] dataArray, ref int offset0, ref uint tlvType, ref uint tlvLength)
        {

            Int32 offset = offset0;
            Int32 cnt = 0;

            if (tlvType == MMWDEMO_OUTPUT_MSG_SYSINFO)
            {
                chartsForm.rangeAccuracy = BitConverter.ToSingle(dataArray, offset); offset += 4;
                chartsForm.framePeriodicity_ms = BitConverter.ToSingle(dataArray, offset); offset += 4;

                UInt16 numChirpsPerFrame = BitConverter.ToUInt16(dataArray, offset); offset += 2;
                UInt16 rangeBinStartIndex = BitConverter.ToUInt16(dataArray, offset); offset += 2;
                UInt16 rangeBinEndIndex = BitConverter.ToUInt16(dataArray, offset); offset += 2;
                Byte rxAntennaProc = dataArray[offset]; offset += 1;

                chartsForm.sysInfoVals[cnt++] = chartsForm.rangeAccuracy * 100;
                chartsForm.sysInfoVals[cnt++] = chartsForm.framePeriodicity_ms;
                chartsForm.sysInfoVals[cnt++] = rxAntennaProc;
                chartsForm.sysInfoVals[cnt++] = rangeBinStartIndex* chartsForm.rangeAccuracy;
                chartsForm.sysInfoVals[cnt++] = rangeBinEndIndex*chartsForm.rangeAccuracy;

                chartsForm.startRangeIdx = rangeBinStartIndex; // Needed to update rangeAxis of charts


            }
            else if (tlvType == MMWDEMO_OUTPUT_MSG_STATS)
            {
                UInt16 rangeBinIndexMax = BitConverter.ToUInt16(dataArray, offset); offset += 2;
                UInt16 rangeBinIndexPhase = BitConverter.ToUInt16(dataArray, offset); offset += 2;
                
                float maxVal = BitConverter.ToSingle(dataArray, offset); offset += 4;
                /* chest dispacement */
                float unwrapPhasePeak_mm = BitConverter.ToSingle(dataArray, offset); offset += 4;
                /* breathing waveform */
                float outputFilterBreathOut = BitConverter.ToSingle(dataArray, offset); offset += 4;
                /* heart rate waveform */
                float outputFilterHeartOut = BitConverter.ToSingle(dataArray, offset); offset += 4;

                UInt32 frameCounter = BitConverter.ToUInt32(dataArray, offset); offset += 4;

                chartsForm.breathWfrmShow[frameIdx] = outputFilterBreathOut;
                chartsForm.heartWfrmShow[frameIdx] = outputFilterHeartOut;

            }
            else if (tlvType == MMWDEMO_OUTPUT_MSG_RANGE_PROFILE)
            {
                Int16 rangeBinIdx;
                Int16 re, im;
                float magSq;
                uint numRb = (tlvLength / 4);

                chartsForm.numRangeBinsPlot = (int)numRb;
                for (rangeBinIdx = 0; rangeBinIdx < numRb; rangeBinIdx++)
                {
                    re = BitConverter.ToInt16(dataArray, offset); offset += 2;
                    im = BitConverter.ToInt16(dataArray, offset); offset += 2;

                    magSq = re * re + im * im;
                    if (magSq <= 1)
                        magSq = 1;

                    chartsForm.rangeProfileShow[rangeBinIdx] = 10.0f * (float)Math.Log10((double)magSq);
                }
            }
            else if (tlvType == MMWDEMO_OUTPUT_MSG_ADC_DATA)
            {
                Int16 sampleIdx, rxIdx;
                Int16 re, im;
                uint numAdcSamples = (tlvLength / 4) / NUM_RX;
                chartsForm.numAdcSamples = numAdcSamples;
                Complex newVal;
                float valForChart = 0;
                for (rxIdx=0; rxIdx < NUM_RX; rxIdx++) {

                    for (sampleIdx = 0; sampleIdx < numAdcSamples; sampleIdx++)
                    {
                        re = BitConverter.ToInt16(dataArray, offset); offset += 2;
                        im = BitConverter.ToInt16(dataArray, offset); offset += 2;
                        newVal = new Complex(re, im);

                        if (adcDataFormat == SHOW_MAGNITUDE)
                            valForChart = (float)newVal.Magnitude;
                        else if (adcDataFormat == SHOW_REAL)
                            valForChart = (float)newVal.Real;
                        else if (adcDataFormat == SHOW_IMAG)
                            valForChart = (float)newVal.Imaginary;

                        if (sampleIdx < chartsForm.N_ADC_DATA_MAX)
                            chartsForm.adcDataShow[rxIdx* chartsForm.N_ADC_DATA_MAX + sampleIdx] = valForChart;
                    }
                }

            }

            offset0 += (int)tlvLength;
        }

        private static void sleep(int n)
        {
            int i;
            for (i = 0; i < n * 1e6; i++) ;
        }

        private static void startRec()
        {
            isRecording = true;
            startRecording = false;

            recFileOut = File.Create(recFileName + ".bin");
            recFileBw = new BinaryWriter(recFileOut);

            nBytesWrittenToFile = 0;
            recFileReady = true;

        }

        private static void stopRec()
        {
            stopRecording = false;

            recFileReady = false;
            recFileBw.Close();
            recFileOut.Close();

            isRecording = false;


        }
    }
    }
