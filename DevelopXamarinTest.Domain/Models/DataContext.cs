using DevelopXamarinTest.Common.Models;
using System.Data.Entity;

namespace DevelopXamarinTest.Domain.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
        
    }
}
