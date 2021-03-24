using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp9WebbShop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(60)]
        [Required]
        [Display(Name = "Produktnamn")]
        public string Name { get; set; }
        [StringLength(500)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [StringLength(200)]
        public string Summary { get; set; }
        [Display(Name = "Pris")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [NotMapped]
        public decimal CalculatedPrice
        {
            get
            {
                if (OnSale)
                    return Math.Round((Price * (decimal)OnSalePercentage), 0);
                else
                    return Math.Round(Price, 0);
            }
        }
        [Display(Name = "Kategori")]
        public ProductCategory Category { get; set; }
        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [StringLength(40)]
        [Display(Name = "Bild")]
        public string ImageFile { get; set; }
        [StringLength(80)]
        [Display(Name = "Bildbeskrivning")]
        public string ImageDescription { get; set; }
        [Display(Name = "Utvald")]
        public bool Highlighted { get; set; }
        [Display(Name = "Nedsatt Pris")]
        public bool OnSale { get; set; }
        [StringLength(500)]
        [Display(Name = "Allergiinformation")]
        public string AllergyInfo { get; set; }
        [Display(Name = "Produkten tillaggd")]
        public DateTime AddedDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OnSalePercentage { get; set; } = 0.8M;
    }
}
