using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinToolkit.ViewModel;

namespace NewsPicker.Mobile.ViewModels.Article
{
    public class ArticleViewModel : ViewModelBase
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

        private string _url;

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                RaisePropertyChanged(nameof(Url));
            }
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();
        }
    }
}