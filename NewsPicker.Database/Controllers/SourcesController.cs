using NewsPicker.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Database.Controllers
{
    public class SourcesController
    {
        private NewsPickerDatabase _db = new NewsPickerDatabase();

        public IList<Source> GetAll()
        {
            return _db.Sources.ToList();
        }

        public IList<Source> GetActive()
        {
            return _db.Sources.Where(s => s.IsActive).ToList();
        }

        public void UpdateDate(int sourceId)
        {
            var source = _db.Sources.Find(sourceId);

            if (source != null)
            {
                source.UpdatedDate = DateTime.UtcNow;
                _db.SaveChanges();
            }
        }
    }
}