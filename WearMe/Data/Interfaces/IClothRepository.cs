using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Models;

namespace WearMe.Data.Interfaces
{
    public interface IClothRepository
    {
        IEnumerable<Cloth> Clothes { get; }
        IEnumerable<Cloth> Trending { get;}
        Cloth GetClothbyId(int id);

    }
}
