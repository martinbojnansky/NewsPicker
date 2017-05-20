using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NewsPicker.Mobile.Controls
{
    public partial class ProgressOverlay : Grid
    {
        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActiveProperty), typeof(bool), typeof(ProgressOverlay), false);

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public ProgressOverlay()
        {
            InitializeComponent();
        }
    }
}