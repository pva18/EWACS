using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWACS_DesktopClient
{
    public class User
    {
        public User()
        {
            Uid = new RfidUid();
            Name = string.Empty;
            IntervalStart = new TimeSpan(0, 0, 0);
            IntervalEnd = new TimeSpan(0, 0, 0);
        }

        public User(RfidUid uid, string name, TimeSpan intervalStart, TimeSpan intervalEnd)
        {
            Uid = uid;
            Name = name;
            IntervalStart = intervalStart;
            IntervalEnd = intervalEnd;
        }

        public RfidUid Uid { get; set; }
        public string Name { get; set; }
        public TimeSpan IntervalStart { get; set; }
        public TimeSpan IntervalEnd { get; set; }
    }
}
