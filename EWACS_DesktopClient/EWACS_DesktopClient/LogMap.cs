using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace EWACS_DesktopClient
{
    public sealed class LogMap : ClassMap<Log>
    {
        public LogMap()
        {
            Map(m => m.Timestamp).Index(0).Name("timestamp");
            Map(m => m.Uid).Index(1).Name("uid");
            Map(m => m.Uid).TypeConverter<RfidUidConverter>();
            Map(m => m.IsAuthorized).Index(2).Name("auth");
        }
    }
}
