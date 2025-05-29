namespace VitalSignsGui
{
    partial class ParamForm
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
            this.paramHelpLabel = new System.Windows.Forms.Label();
            this.paramHelpCloseButton = new System.Windows.Forms.Button();
            this.sendParam_button = new System.Windows.Forms.Button();
            this.messageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // paramHelpLabel
            // 
            this.paramHelpLabel.AutoSize = true;
            this.paramHelpLabel.BackColor = System.Drawing.SystemColors.Control;
            this.paramHelpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramHelpLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.paramHelpLabel.Location = new System.Drawing.Point(44, 38);
            this.paramHelpLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramHelpLabel.Name = "paramHelpLabel";
            this.paramHelpLabel.Size = new System.Drawing.Size(37, 16);
            this.paramHelpLabel.TabIndex = 25;
            this.paramHelpLabel.Text = "help";
            this.paramHelpLabel.Visible = false;
            // 
            // paramHelpCloseButton
            // 
            this.paramHelpCloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramHelpCloseButton.ForeColor = System.Drawing.Color.Red;
            this.paramHelpCloseButton.Location = new System.Drawing.Point(123, 33);
            this.paramHelpCloseButton.Margin = new System.Windows.Forms.Padding(4);
            this.paramHelpCloseButton.Name = "paramHelpCloseButton";
            this.paramHelpCloseButton.Size = new System.Drawing.Size(25, 32);
            this.paramHelpCloseButton.TabIndex = 24;
            this.paramHelpCloseButton.Text = "X";
            this.paramHelpCloseButton.UseVisualStyleBackColor = true;
            this.paramHelpCloseButton.Visible = false;
            // 
            // sendParam_button
            // 
            this.sendParam_button.Location = new System.Drawing.Point(205, 505);
            this.sendParam_button.Margin = new System.Windows.Forms.Padding(5);
            this.sendParam_button.Name = "sendParam_button";
            this.sendParam_button.Size = new System.Drawing.Size(629, 33);
            this.sendParam_button.TabIndex = 26;
            this.sendParam_button.Text = "SEND PARAMETERS TO THE RADAR";
            this.sendParam_button.UseVisualStyleBackColor = true;
            this.sendParam_button.Click += new System.EventHandler(this.sendParam_button_Click);
            // 
            // messageLabel
            // 
            this.messageLabel.BackColor = System.Drawing.Color.White;
            this.messageLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.ForeColor = System.Drawing.Color.Red;
            this.messageLabel.Location = new System.Drawing.Point(205, 461);
            this.messageLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(629, 39);
            this.messageLabel.TabIndex = 27;
            this.messageLabel.Text = "messageLabel";
            // 
            // ParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.sendParam_button);
            this.Controls.Add(this.paramHelpLabel);
            this.Controls.Add(this.paramHelpCloseButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ParamForm";
            this.Text = "ParamForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label paramHelpLabel;
        private System.Windows.Forms.Button paramHelpCloseButton;
        private System.Windows.Forms.Button sendParam_button;
        private System.Windows.Forms.Label messageLabel;
    }
}