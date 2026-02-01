using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace EWACS_DesktopClient
{
    public partial class UsageViewerCtrl : UserControl
    {
        private enum GraphTime
        {
            HOUR,
            DAY,
            WEEK,
            MONTH,
            YEAR
        }

        private GraphTime graphTime;
        private int graphColumnCount = 5;

        private struct GraphColumn
        {
            public int size;
            public DateTime time;
        }

        private GraphColumn[] graphColumns;
        private int graphTotalSize = 0;

        private static readonly int graphWidthMin = 100;
        private static readonly int graphHeightMin = 100;
        private static readonly int graphMargin = 10;
        private int graphOffsetX = graphMargin;
        private int graphOffsetY = graphMargin;

        public UsageViewerCtrl()
        {
            InitializeComponent();
            this.Paint += paintGraph;

            graphOffsetY = groupBox1.Bottom + graphMargin;

            comboBox1.SelectedIndex = 0;
            numericUpDown1.Value = graphColumnCount;

            button1.Click += buttonSave;

            graphColumns = new GraphColumn[graphColumnCount];
            DateTime currentTime = DateTime.Now;

            createGraphColumns();
            Invalidate();
        }

        public void UpdateView()
        {
            createGraphColumns();
        }

        private void buttonSave(object? sender, EventArgs e)
        {
            graphColumnCount = (int)(numericUpDown1.Value);

            string time = (string)comboBox1.Items[comboBox1.SelectedIndex];

            if (time != null)
            {
                if (time == "HOUR")
                {
                    graphTime = GraphTime.HOUR;
                }
                else if (time == "DAY")
                {
                    graphTime = GraphTime.DAY;
                }
                else if (time == "WEEK")
                {
                    graphTime = GraphTime.WEEK;
                }
                else if (time == "MONTH")
                {
                    graphTime = GraphTime.MONTH;
                }
                else if (time == "YEAR")
                {
                    graphTime = GraphTime.YEAR;
                }
            }

            createGraphColumns();

            Invalidate();
        }

        private void paintGraph(object? sender, PaintEventArgs e)
        {
            int graphWidth = Size.Width - graphOffsetX - graphMargin;
            if (graphWidth < graphWidthMin)
            {
                graphWidth = graphWidthMin;
            }
            int graphHeight = Size.Height - graphOffsetY - graphMargin;
            if (graphHeight < graphHeightMin)
            {
                graphHeight = graphHeightMin;
            }

            // Background
            e.Graphics.FillRectangle(
                Brushes.Black,
                graphOffsetX, graphOffsetY,
                graphWidth, graphHeight);

            // Axes
            e.Graphics.DrawLine(
                Pens.GreenYellow,
                new Point(10 + 20, graphOffsetY),
                new Point(10 + 20, graphOffsetY + graphHeight));
            e.Graphics.DrawLine(
                Pens.GreenYellow,
                new Point(10, graphOffsetY + graphHeight - 20),
                new Point(10 + graphWidth, graphOffsetY + graphHeight - 20));

            // Data
            int columnWidth = (graphWidth - 20) / graphColumnCount;

            for (int i = 0; i < graphColumnCount; i++)
            {
                if (graphColumns[i].size == 0)
                {
                    e.Graphics.DrawString(graphColumns[i].size.ToString(),
                        this.Font,
                        Brushes.HotPink,
                        10 + 20 + 10 + i * columnWidth,
                        graphOffsetY + graphHeight - 40);
                }
                else
                {
                    int columnHeight = (graphHeight - 20 - 20) * graphColumns[i].size / graphTotalSize;
                    e.Graphics.FillRectangle(
                        Brushes.GreenYellow,
                        10 + 20 + 10 + i * columnWidth, graphOffsetY + graphHeight - 20 - columnHeight,
                        columnWidth - 10, columnHeight);

                    e.Graphics.DrawString(graphColumns[i].size.ToString(),
                        this.Font,
                        Brushes.HotPink,
                        10 + 20 + 10 + i * columnWidth,
                        graphOffsetY + graphHeight - 20 - columnHeight - 20);
                }

                e.Graphics.DrawString(graphColumns[i].time.ToString(getDateStringFormat(graphTime)),
                    this.Font,
                    Brushes.HotPink,
                    10 + 20 + 10 + i * columnWidth,
                    graphOffsetY + graphHeight - 20);
            }
        }

        private void createGraphColumns()
        {
            graphColumns = new GraphColumn[graphColumnCount];

            DateTime currentTime = DateTime.Now;

            for (int i = 0; i < graphColumnCount; i++)
            {
                graphColumns[i].size = 0;
                graphColumns[i].time = getColumnTime(graphTime, currentTime, i);
            }

            foreach (Log log in App.Instance.LogDoc.list)
            {
                for (int i = 0; i < graphColumnCount; i++)
                {
                    if (compareGraphTime(graphTime, log.Timestamp, graphColumns[i].time))
                    {
                        graphColumns[i].size++;
                        break;
                    }
                }
            }

            graphTotalSize = 0;
            foreach (var column in graphColumns)
            {
                graphTotalSize += column.size;
            }
        }

        private string getDateStringFormat(GraphTime graphTime)
        {
            return graphTime switch
            {
                GraphTime.HOUR => "yyyy/MM/dd HH:mm",
                GraphTime.DAY => "yyyy/MM/dd",
                GraphTime.WEEK => "yyyy/MM/dd",
                GraphTime.MONTH => "yyyy/MM",
                GraphTime.YEAR => "yyyy",
                _ => "yyyy/MM/dd HH:mm:ss",
            };
        }

        private static DateTime getColumnTime(GraphTime graphTime, DateTime time, int sub)
        {
            return graphTime switch
            {
                GraphTime.HOUR => (new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0)).AddHours(-sub),
                GraphTime.DAY => (new DateTime(time.Year, time.Month, time.Day, 0, 0, 0)).AddDays(-sub),
                GraphTime.WEEK => (new DateTime(time.Year, time.Month, time.Day, 0, 0, 0)).Subtract(
                    new TimeSpan((((int)time.DayOfWeek == 0) ? 7 : (int)time.DayOfWeek) - (sub * 7), 0, 0, 0)),
                GraphTime.MONTH => (new DateTime(time.Year, time.Month, 1)).AddMonths(-sub),
                GraphTime.YEAR => new DateTime(time.Year, 1, 1).AddYears(-sub),
                _ => new DateTime(),
            };
        }

        private static bool compareGraphTime(GraphTime gt, DateTime t1, DateTime t2)
        {
            return gt switch
            {
                GraphTime.HOUR => ((t2 - t1).TotalHours < 1),
                GraphTime.DAY => ((t2 - t1).TotalDays < 1),
                GraphTime.WEEK => ((t2 - t1).TotalDays < 7),
                GraphTime.MONTH => (t1.Year == t2.Year) && (t1.Month == t2.Month),
                GraphTime.YEAR => (t1.Year == t2.Year),
                _ => false,
            };
        }
    }
}
