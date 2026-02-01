using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWACS_DesktopClient
{
    public class Log
    {
        public Log()
        {
            Timestamp = new DateTime();
            Uid = new RfidUid();
        }

        public Log(DateTime timestamp, RfidUid uid, bool isAuthorized)
        {
            Uid = uid;
            Timestamp = timestamp;
            IsAuthorized = isAuthorized;
        }

        public DateTime Timestamp { get; set; }
        public RfidUid Uid { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
