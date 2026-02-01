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
    public partial class StartWindowCtrl : UserControl
    {
        public StartWindowCtrl()
        {
            InitializeComponent();

            textBox1.Text = App.Instance.UserDocumentPath;
            textBox2.Text = App.Instance.LogDocumentPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            ofd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            ofd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            App.Instance.UserDocumentPath = textBox1.Text;
            App.Instance.UserUpdate();

            label4.Text = App.Instance.UserDoc.list.Count.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            App.Instance.LogDocumentPath = textBox2.Text;
            App.Instance.LogUpdate();

            label5.Text = App.Instance.LogDoc.list.Count.ToString();
        }
    }
}
