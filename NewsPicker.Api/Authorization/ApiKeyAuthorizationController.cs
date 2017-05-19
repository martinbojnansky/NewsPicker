using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NewsPicker.Api.Authorization
{
    [ApiKeyAuthorization]
    public abstract class ApiKeyAuthorizationController : ApiController { }
}