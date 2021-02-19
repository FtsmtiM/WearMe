using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Models;

namespace WearMe.ViewModels
{
    public class SexMenuViewModel
    {
        public IEnumerable<Sex> Sexs { get; set; }
        public int CurrentCategory { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public int CurrentSex { get; set; }

    }
}
