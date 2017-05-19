using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Shared.DTO.Source;

namespace NewsPicker.Shared.DTO.Article
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ShareCount { get; set; }
        public virtual SourceDTO Source { get; set; }
    }
}
