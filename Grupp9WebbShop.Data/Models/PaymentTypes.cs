using System.ComponentModel.DataAnnotations;

namespace Grupp9WebbShop.Data.Models
{
    public enum PaymentTypes
    {
        [Display(Name ="Kreditkort")]
        CreditCard,
        Klarna
    }
}
