using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToElapsedTimeString(this DateTime datetime)
        {
            var timeSpan = DateTime.Now.Subtract(datetime);
            var date = DateTime.MinValue + timeSpan;

            return ProcessPeriod(date.Year - 1, "year")
                   ?? ProcessPeriod(date.Month - 1, "month")
                   ?? ProcessPeriod(date.Day - 1, "day", "Yesterday")
                   ?? ProcessPeriod(date.Hour, "hour")
                   ?? ProcessPeriod(date.Minute, "minute")
                   ?? ProcessPeriod(date.Second, "second")
                   ?? "Right now";
        }

        private static string ProcessPeriod(int value, string name, string singularName = null)
        {
            switch (value)
            {
                case 0:
                {
                    return null;
                }
                case 1:
                {
                    if (!string.IsNullOrEmpty(singularName))
                    {
                        return singularName;
                    }

                    return $"{value} {name} ago";
                }
                default:
                {
                    return $"{value} {name}s ago";
                }
            }
        }
    }
}
