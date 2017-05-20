using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using NewsPicker.Database.Controllers;
using NewsPicker.Robot.Services.Automation;

namespace NewsPicker.Robot
{
    public class Functions
    {
        [NoAutomaticTrigger]
        public static void DeleteOldArticles()
        {
            ArticlesController articles = new ArticlesController();
            articles.DeleteOld();
        }

        [NoAutomaticTrigger]
        public static void UpdateArticles()
        {
            ArticlesClient articles = new ArticlesClient();
            articles.UpdateAsync().Wait();
        }

        [NoAutomaticTrigger]
        public static void UpdateShareCount()
        {
            ShareCountClient shares = new ShareCountClient();
            shares.UpdateAsync().Wait();
        }
    }
}
