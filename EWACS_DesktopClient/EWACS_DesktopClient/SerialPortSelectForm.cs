using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EWACS_DesktopClient
{
    public partial class SerialPortSelectForm : Form
    {
        private string[] portList = new string[1];

        public SerialPortSelectForm()
        {
            InitializeComponent();
        }

        public string[] PortList
        {
            get { return portList; }
            set
            {
                portList = value;
                comboBox1.DataSource = portList;
            }
        }

        public string SelectedPort { get; private set; } = string.Empty;

        private void button1_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (portList.Length < 1)
            {
                SelectedPort = string.Empty;
            }
            else
            {
                SelectedPort = portList[index];
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
