using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Grupp9WebbShop.Data.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
//        public List<Product> Products { get; set; }
        [StringLength(40)]
        public string ImageFile { get; set; }
        [StringLength(40)]
        public string ImageDescription { get; set; }

        public string Description { get; set; }
    }
}
