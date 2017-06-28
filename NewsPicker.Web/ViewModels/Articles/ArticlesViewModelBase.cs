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

namespace NewsPicker.Web.ViewModels.Articles
{
    public class ArticlesViewModelBase : NewsPicker.Web.ViewModels.LayoutViewModel
    {
        //private const string COUNTRY_QUERY_KEY = "co";
        //private const string CATEGORY_QUERY_KEY = "ca";
        //private const string TIME_PERIOD_QUERY_KEY = "tp";

        private readonly CountriesFacade _countriesFacade = new CountriesFacade();

        private readonly CategoriesFacade _categoriesFacade = new CategoriesFacade();

        private readonly ArticlesFacade _articlesFacade = new ArticlesFacade();

        public int SelectedCountryId { get; set; }

        public int SelectedCategoryId { get; set; }

        public int SelectedTimePeriodId { get; set; }

        public bool IsFilterVisible { get; set; }

        public List<CountryDTO> Countries { get; set; }

        public List<CategoryDTO> Categories { get; set; }

        public List<ArticleDTO> Articles { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                LoadFilterValues();
                LoadData();
            }

            return base.PreRender();
        }

        private void LoadFilterValues()
        {
            LoadSelectedCountry();
            LoadSelectedCategory();
            LoadSelectedTimePeriod();
        }

        public void LoadData()
        {
            LoadCountries();
            LoadCategories();
            LoadArticles();
        }

        private void LoadSelectedCountry()
        {
            //var selectedCountryId = Convert.ToInt32(Context.Query[COUNTRY_QUERY_KEY]);
            //if (selectedCountryId != 0)
            //{
            //    SelectedCountryId = selectedCountryId;
            //    return;
            //}

            var selectedCountryId = Convert.ToInt32(Cookies.Get(nameof(ArticlesFilter), nameof(SelectedCountryId)));
            if (selectedCountryId != 0)
            {
                SelectedCountryId = selectedCountryId;
                return;
            }

            SelectedCountryId = 1;
            IsFilterVisible = true;
        }

        private void LoadSelectedCategory()
        {
            //SelectedCategoryId = Convert.ToInt32(Context.Query[CATEGORY_QUERY_KEY]);
        }

        private void LoadSelectedTimePeriod()
        {
            //var selectedTimePeriodId = Convert.ToInt32(Context.Query[TIME_PERIOD_QUERY_KEY]);
            //if (selectedTimePeriodId != 0)
            //{
            //    SelectedTimePeriodId = selectedTimePeriodId;
            //    return;
            //}

            SelectedTimePeriodId = (int)TimePeriodValue.DAY;
        }

        public void LoadCountries()
        {
            Countries = _countriesFacade.GetCountries();
            LoadSelectedCountry();
        }

        public void LoadCategories()
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

        public void LoadArticles()
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

        public void Filter()
        {
            if (IsFilterVisible)
            {
                IsFilterVisible = false;
                Context.ResultIdFragment = "top";

                Cookies.Set(nameof(ArticlesFilter), nameof(SelectedCountryId), SelectedCountryId);
                LoadArticles();
                //Context.RedirectToRoute("Default", null, true, true,
                //    $"?{COUNTRY_QUERY_KEY}={SelectedCountryId}&{CATEGORY_QUERY_KEY}={SelectedCategoryId}&{TIME_PERIOD_QUERY_KEY}={SelectedTimePeriodId}");
            }
            else
            {
                IsFilterVisible = true;
                Context.ResultIdFragment = "bottom";
            }
        }
    }
}