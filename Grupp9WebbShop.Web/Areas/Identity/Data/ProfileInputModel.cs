using System.ComponentModel.DataAnnotations;

namespace Grupp9WebbShop.Web.Areas.Identity.Data
{
    public class ProfileInputModel
    {
        [Required(ErrorMessage = "Förnamn måste fyllas i")]
        [Display(Name = "Förnamn")]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Efternamn måste fyllas i")]
        [Display(Name = "Efternamn")]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gatuadress måste fyllas i")]
        [Display(Name = "Gatuadress")]
        [StringLength(40)]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Postnummer måste fyllas i")]
        [Display(Name = "Postnummer")]
        [StringLength(6)]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Postort måste fyllas i")]
        [Display(Name = "Postort")]
        [StringLength(20)]
        public string City { get; set; }

        [Phone]
        [Display(Name = "Telefon")]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
    }

}
