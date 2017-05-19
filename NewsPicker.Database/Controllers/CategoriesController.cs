using NewsPicker.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Database.Controllers
{
    public class CategoriesController
    {
        private NewsPickerDatabase _db = new NewsPickerDatabase();

        public IList<Category> GetAll()
        {
            return _db.Categories.ToList();
        }
    }
}
