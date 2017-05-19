using NewsPicker.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Database.Controllers
{
    public class ErrorsController
    {
        private NewsPickerDatabase _db = new NewsPickerDatabase();

        public IList<Article> GetAll()
        {
            return _db.Articles.ToList();
        }

        public void Add(string message)
        {
            _db.Errors.Add(new Error()
            {
                Message = message
            });

            _db.SaveChanges();
        }
    }
}
