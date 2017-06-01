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
        public CountryDTO SelectedCountry
        {
            get { return (CountryDTO)GetValue(SelectedCountryProperty); }
            set { SetValue(SelectedCountryProperty, value); }
        }

        public static readonly DotvvmProperty SelectedCountryProperty
            = DotvvmProperty.Register<CountryDTO, ArticlesFilter>(c => c.SelectedCountry, null, true);

        [MarkupOptions(Required = true)]
        public CategoryDTO SelectedCategory
        {
            get { return (CategoryDTO)GetValue(SelectedCategoryProperty); }
            set { SetValue(SelectedCategoryProperty, value); }
        }

        public static readonly DotvvmProperty SelectedCategoryProperty
            = DotvvmProperty.Register<CategoryDTO, ArticlesFilter>(c => c.SelectedCategory, null, true);

        [MarkupOptions(Required = true)]
        public Command ApplyCommand
        {
            get { return (Command)GetValue(ApplyCommandProperty); }
            set { SetValue(ApplyCommandProperty, value); }
        }

        public static readonly DotvvmProperty ApplyCommandProperty
            = DotvvmProperty.Register<Command, ArticlesFilter>(c => c.ApplyCommand, null);

        [AllowStaticCommand]
        public async void Apply()
        {
            Hide();
            await ApplyCommand.Invoke();
        }

        [AllowStaticCommand]
        public void Show() => IsVisible = true;

        [AllowStaticCommand]
        public void Hide() => IsVisible = false;
    }
}