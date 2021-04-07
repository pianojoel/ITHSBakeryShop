using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Grupp9WebbShop.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]

        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
