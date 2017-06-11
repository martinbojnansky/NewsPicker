using NewsPicker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPicker.Web.Models.ArticlesFilter
{
    public static class TimePeriods
    {
        private const string HOURS_SUFFIX = "Hours";
        private const string DAY_SUFFIX = "Day";
        private const string DAYS_SUFFIX = "Days";
        private const string WEEK_SUFFIX = "Week";

        public static List<TimePeriod> GetDefault()
        {
            return new List<TimePeriod>()
            {
                new TimePeriod(TimePeriodValue.SIX_HOURS, $"6 {HOURS_SUFFIX}"),
                new TimePeriod(TimePeriodValue.TWELVE_HOURS, $"12 {HOURS_SUFFIX}"),
                new TimePeriod(TimePeriodValue.DAY, $"1 {DAY_SUFFIX}"),
                new TimePeriod(TimePeriodValue.THREE_DAYS, $"3 {DAYS_SUFFIX}"),
                new TimePeriod(TimePeriodValue.WEEK, $"1 {WEEK_SUFFIX}")
            };
        }
    }
}