using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Binding;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Shared.DTO.Category;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Binding.Expressions;

namespace NewsPicker.Web.Controls
{
    public class ArticlesFilter : DotvvmMarkupControl
    {
        [MarkupOptions(Required = true)]
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public static readonly DotvvmProperty IsVisibleProperty
            = DotvvmProperty.Register<bool, ArticlesFilter>(c => c.IsVisible, false);

        [MarkupOptions(Required = true)]
        public List<CountryDTO> Countries
        {
            get { return (List<CountryDTO>)GetValue(CountriesProperty); }
            set { SetValue(CountriesProperty, value); }
        }

        public static readonly DotvvmProperty CountriesProperty
            = DotvvmProperty.Register<List<CountryDTO>, ArticlesFilter>(c => c.Countries, null);

        [MarkupOptions(Required = true)]
        public List<CategoryDTO> Categories
        {
            get { return (List<CategoryDTO>)GetValue(CategoriesProperty); }
            set { SetValue(CategoriesProperty, value); }
        }

        public static readonly DotvvmProperty CategoriesProperty
            = DotvvmProperty.Register<List<CategoryDTO>, ArticlesFilter>(c => c.Categories, null);

        [MarkupOptions(Required = true)]
        public int SelectedCountryId
        {
            get { return (int)GetValue(SelectedCountryIdProperty); }
            set { SetValue(SelectedCountryIdProperty, value); }
        }

        public static readonly DotvvmProperty SelectedCountryIdProperty
            = DotvvmProperty.Register<int, ArticlesFilter>(c => c.SelectedCountryId);

        [MarkupOptions(Required = true)]
        public int SelectedCategoryId
        {
            get { return (int)GetValue(SelectedCategoryIdProperty); }
            set { SetValue(SelectedCategoryIdProperty, value); }
        }

        public static readonly DotvvmProperty SelectedCategoryIdProperty
            = DotvvmProperty.Register<int, ArticlesFilter>(c => c.SelectedCategoryId);

        public Command CountryChangedCommand
        {
            get { return (Command)GetValue(CountryChangedCommandProperty); }
            set { SetValue(CountryChangedCommandProperty, value); }
        }

        public static readonly DotvvmProperty CountryChangedCommandProperty
            = DotvvmProperty.Register<Command, ArticlesFilter>(c => c.CountryChangedCommand);

        [MarkupOptions(Required = true)]
        public Command CategoryChangedCommand
        {
            get { return (Command)GetValue(CategoryChangedCommandProperty); }
            set { SetValue(CategoryChangedCommandProperty, value); }
        }

        public static readonly DotvvmProperty CategoryChangedCommandProperty
            = DotvvmProperty.Register<Command, ArticlesFilter>(c => c.CategoryChangedCommand);

        public Command TimePeriodChangedCommand
        {
            get { return (Command)GetValue(TimePeriodChangedCommandProperty); }
            set { SetValue(TimePeriodChangedCommandProperty, value); }
        }

        public static readonly DotvvmProperty TimePeriodChangedCommandProperty
            = DotvvmProperty.Register<Command, ArticlesFilter>(c => c.TimePeriodChangedCommand);

        [AllowStaticCommand]
        public void Show() => IsVisible = true;

        [AllowStaticCommand]
        public void Hide() => IsVisible = false;
    }
}