using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Database;

namespace NewsPicker.Web.Controllers
{
    public class CountriesController
    {
        private NewsPickerDatabase db = new NewsPickerDatabase();

        public List<CountryDTO> GetCountries()
        {
            return db.Countries
                .OrderBy(c => c.Name)
                .ProjectTo<CountryDTO>().ToList();
        }
    }
}