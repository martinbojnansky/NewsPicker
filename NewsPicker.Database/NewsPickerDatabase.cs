using NewsPicker.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using XamarinToolkit.IoC;

namespace NewsPicker.Database
{
    public class NewsPickerDatabase : DbContext, ISingleResolvable
    {  
        public NewsPickerDatabase() : base($"name={nameof(NewsPickerDatabase)}") { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Error> Errors { get; set; }
    }
}
