using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWACS_DesktopClient
{
    public class App
    {
        private App()
        {
            UserDoc = new UserDocument();
            LogDoc = new LogDocument();

            Serial = new SerialHelper();
        }

        public static App Instance { get; private set; } = new App();

        public SerialHelper Serial { get; private set; }

        public UserDocument UserDoc { get; private set; }

        public LogDocument LogDoc { get; private set; }

        public string LogDocumentPath { get; set; } = System.IO.Directory.GetCurrentDirectory() + @"\log_doc.csv";

        public string UserDocumentPath { get; set; } = System.IO.Directory.GetCurrentDirectory() + @"\user_doc.csv";

        public void LogUpdate()
        {
            LogDoc = new LogDocument(LogDocumentPath);
        }

        public void UserUpdate()
        {
            UserDoc = new UserDocument(UserDocumentPath);
        }
    }
}
