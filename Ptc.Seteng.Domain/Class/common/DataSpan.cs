using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class DateSpan
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public DateSpan()
        {

        }

        public DateSpan(DateTime? start, DateTime? end)
        {
            Start = start;
            End = end;
        }

        public DateSpan(string start, string end)
        {
            DateTime currentStart = new DateTime();
            DateTime currentEnd = new DateTime();

            if (!string.IsNullOrEmpty(start) && DateTime.TryParse(start, out currentStart))
            {
                Start = currentStart;
            }
            else
            {
                throw new ArgumentException(nameof(start) + " is not a date string");
            }


            if (!string.IsNullOrEmpty(end) && DateTime.TryParse(end, out currentEnd))
            {
                End = currentEnd;
            }
            else
            {
                throw new ArgumentException(nameof(end) + " is not a date string");
            }

            if (currentStart > currentEnd)
                throw new ArgumentException(nameof(start) + " shouldn't later than " + nameof(end));

        }

        public DateSpan(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentException(nameof(start) + " shouldn't later than " + nameof(end));

            Start = start;
            End = end;
        }

        public string ToString(string format)
        {
            if (Start.HasValue == false || End.HasValue == false)
                return "尚未定義日期";

            return Start.Value.ToString(format) + " - " + End.Value.ToString(format);
        }

        public DateTime? FormatDateTime(string dateTime)
        {
            DateTime currentDate = new DateTime();


            if (!string.IsNullOrEmpty(dateTime) && DateTime.TryParse(dateTime, out currentDate))
            {
                return currentDate;
            }
            else
            {
                return null;
            }
           

        }
    }

}
