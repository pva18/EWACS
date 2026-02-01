namespace EWACS_DesktopClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            serialPortToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            startWindowToolStripMenuItem = new ToolStripMenuItem();
            userEditorToolStripMenuItem = new ToolStripMenuItem();
            logReaderToolStripMenuItem = new ToolStripMenuItem();
            usageViewerToolStripMenuItem = new ToolStripMenuItem();
            timeUpdaterToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, optionsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(784, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { serialPortToolStripMenuItem, saveToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // serialPortToolStripMenuItem
            // 
            serialPortToolStripMenuItem.Name = "serialPortToolStripMenuItem";
            serialPortToolStripMenuItem.Size = new Size(180, 22);
            serialPortToolStripMenuItem.Text = "Serial Port";
            serialPortToolStripMenuItem.Click += serialPortToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(180, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { startWindowToolStripMenuItem, userEditorToolStripMenuItem, logReaderToolStripMenuItem, usageViewerToolStripMenuItem, timeUpdaterToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // startWindowToolStripMenuItem
            // 
            startWindowToolStripMenuItem.Name = "startWindowToolStripMenuItem";
            startWindowToolStripMenuItem.Size = new Size(145, 22);
            startWindowToolStripMenuItem.Text = "Start Window";
            startWindowToolStripMenuItem.Click += startWindowToolStripMenuItem_Click;
            // 
            // userEditorToolStripMenuItem
            // 
            userEditorToolStripMenuItem.Name = "userEditorToolStripMenuItem";
            userEditorToolStripMenuItem.Size = new Size(145, 22);
            userEditorToolStripMenuItem.Text = "User Editor";
            userEditorToolStripMenuItem.Click += userEditorToolStripMenuItem_Click;
            // 
            // logReaderToolStripMenuItem
            // 
            logReaderToolStripMenuItem.Name = "logReaderToolStripMenuItem";
            logReaderToolStripMenuItem.Size = new Size(145, 22);
            logReaderToolStripMenuItem.Text = "Log Reader";
            logReaderToolStripMenuItem.Click += logReaderToolStripMenuItem_Click;
            // 
            // usageViewerToolStripMenuItem
            // 
            usageViewerToolStripMenuItem.Name = "usageViewerToolStripMenuItem";
            usageViewerToolStripMenuItem.Size = new Size(145, 22);
            usageViewerToolStripMenuItem.Text = "Usage Viewer";
            usageViewerToolStripMenuItem.Click += usageViewerToolStripMenuItem_Click;
            // 
            // timeUpdaterToolStripMenuItem
            // 
            timeUpdaterToolStripMenuItem.Name = "timeUpdaterToolStripMenuItem";
            timeUpdaterToolStripMenuItem.Size = new Size(145, 22);
            timeUpdaterToolStripMenuItem.Text = "Time Updater";
            timeUpdaterToolStripMenuItem.Click += timeUpdaterToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 361);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem serialPortToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem startWindowToolStripMenuItem;
        private ToolStripMenuItem userEditorToolStripMenuItem;
        private ToolStripMenuItem logReaderToolStripMenuItem;
        private ToolStripMenuItem usageViewerToolStripMenuItem;
        private ToolStripMenuItem timeUpdaterToolStripMenuItem;
    }
}