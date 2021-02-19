using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Interfaces;
using WearMe.Data.Models;

namespace WearMe.Data.Repositories
{
    public class SexRepository : ISexRepository
    {
        AppDbContext _appDbContext;
        public SexRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Sex> SexGroups => _appDbContext.SexGroups.Include(c=>c.SexCategories);
    }
}
