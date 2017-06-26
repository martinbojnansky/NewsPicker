using NewsPicker.Mobile.Controls;
using NewsPicker.Mobile.ViewModels.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinToolkit.ViewModel;

namespace NewsPicker.Mobile.Views.Articles
{
    public partial class ArticlesView : ViewModelPage
    {
        public ArticlesView()
        {
            InitializeComponent();
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            ViewModel().IsBusy = true;
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            ViewModel().IsBusy = false;
        }

        protected override bool OnBackButtonPressed()
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}