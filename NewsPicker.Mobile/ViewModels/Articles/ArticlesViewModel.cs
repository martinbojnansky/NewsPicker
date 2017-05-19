using NewsPicker.Mobile.Services.Api;
using NewsPicker.Mobile.ViewModels.Article;
using NewsPicker.Shared.DTO.Article;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinToolkit.ViewModel;

namespace NewsPicker.Mobile.ViewModels.Articles
{
    public class ArticlesViewModel : ViewModelBase
    {
        private bool isBusy;

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        private ObservableCollection<ArticleDTO> _articles;

        public ObservableCollection<ArticleDTO> Articles
        {
            get
            {
                return _articles;
            }
            set
            {
                _articles = value;
                RaisePropertyChanged(nameof(Articles));
            }
        }

        private Command<string> _navigateToArticleCommand;
        public Command<string> NavigateToArticleCommand => _navigateToArticleCommand ?? (_navigateToArticleCommand = new Command<string>(NavigateToArticle));

        public NewsPickerApi Api { get; set; }

        public override async Task OnAppearing()
        {
            IsBusy = true;
            await DownloadArticlesAsync();
            await base.OnAppearing();
            IsBusy = false;
        }

        public async Task DownloadArticlesAsync()
        {
            List<ArticleDTO> articles = new List<ArticleDTO>();

            try { articles = await Api.GetAsync<List<ArticleDTO>>("Articles/GetTopArticlesByCountryId?countryId=1&timePeriod=24"); }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            if (articles != null)
            {
                Articles = new ObservableCollection<ArticleDTO>(articles);
            }
        }

        public async void NavigateToArticle(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            var viewModel = Navigation.ResolveViewModel<ArticleViewModel>();
            viewModel.Url = url;
            await Navigation.PushAsync(viewModel);
        }
    }
}