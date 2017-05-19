using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Database.Models
{
    public class Source
    {
        public int Id { get; set; }
        [Url]
        [Required]
        public string Url { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
