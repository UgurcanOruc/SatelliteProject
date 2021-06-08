using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA.Interfaces_and_Repositories
{
    public interface IUserCategoryRepository
    {
        void KonuTakipEt(int userID, int konuID);
        void KonuTakiptenÇıkar(int userID, int konuID);
    }
}
