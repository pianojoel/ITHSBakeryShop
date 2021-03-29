using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Grupp9WebbShop.Web.Areas.Identity.Data
{
    public class RegisterInputModel
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

        [Required(ErrorMessage = "E-postadress måste fyllas i")]
        [EmailAddress]
        [Display(Name = "E-postadress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lösenord måste fyllas i")]
        [StringLength(100, ErrorMessage = "{0}et måste vara minst {2} och max {1} tecken långt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte.")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="Godkänn vårt avtal")]
        [Range(typeof(bool),"true","true", ErrorMessage ="Du måste godkänna de allmänna villkoren")]
        public bool Agreement { get; set; }

    }
}
