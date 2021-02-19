using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Models;

namespace WearMe.Data.Interfaces
{
    public interface ISexRepository
    {
        IEnumerable<Sex> SexGroups { get; }
    }
}
