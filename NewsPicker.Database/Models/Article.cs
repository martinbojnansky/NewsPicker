using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Database.Models
{
    public class Article
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Url]
        public string Image { get; set; }

        [Url]
        [Required]
        public string Url { get; set; }

        [Required]
        [Index]
        public int UrlHash { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }

        public int EngagementCount { get; set; }

        public virtual ICollection<Source> Sources { get; set; }
    }
}