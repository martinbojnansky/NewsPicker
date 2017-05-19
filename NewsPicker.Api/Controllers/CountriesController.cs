using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Api.Authorization;
using NewsPicker.Database;

namespace NewsPicker.Api.Controllers
{
    public class CountriesController : ApiKeyAuthorizationController
    {
        private NewsPickerDatabase db = new NewsPickerDatabase();

        [HttpGet]
        [ResponseType(typeof(List<CountryDTO>))]
        public List<CountryDTO> GetCountries()
        {
            return db.Countries.ProjectTo<CountryDTO>().ToList();
        }
    }
}