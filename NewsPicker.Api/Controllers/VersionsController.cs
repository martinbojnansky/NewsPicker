using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using NewsPicker.Database.Models;
using NewsPicker.Shared.Constants;
using NewsPicker.Api.Authorization;
using NewsPicker.Database;

namespace NewsPicker.Api.Controllers
{
    public class VersionsController : ApiKeyAuthorizationController
    {
        private NewsPickerDatabase db = new NewsPickerDatabase();

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult IsVersionSupported(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                return BadRequest();
            }

            var isVersionSupported = ApiConstants.VERSION.Equals(version);

            return Ok(isVersionSupported);
        }
    }
}