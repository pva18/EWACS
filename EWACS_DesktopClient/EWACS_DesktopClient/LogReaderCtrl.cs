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
    public partial class LogReaderCtrl : UserControl
    {
        private DataTable dt;
        public LogReaderCtrl()
        {
            InitializeComponent();

            dt = new DataTable();
            dt.Columns.Add("Timestamp", typeof(DateTime));
            dt.Columns.Add("RFID UID", typeof(RfidUid));
            dt.Columns.Add("Authorized", typeof(bool));

            dataGridView1.DataSource = dt;

            copyList2DataTable();
        }

        public void UpdateView()
        {
            copyList2DataTable();
        }

        private void copyList2DataTable()
        {
            dt.Rows.Clear();

            foreach (var item in App.Instance.LogDoc.list)
            {
                dt.Rows.Add(new object[]
                {
                    item.Timestamp, item.Uid, item.IsAuthorized
                });
            }
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            App.Instance.LogDoc.list.Clear();
            copyList2DataTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            App.Instance.LogDoc.list.RemoveAt(index);

            copyList2DataTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            App.Instance.Serial.ListLogs();
        }
    }
}
