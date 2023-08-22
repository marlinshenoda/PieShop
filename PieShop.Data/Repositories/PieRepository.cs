using Microsoft.EntityFrameworkCore;
using PieShop.Core.Models;
using PieShop.Data;
using PieShop.Data.Reposities;
using System.Collections.Generic;
using System.Linq;

namespace PieShop.Data.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public PieRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> Pies
        {
            get
            {
                return _appDbContext.Pie.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _appDbContext.Pie.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int  pieId)
        {
            return _appDbContext.Pie.Include(p => p.PieReviews).FirstOrDefault(p => p.PieId == pieId);
        }

        public void UpdatePie(Pie pie)
        {
            _appDbContext.Pie.Update(pie);
            _appDbContext.SaveChanges();
        }

        public void CreatePie(Pie pie)
        {
            _appDbContext.Pie.Add(pie);
            _appDbContext.SaveChanges();
        }
    }
}
