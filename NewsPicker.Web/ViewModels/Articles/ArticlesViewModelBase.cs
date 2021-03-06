using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Shared.DTO.Article;
using NewsPicker.Web.Facades;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Shared.DTO.Category;
using NewsPicker.Shared.Models;
using System.Web;
using NewsPicker.Web.Models.ArticlesFilter;
using NewsPicker.Web.Resources.Localization;
using NewsPicker.Web.Helpers.Localization;

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

        public List<TimePeriod> TimePeriods { get; set; }

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
            LoadTimePeriods();
            LoadArticles();
        }

        private void LoadSelectedCountry()
        {
            var selectedCountry = Countries.FirstOrDefault(c => c.Code == CultureName);
            if (selectedCountry != null)
            {
                SelectedCountryId = selectedCountry.Id;
            }
            else
            {
                Context.ChangeCulture(Constants.DefaultCultureName);
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

        private void LoadTimePeriods()
        {
            TimePeriods = new List<TimePeriod>()
            {
                new TimePeriod(TimePeriodValue.TWELVE_HOURS, $"12 {LocalizedStringResources.HoursOptionText}"),
                new TimePeriod(TimePeriodValue.DAY, $"1 {LocalizedStringResources.DayOptionText}"),
                new TimePeriod(TimePeriodValue.THREE_DAYS, $"3 {LocalizedStringResources.DaysOptionText}"),
                new TimePeriod(TimePeriodValue.WEEK, $"1 {LocalizedStringResources.WeekOptionText}")
            };
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

        public void SelectedCountryChanged()
        {
            Context.ChangeCulture(Countries?.FirstOrDefault(c => c.Id == SelectedCountryId)?.Code);
        }

        public void SelectedCategoryChanged() => LoadArticles();

        public void SelectedTimePeriodChanged() => LoadArticles();
    }
}