using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Shared.DTO.Article;
using NewsPicker.Web.Controllers;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Shared.DTO.Category;
using System.Globalization;
using NewsPicker.Shared.Models;
using System.Web;
using NewsPicker.Web.Controls;
using NewsPicker.Web.Services.Http;

namespace NewsPicker.Web.ViewModels.Articles
{
    public class ArticlesViewModelBase : NewsPicker.Web.ViewModels.LayoutViewModel
    {
        private readonly CountriesController _countriesApi = new CountriesController();

        private readonly CategoriesController _categoriesApi = new CategoriesController();

        private readonly ArticlesController _articlesApi = new ArticlesController();

        public int SelectedCountryId { get; set; }

        public int SelectedCategoryId { get; set; }

        public int SelectedTimePeriodId { get; set; } = (int)TimePeriodValue.DAY;

        public bool IsFilterVisible { get; set; } = false;

        public List<CountryDTO> Countries { get; set; }

        public List<CategoryDTO> Categories { get; set; }

        public List<ArticleDTO> Articles { get; set; }

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
            PreSelectCountry();
        }

        private void PreSelectCountry()
        {
            // Cookie value
            var selectedCountryId = Convert.ToInt32(Cookies.Get(nameof(ArticlesFilter), nameof(SelectedCountryId)));
            if (selectedCountryId != 0)
            {
                SelectedCountryId = selectedCountryId;
                return;
            }

            // Region info
            var currentRegionCountry = Countries.FirstOrDefault(c => c.Code == RegionInfo.CurrentRegion.TwoLetterISORegionName.ToLowerInvariant());
            if (currentRegionCountry != null)
            {
                SelectedCountryId = currentRegionCountry.Id;
                return;
            }
        }

        private void LoadCategories()
        {
            if (SelectedCountryId != 0)
            {
                Categories = _categoriesApi.GetCategoriesByCountryId(SelectedCountryId);
                Categories.Insert(0, new CategoryDTO() { Id = 0, Name = "All" });
            }
            else
            {
                Categories = null;
            }

            SelectedCategoryId = 0;
        }

        private void LoadArticles()
        {
            if (SelectedCategoryId != 0)
            {
                Articles = _articlesApi.GetTopArticlesByCategoryId(SelectedCategoryId, SelectedTimePeriodId);
            }
            else if (SelectedCountryId != 0)
            {
                Articles = _articlesApi.GetTopArticlesByCountryId(SelectedCountryId, SelectedTimePeriodId);
            }
            else
            {
                Articles = null;
            }
        }

        public void ApplyFilter()
        {
            LoadArticles();
            SaveFilter();
        }

        private void SaveFilter()
        {
            Cookies.Set(nameof(ArticlesFilter), nameof(SelectedCountryId), SelectedCountryId);
        }

        public void UpdateFilter()
        {
            LoadCategories();
            ApplyFilter();
        }
    }
}