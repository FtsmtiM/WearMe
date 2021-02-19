using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Models;

namespace WearMe.ViewModels
{
    public class ClothsListViewModel
    {
        public IEnumerable<Cloth> Cloths { get; set; }
        public int CurrentSex { get; set; }
        public int CurrentCatagory { get; set; }
    }
}
