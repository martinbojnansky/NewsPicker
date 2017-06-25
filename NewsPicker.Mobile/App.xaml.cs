using NewsPicker.Mobile.ViewModels.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;
using XamarinToolkit.IoC;

namespace NewsPicker.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var resolver = GetResolver();
            var viewModel = resolver.ResolveViewModel<ArticlesViewModel>();
            var view = resolver.ResolveViewModelPage(viewModel);

            MainPage = view;
        }

        private IoCResolver GetResolver()
        {
            var resolver = new IoCResolver();
            resolver.BuildContainer(new Assembly[]
            {
                typeof(App).GetTypeInfo().Assembly,
                typeof(Shared.NewsPickerShared).GetTypeInfo().Assembly,
                typeof(XamarinToolkit.XamarinToolkit).GetTypeInfo().Assembly
            });

            return resolver;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}