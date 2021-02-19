using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Interfaces;
using WearMe.Data.Models;

namespace WearMe.Data.Repositories
{
    public class ClothRepository : IClothRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClothRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Cloth> Clothes => _appDbContext.Clothes.Include(c=>c.SexCategory);
        public IEnumerable<Cloth> Trending => _appDbContext.Clothes.Where(t => t.IsTrending).Include(c => c.SexCategory);
        public Cloth GetClothbyId(int id) => _appDbContext.Clothes.FirstOrDefault(c => c.ClothId == id);
        
    }
}
