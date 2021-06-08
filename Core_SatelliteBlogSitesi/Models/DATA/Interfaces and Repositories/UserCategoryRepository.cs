using Core_SatelliteBlogSitesi.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA.Interfaces_and_Repositories
{
    public class UserCategoryRepository : IUserCategoryRepository
    {
        private ProjectContext _context;
        public UserCategoryRepository(ProjectContext context)
        {
            _context = context;
        }
        
        
        public void KonuTakipEt(int userID, int konuID)
        {
            UserCategory konuTakip = new UserCategory()
            {
                UserID = userID,
                CategoryID = konuID,
            };

            Category kategori = _context.Categories.Where(x => x.CategoryID == konuID).FirstOrDefault();
            kategori.HitRate++;

            _context.UserCategories.Add(konuTakip);
            _context.Categories.Update(kategori);
            _context.SaveChanges();
        }


        public void KonuTakiptenÇıkar(int userID, int konuID)
        {
            UserCategory konuTakiptenÇıkart = _context.UserCategories.Where(x => x.UserID == userID && x.CategoryID == konuID).FirstOrDefault();
            _context.UserCategories.Remove(konuTakiptenÇıkart);
            _context.SaveChanges();
        }
    }
}
