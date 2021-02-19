using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Models;

namespace WearMe.ViewModels
{
    public class CategoryMenuViewModel
    {
       // public IEnumerable<Category> Categories { get; set; }
        public int CurrentSex { get; set; }
        public int CurrentCategory { get; set; }
        public IEnumerable<SexCategory> CategoriesSex { get; set; }
    }
}
