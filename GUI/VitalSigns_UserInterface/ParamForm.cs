using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO; // Required for reading/writing files
using System.Linq;

namespace VitalSignsGui
{
    public partial class ParamForm : Form
    {
        private const int N_COMMANDS_MAX = 3;
        private const int N_PARAMS_MAX = 128;
        private const int N_PARAMS_PER_CMD_MAX = 32;

        public ParamForm()
        {
            InitializeComponent();
            my_Initializations();
        }

        private const int HELP_LABEL_WIDTH = 50;
        private static bool readParametersFromFile = true;


        public static string[] cmdString = new string[N_COMMANDS_MAX];

        public static string[] paramVal = new string[N_PARAMS_MAX];
        private static string[] paramName = new string[N_PARAMS_MAX];
        private static string[] paramHelp = new string[N_PARAMS_MAX];

        public static int[] nParamPerCmd = new int[N_COMMANDS_MAX];
        public static int cntCmds; // needed by communication
        public static int cntParams; // needed by communication

        private const int Y0 = 25;
        private const int YOFFSET_ROW = 25;

        private const int NAME_TEXTBOX_XLOC = 10;
        private const int NAME_TEXTBOX_WIDTH = 80;
 
        private const int VAL_TEXTBOX_XLOC = NAME_TEXTBOX_XLOC + NAME_TEXTBOX_WIDTH;
        private const int VAL_TEXTBOX_WIDTH = 65;
        private const int HELP_TEXTBOX_XLOC = VAL_TEXTBOX_XLOC + VAL_TEXTBOX_WIDTH + 5;
        private const int HELP_TEXTBOX_WIDTH = 500;

        public void my_Initializations()
        {
            bool fileIsValid = true;
            paramHelpLabel.Width = HELP_LABEL_WIDTH;

            messageLabel.Visible = false;
            sendParam_button.Enabled = true;

            if (readParametersFromFile == true)
            {
                cntParams = 0;
                cntCmds = 0;
                string fileIn = "userParameters_default.txt";
                if (File.Exists(fileIn))
                {
                    StreamReader FileReader = new StreamReader(fileIn);
                    string line;

                    // Look for first line 
                    while ((line = FileReader.ReadLine()) != null && cntParams <= N_PARAMS_MAX)
                    {
                        if (line.StartsWith("%") == false && string.IsNullOrEmpty(line)==false
                            && string.IsNullOrWhiteSpace(line) == false )
                        {
                            string[] parts = line.Split(' ');

                            paramVal[cntParams] = parts[0];
                            if (parts.Length > 1)
                            {
                                int idx1 = line.IndexOf('<');
                                int idx2 = line.IndexOf('>');
                                int idx3 = line.IndexOf('%');
                                paramName[cntParams] = line.Substring(idx1 + 1, idx2 - idx1 - 1);
                                paramHelp[cntParams] = line.Substring(idx3 + 1, line.Length - idx3 - 1);
                                cntParams++;
                            }
                        }

                    }
                    if (cntParams == 0 || cntParams > N_PARAMS_MAX)
                    {
                        messageLabel.Text = "PARAMETERES FILE DOES NOT CONTAIN VALID DATA, close this window and check";
                        messageLabel.Visible = true;
                        sendParam_button.Visible = false;

                        fileIsValid = false;
                    }

                    FileReader.Close();
                    readParametersFromFile = false;
                }
                else
                {
                    messageLabel.Text = "PARAMETERES FILE IS MISSING" + Environment.NewLine + "close this window and check";
                    messageLabel.Visible = true;
                    sendParam_button.Visible = false;
                }
            }    
            if (fileIsValid == true)
            {

                int yOffset = Y0;
                int i;

                for (i = 0; i < cntParams; i++)
                {
                    // Add parameters' name label
                    Label lb = new Label();
                    lb.Name = i.ToString();
                    lb.Location = new Point(NAME_TEXTBOX_XLOC, yOffset + 2);
                    lb.Width = NAME_TEXTBOX_WIDTH;
                    lb.Text = paramName[i];
                    Controls.Add(lb);

                    // Add parameters' value textbox 
                    TextBox tb = new TextBox();
                    tb.Location = new Point(VAL_TEXTBOX_XLOC, yOffset-2);
                    tb.Width = VAL_TEXTBOX_WIDTH;
                    tb.Text = paramVal[i];
                    Controls.Add(tb);

                    // Add description label for each parameter
                    Label lb2 = new Label();
                    lb2.Name = i.ToString();
                    lb2.Location = new Point(HELP_TEXTBOX_XLOC, yOffset);
                    lb2.Width = HELP_TEXTBOX_WIDTH;
                    lb2.Text = paramHelp[i];
                    Controls.Add(lb2);

                    yOffset += YOFFSET_ROW;
                }

                sendParam_button.Visible = true;
            }  
        }

       private void sendParam_button_Click(object sender, EventArgs e)
        {
            sendParam_button.Enabled = false;

            /* Read parameters from the form */
            int i = 0;
            string validString;

            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                validString = tb.Text;
                validString = validString.TrimStart();
                validString = validString.TrimEnd();

                paramVal[i] = validString;

                i++;
                if (i>= N_PARAMS_MAX)
                    break;
            }

            messageLabel.Visible = true;
            messageLabel.Text = "Stopping the sensor...";

            messageLabel.Visible = true;
            bool retVal = communication.sendStopCommand();
            if (retVal == true)
            {
                dataAcquire.newDataAvailable = false;
                
                messageLabel.Text = "Sending parameters, please wait ...";
                messageLabel.Invalidate();
                messageLabel.Update();

                communication.updateParameters();

                messageLabel.Text = "Restarting the sensor...";
                messageLabel.Invalidate();
                messageLabel.Update();

                retVal = communication.sendStartCommand();
                if (retVal == true)
                {
                    messageLabel.Text = "Almost done ...";
                    messageLabel.Invalidate();
                    messageLabel.Update();
                    
                    while (dataAcquire.newDataAvailable == false) ;

                    messageLabel.Text = "Sensor restarted.";
                    this.Close();
                }
                else
                {
                    messageLabel.Text = "Could not restart the sensor, please retry.";
                }

            }
            else
            {
                messageLabel.Text = "Could not stop the sensor, please retry.";
            }
        }

    }



}
