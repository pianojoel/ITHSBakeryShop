using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Grupp9WebbShop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(40)]
        [Required]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Required]
        public ProductCategory Category { get; set; }

        public string ImageFile{ get; set; }

    }
}
