using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Grupp9WebbShop.Web.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the WebbShopUser class
    public class WebbShopUser : IdentityUser
    {
        [ProtectedPersonalData]
        public string FirstName { get; set; }
        [ProtectedPersonalData]
        public string LastName { get; set; }
        [ProtectedPersonalData]
        public string StreetAddress { get; set; }
        [ProtectedPersonalData]
        public string PostalCode { get; set; }
        [ProtectedPersonalData]
        public string City { get; set; }
    }
}
