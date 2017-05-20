using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Binding;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Shared.DTO.Category;
using DotVVM.Framework.ViewModel;

namespace NewsPicker.Web.Controls
{
    public class ArticlesFilter : DotvvmMarkupControl
    {
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public static readonly DotvvmProperty IsVisibleProperty
            = DotvvmProperty.Register<bool, ArticlesFilter>(c => c.IsVisible, false);

        public List<CountryDTO> Countries
        {
            get { return (List<CountryDTO>)GetValue(CountriesProperty); }
            set { SetValue(CountriesProperty, value); }
        }

        public static readonly DotvvmProperty CountriesProperty
            = DotvvmProperty.Register<List<CountryDTO>, ArticlesFilter>(c => c.Countries, null);

        public List<CategoryDTO> Categories
        {
            get { return (List<CategoryDTO>)GetValue(CategoriesProperty); }
            set { SetValue(CategoriesProperty, value); }
        }

        public static readonly DotvvmProperty CategoriesProperty
            = DotvvmProperty.Register<List<CategoryDTO>, ArticlesFilter>(c => c.Categories, null);

        public CountryDTO SelectedCountry { get; set; }

        public string SelectedQuery
        {
            get { return (string)GetValue(SelectedQueryProperty); }
            set { SetValue(SelectedQueryProperty, value); }
        }

        public static readonly DotvvmProperty SelectedQueryProperty
            = DotvvmProperty.Register<string, ArticlesFilter>(c => c.SelectedQuery, null);

        [AllowStaticCommand]
        public void Show() => IsVisible = true;

        [AllowStaticCommand]
        public void Hide() => IsVisible = false;
    }
}