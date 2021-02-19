using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Models;

namespace WearMe.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Cloth> Trending { get; set; }
    }
}
