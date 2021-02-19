using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WearMe.Data.Models
{
    public class Sex
    {
        //public Sex()
        //{
        //    this.Categories = new HashSet<Category>();
        //}
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SexId { get; set; }
        public string SexGroup { get; set; }
        //public List<Cloth> Clothes { get; set; }
        public virtual ICollection<SexCategory> SexCategories { get; set; }
    }
}
