﻿using System.ComponentModel.DataAnnotations.Schema;
namespace Grupp9WebbShop.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [NotMapped]
        public decimal RowPrice
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}