using DevelopXamarinTest.Domain.Models;
using DevelopXamarinTestAPI;

namespace DevelopXamarinTestBackend.Models
{
    public class LocalDataContext : DataContext
    {
        public new System.Data.Entity.DbSet<DevelopXamarinTest.Common.Models.Product> Products { get; set; }
    }
}