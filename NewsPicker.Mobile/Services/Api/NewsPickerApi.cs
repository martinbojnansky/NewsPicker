using NewsPicker.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinToolkit.Http;

namespace NewsPicker.Mobile.Services.Api
{
    public class NewsPickerApi : RestClient
    {
        public NewsPickerApi()
        {
            Init(ApiConstants.BASE_ADDRESS, ApiConstants.API_KEY);
        }
    }
}
