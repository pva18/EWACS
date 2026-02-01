namespace EWACS_DesktopClient
{
    public partial class Form1 : Form
    {
        private StartWindowCtrl startWindow = new StartWindowCtrl()
        {
            Dock = DockStyle.Fill,
        };
        private UserEditorCtrl userEditor = new UserEditorCtrl()
        {
            Dock = DockStyle.Fill,
        };
        private LogReaderCtrl logReader = new LogReaderCtrl()
        {
            Dock = DockStyle.Fill,
        };
        private UsageViewerCtrl usageViewer = new UsageViewerCtrl()
        {
            Dock = DockStyle.Fill,
        };
        private TimeUpdaterCtrl timeUpdater = new TimeUpdaterCtrl()
        {
            Dock = DockStyle.Fill,
        };

        private UserControl lastPage;

        public Form1()
        {
            InitializeComponent();

            ResizeRedraw = true;

            this.Controls.Add(startWindow);
            lastPage = startWindow;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            lastPage.Invalidate();
        }

        private void changePage(UserControl newPage)
        {
            Controls.Remove(lastPage);
            Controls.Add(newPage);
            lastPage = newPage;
        }

        private void startWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePage(startWindow);
        }

        private void userEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePage(userEditor);
            userEditor.UpdateView();
        }

        private void logReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePage(logReader);
            logReader.UpdateView();
        }

        private void usageViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePage(usageViewer);
            usageViewer.UpdateView();
        }

        private void timeUpdaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePage(timeUpdater);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.UserDoc.SaveDocument();
            App.Instance.LogDoc.SaveDocument();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void serialPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerialPortSelectForm form = new SerialPortSelectForm();
            form.PortList = SerialHelper.GetAvailablePortNames();

            if (form.ShowDialog() == DialogResult.OK)
            {
                App.Instance.Serial.PortName = form.SelectedPort;
            }
        }
    }
}