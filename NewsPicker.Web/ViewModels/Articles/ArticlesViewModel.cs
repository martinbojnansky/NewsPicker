using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using NewsPicker.Shared.DTO.Article;
using NewsPicker.Web.Controllers;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Shared.DTO.Category;

namespace NewsPicker.Web.ViewModels.Articles
{
    public class ArticlesViewModel : NewsPicker.Web.ViewModels.LayoutViewModel
    {
        private CountriesController _countriesApi = new CountriesController();
        private CategoriesController _categoriesApi = new CategoriesController();
        private ArticlesController _articlesApi = new ArticlesController();

        public List<CountryDTO> Countries { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<ArticleDTO> Articles { get; set; } = new List<ArticleDTO>();

        public bool IsFilterVisible { get; set; } = false;

        public override Task PreRender()
        {
            Countries = _countriesApi.GetCountries();
            Categories = _categoriesApi.GetCategoriesByCountryId(1);
            Articles = _articlesApi.GetTopArticlesByCountryId(1);

            return base.PreRender();
        }

        public void RedirectToUrl(string url)
        {
            Context.RedirectToUrl(url);
        }
    }
}