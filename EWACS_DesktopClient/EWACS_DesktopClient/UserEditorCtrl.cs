using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace EWACS_DesktopClient
{
    public partial class UserEditorCtrl : UserControl
    {
        private DataTable dt;

        public UserEditorCtrl()
        {
            InitializeComponent();

            dt = new DataTable();
            dt.Columns.Add("RFID UID");
            dt.Columns.Add("User Name");
            dt.Columns.Add("Interval Start");
            dt.Columns.Add("Interval End");
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

            foreach (var item in App.Instance.UserDoc.list)
            {
                dt.Rows.Add(new object[]
                {
                    item.Uid, item.Name, item.IntervalStart, item.IntervalEnd
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm();

            if (addUserForm.ShowDialog() == DialogResult.OK)
            {
                User user = new User(
                    addUserForm.Uid,
                    addUserForm.UserName,
                    new TimeSpan(addUserForm.IntervalStart.Hour, addUserForm.IntervalStart.Minute, 0),
                    new TimeSpan(addUserForm.IntervalEnd.Hour, addUserForm.IntervalEnd.Minute, 0));

                App.Instance.UserDoc.list.Add(user);

                App.Instance.Serial.AddUser(user);
            }

            copyList2DataTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;

            AddUserForm editUserForm = new AddUserForm()
            {
                Uid = App.Instance.UserDoc.list[index].Uid,
                UserName = App.Instance.UserDoc.list[index].Name,
                IntervalStart = new DateTime(
                    2000, 1, 1,
                    App.Instance.UserDoc.list[index].IntervalStart.Hours,
                    App.Instance.UserDoc.list[index].IntervalStart.Minutes,
                    0),
                IntervalEnd = new DateTime(
                    2000, 1, 1,
                    App.Instance.UserDoc.list[index].IntervalEnd.Hours,
                    App.Instance.UserDoc.list[index].IntervalEnd.Minutes,
                    0),
            };

            if (editUserForm.ShowDialog() == DialogResult.OK)
            {
                App.Instance.UserDoc.list[index].Uid = editUserForm.Uid;
                App.Instance.UserDoc.list[index].Name = editUserForm.UserName;
                App.Instance.UserDoc.list[index].IntervalStart = new TimeSpan(
                    editUserForm.IntervalStart.Hour,
                    editUserForm.IntervalStart.Minute,
                    0);
                App.Instance.UserDoc.list[index].IntervalEnd = new TimeSpan(
                    editUserForm.IntervalEnd.Hour,
                    editUserForm.IntervalEnd.Minute,
                    0);

                App.Instance.Serial.AddUser(App.Instance.UserDoc.list[index]);
            }

            copyList2DataTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;

            App.Instance.Serial.RemoveUser(App.Instance.UserDoc.list[index]);

            App.Instance.UserDoc.list.RemoveAt(index);

            copyList2DataTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!App.Instance.Serial.IsOpen)
            {
                MessageBox.Show("The serial port is closed.", "Error");
                return;
            }

            foreach (var user in App.Instance.UserDoc.list)
            {
                App.Instance.Serial.AddUser(user);
            }
        }
    }
}
