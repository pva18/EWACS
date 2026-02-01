using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace EWACS_DesktopClient
{
    public class UserDocument : Document<User>
    {
        public UserDocument() : base()
        {
        }

        public UserDocument(string fileName) : base(fileName)
        {
        }

        protected override void readDocument()
        {
            try
            {
                using (StreamReader reader = new StreamReader(FileName))
                using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<UserMap>();
                    list.Clear();

                    var records = csv.GetRecords<User>();
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

        protected override void writeDocument()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<UserMap>();
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
