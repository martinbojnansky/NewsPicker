using NewsPicker.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace NewsPicker.Api.Authorization
{
    public class ApiKeyAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
#if !DEBUG
            var key = actionContext.Request.Headers.Authorization?.Scheme;

            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            else if (key == ApiConstants.API_KEY)
            {
                return true;
            }

            HandleUnauthorizedRequest(actionContext);
            return false;
#else
            return true;
#endif
        }
    }
}