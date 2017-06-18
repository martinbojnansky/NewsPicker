using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NewsPicker.Shared.DTO.Category;
using NewsPicker.Database;

namespace NewsPicker.Web.Facades
{
    public class CategoriesFacade
    {
        private NewsPickerDatabase db = new NewsPickerDatabase();

        public List<CategoryDTO> GetCategoriesByCountryId(int countryId)
        {
            return db.Categories
                .Where(c => c.CountryId == countryId && c.IsActive)
                .OrderByDescending(c => c.Order.HasValue).ThenBy(c => c.Order.Value).ThenBy(c => c.Name)
                .ProjectTo<CategoryDTO>().ToList();
        }
    }
}