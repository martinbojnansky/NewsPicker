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

        public int SelectedCountryId { get; set; }
        public int SelectedCategoryId { get; set; }

        public bool IsFilterVisible { get; set; } = false;

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                LoadData();
            }

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

            if (SelectedCountryId == 0)
            {
                SelectedCountryId = Countries.FirstOrDefault(c => c.Code == RegionInfo.CurrentRegion.TwoLetterISORegionName.ToLowerInvariant()).Id;
            }
        }

        private void LoadCategories()
        {
            Categories = _categoriesApi.GetCategoriesByCountryId(SelectedCountryId);
            Categories.Insert(0, new CategoryDTO() { Id = 0, Name = "All" });

            SelectedCategoryId = 0;
        }

        private void LoadArticles()
        {
            if (SelectedCategoryId != 0)
            {
                Articles = _articlesApi.GetTopArticlesByCategoryId(SelectedCategoryId);
            }
            else
            {
                Articles = _articlesApi.GetTopArticlesByCountryId(SelectedCountryId);
            }
        }

        public void ApplyFilter(bool loadCategories = false)
        {
            if (loadCategories)
            {
                LoadCategories();
            }

            LoadArticles();
        }
    }
}