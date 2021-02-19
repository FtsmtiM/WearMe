using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Interfaces;
using WearMe.Data.Models;

namespace WearMe.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            if (appDbContext.Database.CanConnect())
            {
                _appDbContext = appDbContext;
            }
        }
        public IEnumerable<Category> Categories => _appDbContext.Categories.Include(s => s.SexCategories);
        public IEnumerable<SexCategory> CategoriesbySex(int id)
        {
            if (id == 0) { id = 1; }
            return _appDbContext.SexCategories.Include(s => s.Category).Where(s => s.SexId == id) ;
        }
    }
}
