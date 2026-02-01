using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace EWACS_DesktopClient
{
    public class RfidUidConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }
            return RfidUid.ConvertFromString(text);
        }

        public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return RfidUid.ConvertToString((RfidUid)value);
        }
    }
}
