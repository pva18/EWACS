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
    public partial class TimeUpdaterCtrl : UserControl
    {
        public TimeUpdaterCtrl()
        {
            InitializeComponent();

            label1.Text = DateTime.Now.ToString();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            App.Instance.Serial.UpdateTime(new DateTime(
                dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day,
                dateTimePicker2.Value.Hour, dateTimePicker2.Value.Minute, dateTimePicker2.Value.Second));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            App.Instance.Serial.UpdateTime(DateTime.Now);
        }
    }
}
