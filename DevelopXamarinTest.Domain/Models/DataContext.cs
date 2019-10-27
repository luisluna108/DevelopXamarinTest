using DevelopXamarinTest.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopXamarinTest.Domain.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DevelopXamarinTestDB")
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
