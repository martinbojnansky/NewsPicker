using NewsPicker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Web.Models.ArticlesFilter
{
    public class TimePeriod
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TimePeriod()
        {
        }

        public TimePeriod(TimePeriodValue value, string name)
        {
            Id = (int)value;
            Name = name;
        }

        public static List<TimePeriod> All()
        {
            const string HOURS_SUFFIX = "Hours";
            const string DAY_SUFFIX = "Day";
            const string DAYS_SUFFIX = "Days";
            const string WEEK_SUFFIX = "Week";

            return new List<TimePeriod>()
            {
                new TimePeriod(TimePeriodValue.TWELVE_HOURS, $"12 {HOURS_SUFFIX}"),
                new TimePeriod(TimePeriodValue.DAY, $"1 {DAY_SUFFIX}"),
                new TimePeriod(TimePeriodValue.THREE_DAYS, $"3 {DAYS_SUFFIX}"),
                new TimePeriod(TimePeriodValue.WEEK, $"1 {WEEK_SUFFIX}")
            };
        }
    }
}