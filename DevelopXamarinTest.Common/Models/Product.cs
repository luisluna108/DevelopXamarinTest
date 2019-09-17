using System;
using System.ComponentModel.DataAnnotations;

namespace DevelopXamarinTest.Common.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Description { get; set; }

        public Decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime PublishdOn { get; set; }
    }
}
