using DevelopXamarinTest.Domain.Models;

namespace DevelopXamarinTestBackend.Models
{
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<DevelopXamarinTest.Common.Models.Product> Products { get; set; }
    }
}