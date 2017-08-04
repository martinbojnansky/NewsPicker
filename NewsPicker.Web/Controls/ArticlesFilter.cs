﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Binding;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Shared.DTO.Category;
using DotVVM.Framework.Binding.Expressions;
using NewsPicker.Web.Models.ArticlesFilter;
using DotVVM.Framework.ViewModel;

namespace NewsPicker.Web.Controls
{
    public class ArticlesFilter : DotvvmMarkupControl
    {
        [MarkupOptions(Required = true)]
        public List<CountryDTO> Countries

        {
            get { return (List<CountryDTO>)GetValue(CountriesProperty); }
            set { SetValue(CountriesProperty, value); }
        }

        public static readonly DotvvmProperty CountriesProperty
            = DotvvmProperty.Register<List<CountryDTO>, ArticlesFilter>(c => c.Countries);

        [MarkupOptions(Required = true)]
        public List<CategoryDTO> Categories
        {
            get { return (List<CategoryDTO>)GetValue(CategoriesProperty); }
            set { SetValue(CategoriesProperty, value); }
        }

        public static readonly DotvvmProperty CategoriesProperty
            = DotvvmProperty.Register<List<CategoryDTO>, ArticlesFilter>(c => c.Categories);

        public List<TimePeriod> TimePeriods
        {
            get { return (List<TimePeriod>)GetValue(TimePeriodsProperty); }
            set { SetValue(TimePeriodsProperty, value); }
        }

        public static readonly DotvvmProperty TimePeriodsProperty
            = DotvvmProperty.Register<List<TimePeriod>, ArticlesFilter>(c => c.TimePeriods);

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

        public int SelectedTimePeriodId
        {
            get { return (int)GetValue(SelectedTimePeriodIdProperty); }
            set { SetValue(SelectedTimePeriodIdProperty, value); }
        }

        public static readonly DotvvmProperty SelectedTimePeriodIdProperty
            = DotvvmProperty.Register<int, ArticlesFilter>(c => c.SelectedTimePeriodId);

        [MarkupOptions(Required = true)]
        public Command UpdateFilterCommand
        {
            get { return (Command)GetValue(UpdateFilterCommandProperty); }
            set { SetValue(UpdateFilterCommandProperty, value); }
        }

        public static readonly DotvvmProperty UpdateFilterCommandProperty
            = DotvvmProperty.Register<Command, ArticlesFilter>(c => c.UpdateFilterCommand);

        [MarkupOptions(Required = true)]
        public Command ApplyFilterCommand
        {
            get { return (Command)GetValue(ApplyFilterCommandProperty); }
            set { SetValue(ApplyFilterCommandProperty, value); }
        }

        public static readonly DotvvmProperty ApplyFilterCommandProperty
            = DotvvmProperty.Register<Command, ArticlesFilter>(c => c.ApplyFilterCommand);
    }
}