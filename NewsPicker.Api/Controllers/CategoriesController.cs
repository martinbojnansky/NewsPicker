using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NewsPicker.Shared.DTO.Category;
using NewsPicker.Api.Authorization;
using NewsPicker.Database;

namespace NewsPicker.Api.Controllers
{
    public class CategoriesController : ApiKeyAuthorizationController
    {
        private NewsPickerDatabase db = new NewsPickerDatabase();

        [HttpGet]
        [ResponseType(typeof(List<CategoryDTO>))]
        public List<CategoryDTO> GetCategoriesByCountryId(int countryId)
        {
            return db.Categories
                .Where(c => c.CountryId == countryId)
                .OrderByDescending(c => c.Order.HasValue).ThenBy(c => c.Order.Value).ThenBy(c => c.Name)
                .ProjectTo<CategoryDTO>().ToList();
        }
    }
}