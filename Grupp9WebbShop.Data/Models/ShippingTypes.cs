using System.ComponentModel.DataAnnotations;

namespace Grupp9WebbShop.Data.Models
{
    public enum ShippingTypes
    {
        [Display(Name = "Standard")]
        Normal,
        [Display(Name ="Express")]
        Express
    }
}
