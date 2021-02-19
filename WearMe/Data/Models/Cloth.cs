using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearMe.Data.Models
{
    public class Cloth
    {
        public int ClothId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool IsTrending { get; set; }
        public bool InStock { get; set; }
        public Size Size { get; set; }
        public int CategoryId { get; set; }
        public int SexId { get; set; }
       
        public virtual SexCategory SexCategory { get; set; }
       
       
       
    }
}
