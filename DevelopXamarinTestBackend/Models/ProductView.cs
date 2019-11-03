using System.Web;
using DevelopXamarinTest.Common.Models;

namespace DevelopXamarinTestBackend.Models
{
    public class ProductView : Product
    {
        public HttpPostedFileBase ImageFile { get; set; }

    }
}