using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearMe.Data.Models
{
    public class SexCategory
    {
        public int SexCategoryId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SexId { get; set; }
        public Sex Sex { get; set; }
        public IList<Cloth> Cloths { get; set; }
    }
}
