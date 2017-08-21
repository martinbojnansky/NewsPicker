using NewsPicker.Shared.Models;
using NewsPicker.Web.Resources.Localization;
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
            return new List<TimePeriod>()
            {
                new TimePeriod(TimePeriodValue.TWELVE_HOURS, $"12 {LocalizedStringResources.HoursOptionText}"),
                new TimePeriod(TimePeriodValue.DAY, $"1 {LocalizedStringResources.DayOptionText}"),
                new TimePeriod(TimePeriodValue.THREE_DAYS, $"3 {LocalizedStringResources.DaysOptionText}"),
                new TimePeriod(TimePeriodValue.WEEK, $"1 {LocalizedStringResources.WeekOptionText}")
            };
        }
    }
}