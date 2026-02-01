using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace EWACS_DesktopClient
{
    public sealed class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Map(m => m.Uid).Index(0).Name("uid");
            Map(m => m.Uid).TypeConverter<RfidUidConverter>();
            Map(m => m.Name).Index(1).Name("name");
            Map(m => m.IntervalStart).Index(2).Name("intervalstart");
            Map(m => m.IntervalEnd).Index(3).Name("intervalend");
        }
    }
}
