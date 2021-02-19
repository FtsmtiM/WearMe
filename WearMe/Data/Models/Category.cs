using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WearMe.Data.Models
{
    public class Category
    {
        //public Category()
        //{
        //    this.SexGroups = new HashSet<Sex>();
        //}
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        //public IList<Sex> SexGroup { get; set; }
        public string Desciption { get; set; }
        //public List<Sex> SexGroup { get; set; }
        //public List<Cloth> Clothes { get; set; }
        //public List<CategorySex> CategorySex { get; set; }
        public virtual ICollection<SexCategory> SexCategories { get; set; }
    }
}
