using System.Configuration;

namespace VitalSignsGui
{


    partial class chartsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        ///     
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rangeProfile_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.frameCount_textBox = new System.Windows.Forms.TextBox();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.sensorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statVals_textBox = new System.Windows.Forms.TextBox();
            this.statNames_textBox = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.recFile_button = new System.Windows.Forms.Button();
            this.recFileName_textBox = new System.Windows.Forms.TextBox();
            this.recFileTimeAndDate_checkBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.missedCount_textBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sysInfoNames_textBox = new System.Windows.Forms.TextBox();
            this.sysInfoVals_textBox = new System.Windows.Forms.TextBox();
            this.adcData_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.showMagnitude = new System.Windows.Forms.RadioButton();
            this.showReal = new System.Windows.Forms.RadioButton();
            this.showImag = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.breathWaveform_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.heartWaveform_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.rangeProfile_chart)).BeginInit();
            this.menuStrip2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adcData_chart)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.breathWaveform_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heartWaveform_chart)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rangeProfile_chart
            // 
            chartArea1.AxisX.LabelStyle.Format = "0.0";
            chartArea1.AxisX.LabelStyle.Interval = 0.1D;
            chartArea1.AxisX.MajorGrid.Interval = 0.1D;
            chartArea1.AxisX.MajorTickMark.Interval = 0.1D;
            chartArea1.AxisX.Maximum = 1D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.Interval = 0.05D;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MinorTickMark.Interval = 0.1D;
            chartArea1.AxisX.Title = "range [m]";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX2.MajorTickMark.Interval = 0.2D;
            chartArea1.AxisY.Title = "magnitude [dB]";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.Name = "ChartArea1";
            this.rangeProfile_chart.ChartAreas.Add(chartArea1);
            this.rangeProfile_chart.DataSource = this.rangeProfile_chart.Series;
            this.rangeProfile_chart.Location = new System.Drawing.Point(321, 6);
            this.rangeProfile_chart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rangeProfile_chart.Name = "rangeProfile_chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.IsVisibleInLegend = false;
            series1.Name = "rangeProfile";
            this.rangeProfile_chart.Series.Add(series1);
            this.rangeProfile_chart.Size = new System.Drawing.Size(555, 338);
            this.rangeProfile_chart.TabIndex = 32;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            title1.Name = "Title1";
            title1.Text = "RANGE PROFILE";
            this.rangeProfile_chart.Titles.Add(title1);
            // 
            // frameCount_textBox
            // 
            this.frameCount_textBox.BackColor = System.Drawing.SystemColors.Window;
            this.frameCount_textBox.Location = new System.Drawing.Point(9, 21);
            this.frameCount_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.frameCount_textBox.Name = "frameCount_textBox";
            this.frameCount_textBox.ReadOnly = true;
            this.frameCount_textBox.Size = new System.Drawing.Size(100, 22);
            this.frameCount_textBox.TabIndex = 33;
            // 
            // messageTextBox
            // 
            this.messageTextBox.BackColor = System.Drawing.Color.White;
            this.messageTextBox.Location = new System.Drawing.Point(98, 6);
            this.messageTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(217, 22);
            this.messageTextBox.TabIndex = 36;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AutoSize = false;
            this.menuStrip2.BackColor = System.Drawing.Color.LightGray;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sensorToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(13, 1);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip2.Size = new System.Drawing.Size(80, 31);
            this.menuStrip2.TabIndex = 38;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // sensorToolStripMenuItem
            // 
            this.sensorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stopToolStripMenuItem,
            this.startToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.exitApplicationToolStripMenuItem});
            this.sensorToolStripMenuItem.Name = "sensorToolStripMenuItem";
            this.sensorToolStripMenuItem.Size = new System.Drawing.Size(60, 27);
            this.sensorToolStripMenuItem.Text = "Menu";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.stopToolStripMenuItem.Text = "Sensor Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.startToolStripMenuItem.Text = "Sensor Restart";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.updateToolStripMenuItem.Text = "Sensor Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // exitApplicationToolStripMenuItem
            // 
            this.exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            this.exitApplicationToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.exitApplicationToolStripMenuItem.Text = "Exit Application";
            this.exitApplicationToolStripMenuItem.Click += new System.EventHandler(this.exitApplicationToolStripMenuItem_Click_1);
            // 
            // statVals_textBox
            // 
            this.statVals_textBox.Location = new System.Drawing.Point(225, 21);
            this.statVals_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.statVals_textBox.Multiline = true;
            this.statVals_textBox.Name = "statVals_textBox";
            this.statVals_textBox.Size = new System.Drawing.Size(67, 90);
            this.statVals_textBox.TabIndex = 39;
            // 
            // statNames_textBox
            // 
            this.statNames_textBox.Location = new System.Drawing.Point(5, 21);
            this.statNames_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.statNames_textBox.Multiline = true;
            this.statNames_textBox.Name = "statNames_textBox";
            this.statNames_textBox.Size = new System.Drawing.Size(213, 90);
            this.statNames_textBox.TabIndex = 43;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.recFile_button);
            this.groupBox5.Controls.Add(this.recFileName_textBox);
            this.groupBox5.Controls.Add(this.recFileTimeAndDate_checkBox);
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox5.Location = new System.Drawing.Point(15, 612);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(292, 76);
            this.groupBox5.TabIndex = 44;
            this.groupBox5.TabStop = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(159, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 24);
            this.label6.TabIndex = 34;
            this.label6.Text = ".bin";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 31;
            this.label4.Text = "file name";
            // 
            // recFile_button
            // 
            this.recFile_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recFile_button.Location = new System.Drawing.Point(207, 31);
            this.recFile_button.Margin = new System.Windows.Forms.Padding(4);
            this.recFile_button.Name = "recFile_button";
            this.recFile_button.Size = new System.Drawing.Size(79, 26);
            this.recFile_button.TabIndex = 28;
            this.recFile_button.Text = "Start Rec";
            this.recFile_button.UseVisualStyleBackColor = true;
            this.recFile_button.Click += new System.EventHandler(this.recFile_button_Click);
            // 
            // recFileName_textBox
            // 
            this.recFileName_textBox.Location = new System.Drawing.Point(5, 31);
            this.recFileName_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.recFileName_textBox.Name = "recFileName_textBox";
            this.recFileName_textBox.Size = new System.Drawing.Size(152, 22);
            this.recFileName_textBox.TabIndex = 29;
            // 
            // recFileTimeAndDate_checkBox
            // 
            this.recFileTimeAndDate_checkBox.AutoSize = true;
            this.recFileTimeAndDate_checkBox.Location = new System.Drawing.Point(95, 9);
            this.recFileTimeAndDate_checkBox.Margin = new System.Windows.Forms.Padding(4);
            this.recFileTimeAndDate_checkBox.Name = "recFileTimeAndDate_checkBox";
            this.recFileTimeAndDate_checkBox.Size = new System.Drawing.Size(156, 20);
            this.recFileTimeAndDate_checkBox.TabIndex = 30;
            this.recFileTimeAndDate_checkBox.Text = "include date and time";
            this.recFileTimeAndDate_checkBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.missedCount_textBox);
            this.groupBox1.Controls.Add(this.frameCount_textBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(300, 53);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Frame Count";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "missed";
            // 
            // missedCount_textBox
            // 
            this.missedCount_textBox.BackColor = System.Drawing.SystemColors.Window;
            this.missedCount_textBox.Location = new System.Drawing.Point(190, 21);
            this.missedCount_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.missedCount_textBox.Name = "missedCount_textBox";
            this.missedCount_textBox.ReadOnly = true;
            this.missedCount_textBox.Size = new System.Drawing.Size(100, 22);
            this.missedCount_textBox.TabIndex = 34;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.statNames_textBox);
            this.groupBox2.Controls.Add(this.statVals_textBox);
            this.groupBox2.Location = new System.Drawing.Point(15, 348);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(300, 135);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vital Signs Statistics";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sysInfoNames_textBox);
            this.groupBox3.Controls.Add(this.sysInfoVals_textBox);
            this.groupBox3.Location = new System.Drawing.Point(13, 102);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(300, 234);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "System Info";
            // 
            // sysInfoNames_textBox
            // 
            this.sysInfoNames_textBox.Location = new System.Drawing.Point(5, 21);
            this.sysInfoNames_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sysInfoNames_textBox.Multiline = true;
            this.sysInfoNames_textBox.Name = "sysInfoNames_textBox";
            this.sysInfoNames_textBox.Size = new System.Drawing.Size(213, 205);
            this.sysInfoNames_textBox.TabIndex = 43;
            // 
            // sysInfoVals_textBox
            // 
            this.sysInfoVals_textBox.Location = new System.Drawing.Point(225, 21);
            this.sysInfoVals_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sysInfoVals_textBox.Multiline = true;
            this.sysInfoVals_textBox.Name = "sysInfoVals_textBox";
            this.sysInfoVals_textBox.Size = new System.Drawing.Size(67, 205);
            this.sysInfoVals_textBox.TabIndex = 39;
            // 
            // adcData_chart
            // 
            chartArea2.AxisX.LabelStyle.Format = "0";
            chartArea2.AxisX.LabelStyle.Interval = 20D;
            chartArea2.AxisX.MajorGrid.Interval = 20D;
            chartArea2.AxisX.MajorTickMark.Interval = 20D;
            chartArea2.AxisX.Maximum = 200D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.MinorGrid.Interval = 0.05D;
            chartArea2.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisX.MinorTickMark.Interval = 0.1D;
            chartArea2.AxisX.Title = "samples";
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            chartArea2.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisX2.MajorTickMark.Interval = 0.2D;
            chartArea2.AxisY.Title = "magnitude";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            chartArea2.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.Name = "ChartArea1";
            this.adcData_chart.ChartAreas.Add(chartArea2);
            this.adcData_chart.DataSource = this.adcData_chart.Series;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Name = "Legend1";
            this.adcData_chart.Legends.Add(legend1);
            this.adcData_chart.Location = new System.Drawing.Point(889, 6);
            this.adcData_chart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.adcData_chart.Name = "adcData_chart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.LegendText = "RX0";
            series2.Name = "adcData_rx0";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series3.Legend = "Legend1";
            series3.LegendText = "RX1";
            series3.Name = "adcData_rx1";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series4.Legend = "Legend1";
            series4.LegendText = "RX2";
            series4.Name = "adcData_rx2";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            series5.Legend = "Legend1";
            series5.LegendText = "RX3";
            series5.Name = "adcData_rx3";
            this.adcData_chart.Series.Add(series2);
            this.adcData_chart.Series.Add(series3);
            this.adcData_chart.Series.Add(series4);
            this.adcData_chart.Series.Add(series5);
            this.adcData_chart.Size = new System.Drawing.Size(555, 338);
            this.adcData_chart.TabIndex = 48;
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            title2.Name = "Title1";
            title2.Text = "ADC DATA";
            this.adcData_chart.Titles.Add(title2);
            // 
            // showMagnitude
            // 
            this.showMagnitude.AutoSize = true;
            this.showMagnitude.Location = new System.Drawing.Point(5, 21);
            this.showMagnitude.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.showMagnitude.Name = "showMagnitude";
            this.showMagnitude.Size = new System.Drawing.Size(91, 20);
            this.showMagnitude.TabIndex = 49;
            this.showMagnitude.TabStop = true;
            this.showMagnitude.Text = "magnitude";
            this.showMagnitude.UseVisualStyleBackColor = true;
            this.showMagnitude.CheckedChanged += new System.EventHandler(this.showMagnitude_CheckedChanged);
            // 
            // showReal
            // 
            this.showReal.AutoSize = true;
            this.showReal.Location = new System.Drawing.Point(5, 47);
            this.showReal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.showReal.Name = "showReal";
            this.showReal.Size = new System.Drawing.Size(51, 20);
            this.showReal.TabIndex = 50;
            this.showReal.TabStop = true;
            this.showReal.Text = "real";
            this.showReal.UseVisualStyleBackColor = true;
            this.showReal.CheckedChanged += new System.EventHandler(this.showMagnitude_CheckedChanged);
            // 
            // showImag
            // 
            this.showImag.AutoSize = true;
            this.showImag.Location = new System.Drawing.Point(5, 73);
            this.showImag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.showImag.Name = "showImag";
            this.showImag.Size = new System.Drawing.Size(58, 20);
            this.showImag.TabIndex = 51;
            this.showImag.TabStop = true;
            this.showImag.Text = "imag";
            this.showImag.UseVisualStyleBackColor = true;
            this.showImag.CheckedChanged += new System.EventHandler(this.showMagnitude_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.showMagnitude);
            this.groupBox4.Controls.Add(this.showImag);
            this.groupBox4.Controls.Add(this.showReal);
            this.groupBox4.Location = new System.Drawing.Point(1450, 6);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(121, 100);
            this.groupBox4.TabIndex = 52;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "adcDataType";
            // 
            // breathWaveform_chart
            // 
            chartArea3.AxisX.LabelStyle.Format = "0";
            chartArea3.AxisX.LabelStyle.Interval = 50D;
            chartArea3.AxisX.MajorGrid.Interval = 25D;
            chartArea3.AxisX.MajorTickMark.Interval = 25D;
            chartArea3.AxisX.Maximum = 250D;
            chartArea3.AxisX.Minimum = 0D;
            chartArea3.AxisX.MinorGrid.Interval = 25D;
            chartArea3.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.AxisX.MinorTickMark.Interval = 25D;
            chartArea3.AxisX.Title = "acquisitions";
            chartArea3.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            chartArea3.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea3.AxisX2.MajorTickMark.Interval = 20D;
            chartArea3.AxisY.Title = "pahse [rad]";
            chartArea3.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            chartArea3.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea3.Name = "ChartArea1";
            this.breathWaveform_chart.ChartAreas.Add(chartArea3);
            this.breathWaveform_chart.DataSource = this.breathWaveform_chart.Series;
            this.breathWaveform_chart.Location = new System.Drawing.Point(321, 348);
            this.breathWaveform_chart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.breathWaveform_chart.Name = "breathWaveform_chart";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series6.IsVisibleInLegend = false;
            series6.Name = "breathWfm";
            this.breathWaveform_chart.Series.Add(series6);
            this.breathWaveform_chart.Size = new System.Drawing.Size(555, 340);
            this.breathWaveform_chart.TabIndex = 53;
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            title3.Name = "Title1";
            title3.Text = "BREATH WAVEFORM";
            this.breathWaveform_chart.Titles.Add(title3);
            // 
            // heartWaveform_chart
            // 
            chartArea4.AxisX.LabelStyle.Format = "0";
            chartArea4.AxisX.LabelStyle.Interval = 50D;
            chartArea4.AxisX.MajorGrid.Interval = 25D;
            chartArea4.AxisX.MajorTickMark.Interval = 25D;
            chartArea4.AxisX.Maximum = 250D;
            chartArea4.AxisX.Minimum = 0D;
            chartArea4.AxisX.MinorGrid.Interval = 5D;
            chartArea4.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea4.AxisX.MinorTickMark.Interval = 10D;
            chartArea4.AxisX.Title = "acquisitions";
            chartArea4.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            chartArea4.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea4.AxisX2.MajorTickMark.Interval = 20D;
            chartArea4.AxisY.Title = "phase [rad]";
            chartArea4.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            chartArea4.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea4.Name = "ChartArea1";
            this.heartWaveform_chart.ChartAreas.Add(chartArea4);
            this.heartWaveform_chart.DataSource = this.heartWaveform_chart.Series;
            this.heartWaveform_chart.Location = new System.Drawing.Point(889, 348);
            this.heartWaveform_chart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.heartWaveform_chart.Name = "heartWaveform_chart";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series7.IsVisibleInLegend = false;
            series7.Name = "heartWfm";
            this.heartWaveform_chart.Series.Add(series7);
            this.heartWaveform_chart.Size = new System.Drawing.Size(555, 340);
            this.heartWaveform_chart.TabIndex = 54;
            title4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            title4.Name = "Title1";
            title4.Text = "HEART WAVEFORM";
            this.heartWaveform_chart.Titles.Add(title4);
            // 
            // chartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1583, 847);
            this.Controls.Add(this.heartWaveform_chart);
            this.Controls.Add(this.breathWaveform_chart);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.adcData_chart);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.rangeProfile_chart);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "chartsForm";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.rangeProfile_chart)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adcData_chart)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.breathWaveform_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heartWaveform_chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

 
        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart rangeProfile_chart;
        private System.Windows.Forms.TextBox frameCount_textBox;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem sensorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitApplicationToolStripMenuItem;
        private System.Windows.Forms.TextBox statVals_textBox;
        private System.Windows.Forms.TextBox statNames_textBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button recFile_button;
        private System.Windows.Forms.TextBox recFileName_textBox;
        private System.Windows.Forms.CheckBox recFileTimeAndDate_checkBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox sysInfoNames_textBox;
        private System.Windows.Forms.TextBox sysInfoVals_textBox;
        private System.Windows.Forms.TextBox missedCount_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart adcData_chart;
        private System.Windows.Forms.RadioButton showMagnitude;
        private System.Windows.Forms.RadioButton showReal;
        private System.Windows.Forms.RadioButton showImag;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart breathWaveform_chart;
        private System.Windows.Forms.DataVisualization.Charting.Chart heartWaveform_chart;
    }
}