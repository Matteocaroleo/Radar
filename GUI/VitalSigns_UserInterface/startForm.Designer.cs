namespace VitalSignsGui
{
    partial class startForm
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
        private void InitializeComponent()
        {
            this.realTime_radioButton = new System.Windows.Forms.RadioButton();
            this.file_radioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.start_button = new System.Windows.Forms.Button();
            this.comPort_groupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.noComPort_label = new System.Windows.Forms.Label();
            this.portCombobox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.comPort_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // realTime_radioButton
            // 
            this.realTime_radioButton.AutoSize = true;
            this.realTime_radioButton.Location = new System.Drawing.Point(8, 23);
            this.realTime_radioButton.Margin = new System.Windows.Forms.Padding(4);
            this.realTime_radioButton.Name = "realTime_radioButton";
            this.realTime_radioButton.Size = new System.Drawing.Size(79, 20);
            this.realTime_radioButton.TabIndex = 0;
            this.realTime_radioButton.TabStop = true;
            this.realTime_radioButton.Text = "real time";
            this.realTime_radioButton.UseVisualStyleBackColor = true;
            // 
            // file_radioButton
            // 
            this.file_radioButton.AutoSize = true;
            this.file_radioButton.Location = new System.Drawing.Point(8, 52);
            this.file_radioButton.Margin = new System.Windows.Forms.Padding(4);
            this.file_radioButton.Name = "file_radioButton";
            this.file_radioButton.Size = new System.Drawing.Size(45, 20);
            this.file_radioButton.TabIndex = 1;
            this.file_radioButton.TabStop = true;
            this.file_radioButton.Text = "file";
            this.file_radioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.start_button);
            this.groupBox1.Controls.Add(this.comPort_groupBox);
            this.groupBox1.Controls.Add(this.realTime_radioButton);
            this.groupBox1.Controls.Add(this.file_radioButton);
            this.groupBox1.Location = new System.Drawing.Point(32, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(267, 318);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Source";
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(38, 229);
            this.start_button.Margin = new System.Windows.Forms.Padding(4);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(175, 67);
            this.start_button.TabIndex = 4;
            this.start_button.Text = "START";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // comPort_groupBox
            // 
            this.comPort_groupBox.Controls.Add(this.label1);
            this.comPort_groupBox.Controls.Add(this.noComPort_label);
            this.comPort_groupBox.Controls.Add(this.portCombobox);
            this.comPort_groupBox.Location = new System.Drawing.Point(0, 98);
            this.comPort_groupBox.Margin = new System.Windows.Forms.Padding(4);
            this.comPort_groupBox.Name = "comPort_groupBox";
            this.comPort_groupBox.Padding = new System.Windows.Forms.Padding(4);
            this.comPort_groupBox.Size = new System.Drawing.Size(267, 123);
            this.comPort_groupBox.TabIndex = 4;
            this.comPort_groupBox.TabStop = false;
            this.comPort_groupBox.Text = "PLEASE SELECT DATA PORT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "for real time data only";
            // 
            // noComPort_label
            // 
            this.noComPort_label.AutoSize = true;
            this.noComPort_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noComPort_label.ForeColor = System.Drawing.Color.Red;
            this.noComPort_label.Location = new System.Drawing.Point(8, 78);
            this.noComPort_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.noComPort_label.Name = "noComPort_label";
            this.noComPort_label.Size = new System.Drawing.Size(205, 17);
            this.noComPort_label.TabIndex = 12;
            this.noComPort_label.Text = "NO COM PORT DETECTED";
            this.noComPort_label.Visible = false;
            // 
            // portCombobox
            // 
            this.portCombobox.FormattingEnabled = true;
            this.portCombobox.Location = new System.Drawing.Point(8, 39);
            this.portCombobox.Margin = new System.Windows.Forms.Padding(4);
            this.portCombobox.Name = "portCombobox";
            this.portCombobox.Size = new System.Drawing.Size(160, 24);
            this.portCombobox.TabIndex = 4;
            // 
            // startForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 392);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "startForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.comPort_groupBox.ResumeLayout(false);
            this.comPort_groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton realTime_radioButton;
        private System.Windows.Forms.RadioButton file_radioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox comPort_groupBox;
        private System.Windows.Forms.Label noComPort_label;
        private System.Windows.Forms.ComboBox portCombobox;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Label label1;
    }
}