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
    public partial class AddUserForm : Form
    {
        private RfidUid uid = new RfidUid();
        private string userName = string.Empty;
        private DateTime intervalStart;
        private DateTime intervalEnd;

        public AddUserForm()
        {
            InitializeComponent();

            dateTimePicker1.CustomFormat = "HH:mm";
            dateTimePicker2.CustomFormat = "HH:mm";
        }

        public RfidUid Uid
        {
            get { return uid; }
            set
            {
                uid = value;
                numericUpDown1.Value = uid[0];
                numericUpDown2.Value = uid[1];
                numericUpDown3.Value = uid[2];
                numericUpDown4.Value = uid[3];
                numericUpDown5.Value = uid[4];
                numericUpDown6.Value = uid[5];
                numericUpDown7.Value = uid[6];
                numericUpDown8.Value = uid[7];
                numericUpDown9.Value = uid[8];
                numericUpDown10.Value = uid[9];
            }
        }

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                textBox2.Text = userName;
            }
        }

        public DateTime IntervalStart
        {
            get { return intervalStart; }
            set
            {
                intervalStart = value;
                dateTimePicker1.Value = intervalStart;
            }
        }

        public DateTime IntervalEnd
        {
            get { return intervalEnd; }
            set
            {
                intervalEnd = value;
                dateTimePicker2.Value = intervalEnd;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uid = new RfidUid(new byte[]
            {
                (byte)numericUpDown1.Value,
                (byte)numericUpDown2.Value,
                (byte)numericUpDown3.Value,
                (byte)numericUpDown4.Value,
                (byte)numericUpDown5.Value,
                (byte)numericUpDown6.Value,
                (byte)numericUpDown7.Value,
                (byte)numericUpDown8.Value,
                (byte)numericUpDown9.Value,
                (byte)numericUpDown10.Value,
            },
            10);
            userName = new string(textBox2.Text);
            intervalStart = dateTimePicker1.Value;
            intervalEnd = dateTimePicker2.Value;

            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
