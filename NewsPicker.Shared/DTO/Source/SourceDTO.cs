using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Shared.DTO.Source
{
    public class SourceDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
