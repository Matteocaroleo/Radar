using System;
using System.Collections.Generic; // For List

using System.Windows.Forms;
using System.Threading;

namespace VitalSignsGui
{
    public partial class startForm : Form
    {
        private static List<string> portList;
        public startForm()
        {
            InitializeComponent();

            portList = communication.InitializeComPort();
            portCombobox.DataSource = portList;
            if (portList.Count <= 0)
                noComPort_label.Visible = true;

        }

        private void start_button_Click(object sender, EventArgs e)
        {
            dataAcquire.dataFromBoard = realTime_radioButton.Checked;
            if (realTime_radioButton.Checked == true)
            {
                int retVal = communication.openPorts(portCombobox.SelectedIndex);
                if (retVal != 0) {

                    string errorMessage = " ";
                    if (retVal == -1)
                        errorMessage = string.Format("Problems opening data port");
                    else if (retVal == -2)
                        errorMessage = string.Format("Problems opening command port");

                    MessageBox.Show(errorMessage, "Fill data", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
  
            }

            // Open new thread for data download 
            Thread dataThread = new Thread(() => dataAcquire.readData());
            dataThread.SetApartmentState(ApartmentState.STA); // Necessary if loading data from file
            dataThread.Start();

            chartsForm form = new chartsForm();
            form.Show();
        }
    }
}
