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
        public int Id { get; private set; }
        public string Name { get; set; }

        public TimePeriod(TimePeriodValue value, string name)
        {
            Id = (int)value;
            Name = name;
        }
    }
}