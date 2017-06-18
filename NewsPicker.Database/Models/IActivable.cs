using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Database.Models
{
    public interface IActivable
    {
        [Index]
        bool IsActive { get; set; }
    }
}