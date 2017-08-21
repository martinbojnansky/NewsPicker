using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using NewsPicker.Web.Services.Localization;

namespace NewsPicker.Web.ViewModels
{
    public class LayoutViewModel : DotvvmViewModelBase
    {
        public bool IsLoading { get; set; }

        public override Task Load()
        {
            if (!Context.IsPostBack)
            {
                Context.LoadCulture();
            }
            return base.Load();
        }
    }
}