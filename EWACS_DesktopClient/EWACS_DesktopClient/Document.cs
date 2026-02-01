using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace EWACS_DesktopClient
{
    public class Document<T>
    {
        public List<T> list = new List<T>();

        private object syncObjectSave = new object();

        public Document()
        {
        }

        public Document(string fileName)
        {
            FileName = fileName;

            lock (syncObjectSave)
            {
                readDocument();
            }
        }

        public string FileName { get; private set; } = string.Empty;

        public void SaveDocument()
        {
            if (FileName == string.Empty)
            {
                MessageBox.Show("File name not selected!", "Error");
                return;
            }

            Thread saveThread = new Thread(new ThreadStart(() =>
            {
                lock (syncObjectSave)
                {
                    writeDocument();
                }
            }));
            saveThread.Start();
        }

        protected virtual void readDocument()
        {
            try
            {
                using (StreamReader reader = new StreamReader(FileName))
                using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list.Clear();

                    var records = csv.GetRecords<T>();
                    foreach (var record in records)
                    {
                        list.Add(record);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                MessageBox.Show(ex.Message, "Error");
            }
            catch (CsvHelper.HeaderValidationException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                MessageBox.Show("Input file has an invalid format: " + ex.Message, "Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                MessageBox.Show(ex.Message, "Error");
            }
        }

        protected virtual void writeDocument()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(list);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
