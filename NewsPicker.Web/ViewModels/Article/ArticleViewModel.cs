using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace NewsPicker.Web.ViewModels.Article
{
    public class ArticleViewModel : NewsPicker.Web.ViewModels.LayoutViewModel
    {
        public const string URL_QUERY_KEY = "url";
        public string Url { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                LoadUrl();
            }

            return base.PreRender();
        }

        private void LoadUrl()
        {
            Url = Context.Query[URL_QUERY_KEY];
        }
    }
}