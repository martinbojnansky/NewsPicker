using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using NewsPicker.Shared.DTO.Article;
using NewsPicker.Web.Controllers;

namespace NewsPicker.Web.ViewModels.Articles
{
    public class ArticlesViewModel : NewsPicker.Web.ViewModels.LayoutViewModel
    {
        public List<ArticleDTO> Articles { get; set; } = new List<ArticleDTO>();
        private ArticlesController _articlesApi = new ArticlesController();

        public override Task PreRender()
        {
            Articles = _articlesApi.GetTopArticlesByCountryId(1);

            return base.PreRender();
        }

        public void RedirectToUrl(string url)
        {
            Context.RedirectToUrl(url);
        }
    }
}