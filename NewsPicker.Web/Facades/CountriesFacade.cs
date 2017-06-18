using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Database;

namespace NewsPicker.Web.Facades
{
    public class CountriesFacade
    {
        private NewsPickerDatabase db = new NewsPickerDatabase();

        public List<CountryDTO> GetCountries()
        {
            return db.Countries
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ProjectTo<CountryDTO>().ToList();
        }
    }
}