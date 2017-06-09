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
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Url]
        public string Image { get; set; }

        [Url]
        [Required]
        public string Url { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }

        public int ShareCount { get; set; }

        [Index]
        public int SourceId { get; set; }

        public virtual Source Source { get; set; }
    }
}