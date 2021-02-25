using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Grupp9WebbShop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(60)]
        [Required]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(200)]
        public string Summary { get; set; }

        public decimal Price { get; set; }
        [Required]
        public ProductCategory Category { get; set; }

        [StringLength(40)]
        public string ImageFile { get; set; }
        [StringLength(80)]
        public string ImageDescription { get; set; }

    }
}
