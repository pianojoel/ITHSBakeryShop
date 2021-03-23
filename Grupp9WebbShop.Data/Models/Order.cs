using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp9WebbShop.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                decimal shippingCost =0;
                switch (ShippingType)
                {
                    case ShippingTypes.Normal:
                        shippingCost = 29;
                        break;
                    case ShippingTypes.Express:
                        shippingCost = 49;
                        break;
                    default:
                        break;
                }

                var totalPrice = OrderItems.Sum(o => o.RowPrice);
                if (totalPrice < 299)
                    totalPrice += shippingCost;
                return totalPrice;
            }
        }

        public string UserID { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        public ShippingTypes ShippingType { get; set; }
        public PaymentTypes PaymentType { get; set; }

    }
}
