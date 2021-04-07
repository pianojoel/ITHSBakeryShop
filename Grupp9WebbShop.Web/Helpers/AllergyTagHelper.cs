using Grupp9WebbShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grupp9WebbShop.Web.Helpers
{
    public static class AllergyTagHelper
    {

        private static string key = "CustomerAllergyTags";
        public static List<SelectListItem> LoadTags(ISession session)
        {
            List<SelectListItem> tags = session.GetObjectFromJson<List<SelectListItem>>(key);
            return tags;
        }
        public static void SaveTags(ISession session, List<SelectListItem> items)
        {
            session.SetObjectAsJson(key, items);
        }
    }
}
