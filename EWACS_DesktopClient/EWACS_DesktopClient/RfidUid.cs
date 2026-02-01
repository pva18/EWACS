using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EWACS_DesktopClient
{
    public class RfidUid
    {
        private byte[] bytes = new byte[10];

        private int size;

        public RfidUid() { }

        public RfidUid(byte[] bytes, int size)
        {
            for (int i = 0; (i < size) || (i < 10); i++)
            {
                this.bytes[i] = bytes[i];
            }
            this.size = size;
        }

        public RfidUid(string uid)
        {
            uid = uid.Trim();
            size = uid.Length / 2;

            try
            {
                // If there is an uneven number of characters in the string, skip the first character
                for (int i = uid.Length % 2; i < size; i++)
                {
                    bytes[i] = byte.Parse(uid.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
            }
            catch (ArgumentException ex)
            {
                size = 0;
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        public int Size
        {
            get { return size; }
        }

        public byte this[int i]
        {
            get { return bytes[i]; }
            set { bytes[i] = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 10 - size; i++)
            {
                sb.Append("00");
            }

            sb.Append(Convert.ToHexString(bytes, 0, size));

            return sb.ToString();
        }

        public static string ConvertToString(RfidUid uid)
        {
            return uid.ToString();
        }

        public static RfidUid ConvertFromString(string str)
        {
            return new RfidUid(str);
        }
    }
}
