using NewsPicker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Shared.Constants
{
    public class ArticleConstants
    {
        public const TimePeriodValue MaxArticleAge = TimePeriodValue.WEEK;
        public static DateTime MinArticleCreatedDate => DateTime.UtcNow.AddHours(-(int)MaxArticleAge);
    }
}