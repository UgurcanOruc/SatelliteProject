using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        User GetByID(int id);
        string InsertUser(string mail);
        string Activation(string id);
        void GirisYap(string Email);
        bool UpdateUser(string isim, string soyisim, string kullanıcıAdı, string bio, IFormFile avatar, int id);
        void IncreaseUserHitRate(User user);
        bool DeleteUser(int id);
       
        Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> TupleListeler();
    }
}
