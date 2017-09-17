using NewsPicker.Shared.Models;
using NewsPicker.Web.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NewsPicker.Web.Models.ArticlesFilter
{
    public class TimePeriod
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TimePeriod()
        { }

        public TimePeriod(TimePeriodValue value, string name)
        {
            Id = (int)value;
            Name = name;
        }
    }
}