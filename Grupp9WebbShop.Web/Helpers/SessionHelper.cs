using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
namespace Grupp9WebbShop.Web.Helpers
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object obj)
        {
            session.SetString(key, JsonSerializer.Serialize(obj));
        }
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var val = session.GetString(key);
            return val == null ? default(T) : JsonSerializer.Deserialize<T>(val);
        }
    }
}
