#define UART_CHAR

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using System.IO.Ports;

namespace VitalSignsGui
{
    public partial class chartsForm : Form
    {

        public const int N_FRAMES_ON_CHART = 250;
        private const int N_RB_ON_CHART_MAX = 64;
        public const int N_ADC_DATA_MAX = 200;
        public const int N_RX = 4;

        private static int CHART_UPDATE_INTERVAL_MS = 100;
        private static int MESSAGE_TIMEOUT_MS = 5000;


        private static UInt16[] adcAx = new UInt16[N_ADC_DATA_MAX];
        public static float[] adcDataShow = new float[N_RX*N_ADC_DATA_MAX];

        private static float[] rangeAx = new float[N_RB_ON_CHART_MAX];
        public static float[] rangeProfileShow = new float[N_RB_ON_CHART_MAX];

        private short[] frameAx = new short[N_FRAMES_ON_CHART];
        public static float[] breathWfrmShow = new float[N_FRAMES_ON_CHART];
        public static float[] heartWfrmShow = new float[N_FRAMES_ON_CHART];

        public static uint frameCount, missedCount;

        public static int numRangeBinsPlot = N_RB_ON_CHART_MAX;
        private static int numRangeBinsPlotPrev = -1;

        public static float rangeAccuracy = 0.04f;
        public static float framePeriodicity_ms;

        private static int messageTextBox_Timeout;

 
        private static int nStatisticsPlot = 0;
        private static int nInfo = 0;
        private const int N_STATISTICS_MAX = 128;
        private const int N_SYSINFO_MAX = 16;

        private static float[] statistics = new float[N_STATISTICS_MAX];
        private static string[] statNames = new string[N_STATISTICS_MAX];
        public static float[] sysInfoVals = new float[N_SYSINFO_MAX];
        private static string[] sysInfoNames = new string[N_SYSINFO_MAX];

        public static int startRangeIdx = 0;
        private static int startRangeIdxPrev = -1;
        public static uint numAdcSamples = N_ADC_DATA_MAX;

        private Color buttonColor = Color.WhiteSmoke;
        private Color buttonPressedColor = Color.DarkGray;

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Interval = CHART_UPDATE_INTERVAL_MS;

            if (startRangeIdx != startRangeIdxPrev || numRangeBinsPlot != numRangeBinsPlotPrev)
            {
                startRangeIdxPrev = startRangeIdx;
                numRangeBinsPlotPrev = numRangeBinsPlot;
                for (int i = 0; i < N_RB_ON_CHART_MAX; i++)
                    rangeAx[i] = (float)(startRangeIdx + i) * rangeAccuracy;

                float rMin = rangeAx[0];
                float rMax = rangeAx[numRangeBinsPlot - 1];
                float dR = (float)Math.Max(0.1, (rMax - rMin) / 10);

                rangeProfile_chart.ChartAreas[0].AxisX.Minimum = rangeAx[0];
                rangeProfile_chart.ChartAreas[0].AxisX.Maximum = rangeAx[numRangeBinsPlot-1];

                rangeProfile_chart.ChartAreas[0].AxisX.LabelStyle.Interval = dR;
                rangeProfile_chart.ChartAreas[0].AxisX.MajorGrid.Interval = dR;
                rangeProfile_chart.ChartAreas[0].AxisX.MajorTickMark.Interval = dR;

                rangeProfile_chart.ChartAreas[0].AxisX.MinorGrid.Interval = dR/2;
            }


            rangeProfile_chart.Series[0].Points.DataBindXY(rangeAx, rangeProfileShow);

            float[] adcPlot = new float[N_ADC_DATA_MAX];
            for (int i = 0; i < N_RX; i++)
            {
                Array.Copy(adcDataShow, i*N_ADC_DATA_MAX, adcPlot, 0, N_ADC_DATA_MAX);
                adcData_chart.Series[i].Points.DataBindXY(adcAx, adcPlot);
            }
            adcData_chart.ChartAreas[0].AxisX.Maximum = numAdcSamples;

            breathWaveform_chart.Series[0].Points.DataBindXY(frameAx, breathWfrmShow);
            heartWaveform_chart.Series[0].Points.DataBindXY(frameAx, heartWfrmShow);

            frameCount_textBox.Text = frameCount.ToString();
            missedCount_textBox.Text = missedCount.ToString();


            statNames_textBox.Clear();
            statVals_textBox.Clear();
            sysInfoNames_textBox.Clear();
            sysInfoVals_textBox.Clear();
 
            for (int ii = 0; ii < nInfo; ii++)
            {
                sysInfoNames_textBox.Text += sysInfoNames[ii] + Environment.NewLine;
                sysInfoVals_textBox.Text += sysInfoVals[ii].ToString("0.##") + Environment.NewLine;
            }
            for (int ii = 0; ii < 20 /*nStatisticsPlot*/; ii++)
            {
                statNames_textBox.Text += statNames[ii] + Environment.NewLine;
                statVals_textBox.Text += statistics[ii].ToString("0.##") + Environment.NewLine;
            }

            /* message textbox update */
            if (messageTextBox_Timeout >= 0)
            {
                messageTextBox_Timeout--;
                if (messageTextBox_Timeout <= 0)
                {
                    messageTextBox.Text = " ";
                    messageTextBox.BackColor = Color.LightGray;
                }
            }
        }

        public chartsForm()
        {
            InitializeComponent();
            my_InitializeComponent();

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

        }
        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            if (readData.dataFromBoard == true)
                readData.sp_cmd.Close();

            done = true;
           */
        }


        private void my_InitializeComponent()
        {
            Axis ax;
            int i;
            Int32 cntStat = 0;
            Int32 cntInfo = 0;

            for (i = 0; i < N_RB_ON_CHART_MAX; i++)
                rangeAx[i] = (float)(i * rangeAccuracy);

            for (i = 0; i < N_ADC_DATA_MAX; i++)
                adcAx[i] = (UInt16)i;

            for (i = 0; i < N_FRAMES_ON_CHART; i++)
                frameAx[i] = (short)i;

            // Range profile, xAxis
            ax = rangeProfile_chart.ChartAreas[0].AxisX;
            ax.Minimum = rangeAx[0];
            ax.Maximum = rangeAx[numRangeBinsPlot - 1];
            ax.MajorGrid.Interval = 0.1;
            ax.MajorTickMark.Interval = ax.MajorGrid.Interval;
            ax.LabelStyle.Interval = ax.MajorTickMark.Interval;
            ax.LabelStyle.Format = "0.0";

            // Range profile, yAxis
            ax = rangeProfile_chart.ChartAreas[0].AxisY;
            ax.Minimum = 0.0f;
            ax.Maximum = 120.0f;
          
            ax = breathWaveform_chart.ChartAreas[0].AxisX;
            ax.Minimum = 0;
            ax.Maximum = N_FRAMES_ON_CHART;
            ax.MajorGrid.Interval = N_FRAMES_ON_CHART / 10;

            ax = heartWaveform_chart.ChartAreas[0].AxisX;
            ax.Minimum = 0;
            ax.Maximum = N_FRAMES_ON_CHART;
            ax.MajorGrid.Interval = N_FRAMES_ON_CHART / 10;


            messageTextBox.Text = " ";
            messageTextBox.BackColor = Color.LightGray;
  
            sysInfoNames[cntInfo++] = "rangeAccuracy [cm]";
            sysInfoNames[cntInfo++] = "framePeriodicity [ms]";
            sysInfoNames[cntInfo++] = "rxAntennaProcessed";
            sysInfoNames[cntInfo++] = "minimumRange [m]";
            sysInfoNames[cntInfo++] = "masximumRange [m]";


            nStatisticsPlot = cntStat;
            nInfo = cntInfo;

            statNames_textBox.Multiline = true;
            statVals_textBox.Multiline = true;
            sysInfoNames_textBox.Multiline = true;
            sysInfoVals_textBox.Multiline = true;
            statNames_textBox.Size = new Size(165, 14* nStatisticsPlot);
            statVals_textBox.Size = new Size(50, 14 * nStatisticsPlot);

            missedCount = 0;

            showReal.Checked = true;

            }

        public const int N_BYTES_CONFIG_COMMAND = 128;
        public const int STOP_CMD = 0xB;
        public const int START_CMD = 0xA;
        public static byte[] cmd_buffer = new byte[N_BYTES_CONFIG_COMMAND];
        public const int N_RETRY_UART = 5; // Maximum number of retry 
        public const int NBYTES_PACKET_MAX = 10000;//when sending heatmap increase for space//when not reduce for speed
        public const int NBYTES_PACKET_MIN = 5000;//increase if problem on index when sending matrix//reduce for speed


        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {

            bool retVal = communication.sendStopCommand();

           // string cmdString = "sensorStop\n";
           // bool retVal = communication.serialPort_sendCommand(cmdString+"\n", 3);
            if (retVal == true)
            {
                messageTextBox.Text = "sensorStop ... delivered";
                messageTextBox.BackColor = Color.Green;
                messageTextBox_Timeout = (int)Math.Round((float)MESSAGE_TIMEOUT_MS / (float)CHART_UPDATE_INTERVAL_MS);
            }

            communication.serialPort_sendCommand("guiMonSel 1 1 0 1\n", 1);
            

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cmdString = "sensorStart";
            bool retVal = communication.serialPort_sendCommand(cmdString+"\n", 1);
            messageTextBox.Text = cmdString + Environment.NewLine + "... delivered";
            messageTextBox.BackColor = Color.Green;
            messageTextBox_Timeout = (int)Math.Round((float)MESSAGE_TIMEOUT_MS / (float)CHART_UPDATE_INTERVAL_MS);

        }


        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ParamForm form = (ParamForm)GetForm("ParamForm");
            if (form != null)
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.BringToFront();
                if (form.WindowState == FormWindowState.Minimized)
                    form.WindowState = FormWindowState.Normal;
            }
            else
            {
                form = new ParamForm();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();

            }
          

        }
        private void exitApplicationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string errMessage = string.Format("Sure you want to quit?");
            var dialogResult =
                MessageBox.Show(errMessage, "Connection Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (dialogResult == DialogResult.Yes)
                exitApplication();

        }

        private static void exitApplication()
        {
            if (communication.sp_cmd.IsOpen)
                communication.sp_cmd.Close();
            if (communication.sp_data.IsOpen)
                communication.sp_data.Close();

            Application.Exit();
            Environment.Exit(0);

        }

        private void showMagnitude_CheckedChanged(object sender, EventArgs e)
        {
            if (showMagnitude.Checked == true) {
                dataAcquire.adcDataFormat = dataAcquire.SHOW_MAGNITUDE;
                adcData_chart.ChartAreas[0].AxisY.Title = "magnitude";
                adcData_chart.ChartAreas[0].AxisY.Minimum = 0;
                adcData_chart.ChartAreas[0].AxisY.Maximum = 32768;
            }
            else if (showReal.Checked == true) {
                dataAcquire.adcDataFormat = dataAcquire.SHOW_REAL;
                adcData_chart.ChartAreas[0].AxisY.Title = "real";
                adcData_chart.ChartAreas[0].AxisY.Minimum = -8192;
                adcData_chart.ChartAreas[0].AxisY.Maximum = 8192;
            }
            else if (showImag.Checked == true) { 
                dataAcquire.adcDataFormat = dataAcquire.SHOW_IMAG;
                adcData_chart.ChartAreas[0].AxisY.Title = "imaginary";
                adcData_chart.ChartAreas[0].AxisY.Minimum = -8192;
                adcData_chart.ChartAreas[0].AxisY.Maximum = 8192;
            }
        }
        private void recFile_button_Click(object sender, EventArgs e)
        {

            if (dataAcquire.isRecording == false)
            {

                recFile_button.BackColor = buttonPressedColor;
                recFile_button.Text = "Recording";

                string fileName = recFileName_textBox.Text;
                if (String.IsNullOrEmpty(fileName) == true)
                    fileName = "dataFile";

                if (recFileTimeAndDate_checkBox.Checked == true)
                {
                    var time = DateTime.Now;
                    string formattedTime = time.ToString("yyyyMMdd_hhmmss_tt");
                    fileName = fileName + "_" + formattedTime;
                }
                dataAcquire.recFileName = fileName;
                dataAcquire.startRecording = true;

            }
            else if (dataAcquire.isRecording == true)
            {
                dataAcquire.stopRecording = true;
                recFile_button.BackColor = buttonColor;
                recFile_button.Text = "Start Rec";

            }

        }

        private Form GetForm(string formName)
        {
            FormCollection openedForm = Application.OpenForms;

            foreach (Form form in openedForm)
            {
                if (form.Name.Equals(formName, StringComparison.OrdinalIgnoreCase))
                {
                    form.BringToFront();
                    return form;
                }
            }
            return null;
        }

    }
}

