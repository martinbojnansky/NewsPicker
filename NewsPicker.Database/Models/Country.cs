using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPicker.Database.Models
{
    public class Country : IActivable
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(2)]
        [Required]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public bool IsActive { get; set; }
    }
}