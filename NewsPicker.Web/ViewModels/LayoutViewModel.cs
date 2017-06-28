using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace NewsPicker.Web.ViewModels
{
    public class LayoutViewModel : DotvvmViewModelBase
    {
        public bool IsLoading { get; set; } = true;
    }
}