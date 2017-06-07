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
using System.Globalization;
using NewsPicker.Shared.Extensions;

namespace NewsPicker.Web.ViewModels.Articles
{
    public class ArticlesViewModel : NewsPicker.Web.ViewModels.LayoutViewModel
    {
        private readonly CountriesController _countriesApi = new CountriesController();
        private readonly CategoriesController _categoriesApi = new CategoriesController();
        private readonly ArticlesController _articlesApi = new ArticlesController();

        public List<CountryDTO> Countries { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<ArticleDTO> Articles { get; set; } = new List<ArticleDTO>();

        public CountryDTO SelectedCountry { get; set; }
        public CategoryDTO SelectedCategory { get; set; }

        public bool IsFilterVisible { get; set; } = false;

        public override Task PreRender()
        {
            LoadData();
            return base.PreRender();
        }

        public void LoadData()
        {
            LoadCountries();
            LoadCategories();
            LoadArticles();
        }

        private void LoadCountries()
        {
            Countries = _countriesApi.GetCountries();

            SelectedCountry = Countries.FirstOrDefault(c => c.Code == "sk");
            //if (SelectedCountry == null)
            //{
            //    SelectedCountry = Countries.FirstOrDefault(c => c.Code == RegionInfo.CurrentRegion.TwoLetterISORegionName.ToLowerInvariant());
            //}
        }

        private void LoadCategories()
        {
            Categories = _categoriesApi.GetCategoriesByCountryId(SelectedCountry.Id);
        }

        private void LoadArticles()
        {
            if (SelectedCategory != null)
            {
                Articles = _articlesApi.GetTopArticlesByCategoryId(SelectedCategory.Id);
            }
            else
            {
                Articles = _articlesApi.GetTopArticlesByCountryId(SelectedCountry.Id);
            }
        }

        public void ApplyFilter()
        {
            LoadArticles();
        }

        public string GetElapsedTimeString(DateTime? date) => date?.ToElapsedTimeString();
    }
}