using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Shared.DTO.Article;
using NewsPicker.Web.Facades;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Shared.DTO.Category;
using System.Globalization;
using NewsPicker.Shared.Models;
using System.Web;
using NewsPicker.Web.Controls;
using NewsPicker.Web.Services.Http;
using NewsPicker.Web.Models.ArticlesFilter;

namespace NewsPicker.Web.ViewModels.Articles
{
    public class ArticlesViewModelBase : NewsPicker.Web.ViewModels.LayoutViewModel
    {
        private readonly CountriesFacade _countriesFacade = new CountriesFacade();

        private readonly CategoriesFacade _categoriesFacade = new CategoriesFacade();

        private readonly ArticlesFacade _articlesFacade = new ArticlesFacade();

        public int SelectedCountryId { get; set; }

        public int SelectedCategoryId { get; set; }

        public int SelectedTimePeriodId { get; set; } = (int)TimePeriodValue.DAY;

        public bool IsFilterVisible { get; set; }

        public string FilterButtonText { get; set; }

        public List<CountryDTO> Countries { get; set; }

        public List<CategoryDTO> Categories { get; set; }

        public List<ArticleDTO> Articles { get; set; }

        public List<TimePeriod> TimePeriods { get; set; } = Models.ArticlesFilter.TimePeriods.GetDefault();

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                LoadFilterValues();
                LoadData();
                UpdateFilterButtonText();
            }

            return base.PreRender();
        }

        private void LoadFilterValues()
        {
            LoadSelectedCountry();
        }

        public void LoadData()
        {
            LoadCountries();
            LoadCategories();
            LoadArticles();
        }

        private void LoadSelectedCountry()
        {
            var selectedCountryId = Convert.ToInt32(Cookies.Get(nameof(ArticlesFilter), nameof(SelectedCountryId)));
            if (selectedCountryId != 0)
            {
                SelectedCountryId = selectedCountryId;
                return;
            }

            SelectedCountryId = 1;
            IsFilterVisible = true;
        }

        private void LoadCountries()
        {
            Countries = _countriesFacade.GetCountries();
            LoadSelectedCountry();
        }

        private void LoadCategories()
        {
            if (SelectedCountryId != 0)
            {
                Categories = _categoriesFacade.GetCategoriesByCountryId(SelectedCountryId);
                Categories.Insert(0, new CategoryDTO() { Id = 0, Name = "All" });
            }
            else
            {
                Categories = null;
            }
        }

        private void LoadArticles()
        {
            if (SelectedCategoryId != 0)
            {
                Articles = _articlesFacade.GetTopArticlesByCategoryId(SelectedCategoryId, SelectedTimePeriodId);
            }
            else if (SelectedCountryId != 0)
            {
                Articles = _articlesFacade.GetTopArticlesByCountryId(SelectedCountryId, SelectedTimePeriodId);
            }
            else
            {
                Articles = null;
            }
        }

        public void UpdateFilter()
        {
            LoadCategories();
        }

        public void ShowHideFilter()
        {
            if (IsFilterVisible)
            {
                IsFilterVisible = false;
                Context.ResultIdFragment = "top";

                LoadArticles();
                Cookies.Set(nameof(ArticlesFilter), nameof(SelectedCountryId), SelectedCountryId);
            }
            else
            {
                IsFilterVisible = true;
                Context.ResultIdFragment = "bottom";
            }

            UpdateFilterButtonText();
        }

        private void UpdateFilterButtonText()
        {
            if (IsFilterVisible)
            {
                FilterButtonText = "Apply";
                return;
            }

            if (SelectedCountryId != 0)
            {
                var countryName = Countries?.FirstOrDefault(c => c.Id == SelectedCountryId).Name;
                var categoryName = Categories?.FirstOrDefault(c => c.Id == SelectedCategoryId).Name;
                var timePeriodName = TimePeriods?.FirstOrDefault(t => t.Id == SelectedTimePeriodId).Name;

                FilterButtonText = $"{countryName} • {categoryName} • {timePeriodName}";
            }
            else
            {
                FilterButtonText = "Filter";
            }
        }
    }
}