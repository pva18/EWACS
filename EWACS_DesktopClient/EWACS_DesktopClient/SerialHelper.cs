using System;
using System.IO.Ports;
using System.Text;

namespace EWACS_DesktopClient
{
    public class SerialHelper
    {
        private SerialPort serial;

        public static string[] GetAvailablePortNames()
        {
            return SerialPort.GetPortNames();
        }

        public SerialHelper()
        {
            serial = new SerialPort()
            {
                BaudRate = 115200,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = 200,
                WriteTimeout = 200
            };

            serial.DataReceived += serial_DataReceived;
        }

        public string PortName
        {
            get { return serial.PortName; }
            set
            {
                if (serial.IsOpen)
                {
                    serial.Close();
                }

                try
                {
                    serial.PortName = value;
                    serial.Open();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public bool IsOpen
        {
            get { return serial.IsOpen; }
        }

        public void AddUser(User user)
        {
            if (!serial.IsOpen)
            {
                MessageBox.Show("The serial port is closed.");
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("A ");
            sb.Append(user.Uid);
            sb.Append(' ');
            sb.Append(user.Name.PadLeft(16));
            sb.Append(' ');
            sb.Append(((int)user.IntervalStart.TotalSeconds).ToString("D10"));
            sb.Append(' ');
            sb.Append(((int)user.IntervalEnd.TotalSeconds).ToString("D10"));

            sendMessage(sb.ToString());
        }

        public void RemoveUser(User user)
        {
            if (!serial.IsOpen)
            {
                MessageBox.Show("The serial port is closed.");
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("R ");
            sb.Append(user.Uid);

            sendMessage(sb.ToString());
        }

        public void ListUsers()
        {
            if (!serial.IsOpen)
            {
                MessageBox.Show("The serial port is closed.");
            }
            sendMessage("Q");
        }

        public void ListLogs()
        {
            if (!serial.IsOpen)
            {
                MessageBox.Show("The serial port is closed.");
            }
            sendMessage("L");
        }

        public void ClearLogs()
        {
            sendMessage("C");
        }

        public void UpdateTime(DateTime time)
        {
            if (!serial.IsOpen)
            {
                MessageBox.Show("The serial port is closed.");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("T ");
            DateTimeOffset dto = new DateTimeOffset(time);
            sb.Append(dto.ToUnixTimeSeconds().ToString("D10"));

            sendMessage(sb.ToString());
        }

        private StringBuilder messageBuffer = new StringBuilder(256);

        private bool messageStarted = false;

        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (serial.BytesToRead > 0)
            {
                char c;
                try
                {
                    c = (char)serial.ReadByte();
                }
                catch
                {
                    return;
                }

                if (messageStarted)
                {
                    if (c == '>')
                    {
                        messageStarted = false;
                        processMessage(messageBuffer.ToString());
                    }
                    else
                    {
                        messageBuffer.Append(c);
                    }
                }
                else
                {
                    if (c == '<')
                    {
                        messageStarted = true;
                        messageBuffer.Clear();
                    }
                }
            }
        }

        void processMessage(string message)
        {
            if (message.Length < 1)
            {
                return;
            }

            System.Diagnostics.Trace.WriteLine(message);

            int idx = message.IndexOf('\n');
            if (idx < 0)
            {
                return;
            }

            string header = message.Substring(0, idx).Trim();
            char type = header[0];
            int count = 0;
            if (!int.TryParse(header.Substring(2), out count))
            {
                return;
            }

            System.Diagnostics.Trace.WriteLine($"Type: {type}; Count: {count}");

            switch (type)
            {
                case 'Q':
                    extractUser(message.Substring(idx + 1), count);
                    break;
                case 'L':
                    extractLog(message.Substring(idx + 1), count);
                    ClearLogs();
                    break;
                default:
                    break;
            }
        }

        private void extractLog(string message, int count)
        {
            if ((count < 0) || string.IsNullOrEmpty(message))
            {
                return;
            }

            string[] lines = message.Split('\n');
            for (int i = 0; i < count; i++)
            {
                if (lines[i].Length < 33)
                {
                    continue;
                }
                string str_timestamp = lines[i].Substring(0, 10);
                string str_uid = lines[i].Substring(11, 20);
                string str_valid = lines[i].Substring(32, 1);

                try
                {
                    Log log = new Log(
                        DateTime.UnixEpoch.AddSeconds(int.Parse(str_timestamp)),
                        new RfidUid(str_uid),
                        (int.Parse(str_valid) != 0));

                    App.Instance.LogDoc.list.Add(log);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }
        }

        private void extractUser(string message, int count)
        {
            if ((count < 0) || string.IsNullOrEmpty(message))
            {
                return;
            }

            string[] lines = message.Split('\n');
            for (int i = 0; i < count; i++)
            {
                if (lines[i].Length < 59)
                {
                    continue;
                }
                string str_uid = lines[i].Substring(0, 20);
                string name = lines[i].Substring(21, 16);
                string str_intervalstart = lines[i].Substring(38, 10);
                string str_intervalend = lines[i].Substring(49, 10);

                try
                {
                    User user = new User(
                        new RfidUid(str_uid),
                        name,
                        TimeSpan.FromSeconds(int.Parse(str_intervalstart)),
                        TimeSpan.FromSeconds(int.Parse(str_intervalend)));

                    App.Instance.UserDoc.list.Add(user);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }
        }

        private void sendMessage(string message)
        {
            try
            {
                serial.WriteLine($"<{message}>");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
