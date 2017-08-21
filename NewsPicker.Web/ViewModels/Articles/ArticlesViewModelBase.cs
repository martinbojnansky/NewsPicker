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
using NewsPicker.Web.Resources.Localization;
using NewsPicker.Web.Services.Localization;

namespace NewsPicker.Web.ViewModels.Articles
{
    public class ArticlesViewModelBase : NewsPicker.Web.ViewModels.LayoutViewModel
    {
        private readonly CountriesFacade _countriesFacade = new CountriesFacade();

        private readonly CategoriesFacade _categoriesFacade = new CategoriesFacade();

        private readonly ArticlesFacade _articlesFacade = new ArticlesFacade();

        public bool IsEmpty => Articles?.Count == 0;

        public int SelectedCountryId { get; set; }

        public int SelectedCategoryId { get; set; }

        public int SelectedTimePeriodId { get; set; } = (int)TimePeriodValue.DAY;

        public string FilterButtonText { get; set; }

        public List<CountryDTO> Countries { get; set; }

        public List<CategoryDTO> Categories { get; set; }

        public List<ArticleDTO> Articles { get; set; }

        public List<TimePeriod> TimePeriods { get; set; } = TimePeriod.All();

        public override Task Load()
        {
            if (!Context.IsPostBack)
            {
                LoadData();
            }

            return base.Load();
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
            }
            else
            {
                SelectedCountryId = 1;
                SelectedCountryChanged();
            }
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
                Categories.Insert(0, new CategoryDTO() { Id = 0, Name = LocalizedStringResources.AllCategoriesOptionText });
                SelectedCategoryId = 0;
            }
            else
            {
                Categories = null;
            }
        }

        private void LoadArticles()
        {
            Context.ResultIdFragment = "top";

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

        public void SelectedCountryChanged()
        {
            Cookies.Set(nameof(ArticlesFilter), nameof(SelectedCountryId), SelectedCountryId);
            Context.ChangeCulture(Countries?.FirstOrDefault(c => c.Id == SelectedCountryId)?.Code);
        }

        public void SelectedCategoryChanged() => LoadArticles();

        public void SelectedTimePeriodChanged() => LoadArticles();
    }
}