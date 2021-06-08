using Core_SatelliteBlogSitesi.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;
using Core_SatelliteBlogSitesi.Models.Methods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Core_SatelliteBlogSitesi.Models.DATA
{
    public class UserRepository : IUserRepository
    {
        private ProjectContext _context;
        
        [Obsolete]
        public IHostingEnvironment Environment;

        [Obsolete]
        public UserRepository(ProjectContext context, IHostingEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }

       
        
        public IQueryable<User> Users => _context.Users;

        
        
        public User GetByID(int id)
        {
            return _context.Users.Find(id);
        }

       
        
        public string InsertUser(string mail)
        {
            User userKontrol = _context.Users.Where(x => x.Email == mail).FirstOrDefault();
            
            if (userKontrol == null)
            {
                Guid activationCode = Guid.NewGuid();
                User user = new User()
                {
                    FirstName = mail.Substring(0, mail.IndexOf('@')),
                    LastName = "Satellite",
                    Email = mail,
                    UserName = mail.Substring(0, 7),
                    URL = "www.satelliteblog.com/@",
                    IsActive = false,
                    ActivationCode = activationCode,
                };

                GeneralMethods.SendActivationMail(user);
                _context.Users.Add(user);
                _context.SaveChanges();
                return "Kaydolma İşleminiz Başarıyla Gerçekleşmiştir";
            }
            
            else
                return "Sistemde zaten bu mail ile kayıtlı bir kullanıcı bulunmaktadır.";
        }


        
        public string Activation(string id)
        {
            Guid activationCode = new Guid(id);
            User user = _context.Users.Where(p => p.ActivationCode == activationCode).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = true;
                _context.Users.Update(user);
                _context.SaveChanges();
                return "Aktivasyon Başarıyla Gerçekleşmiştir.Lütfen Anasayfaya Dönerek Üye Girişinizi Yapınız.";
            }
            return "Aktivasyon Hatası.";
        }

        
        
        public void GirisYap(string Email)
        {
            User user = _context.Users.Where(x => x.Email == Email).FirstOrDefault();
            if (user != null)
            {
                GeneralMethods.GirisEmail(user);
            }
        }

       
        
        
        [Obsolete]
        public bool UpdateUser(string isim, string soyisim, string kullanıcıAdı, string bio, IFormFile avatar, int id)
        {
            User user = Users.Where(x => x.UserID == id).FirstOrDefault();
            
            string uniqueFileName = string.Empty;

            if (avatar == null)
                uniqueFileName = user.Avatar;
            
            else
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\image\\postedImage", user.Avatar);

                if (File.Exists(path)) File.Delete(path);

                uniqueFileName = GeneralMethods.UploadImage(avatar, Environment);
            }
              

            user.FirstName = isim;
            user.LastName = soyisim;
            user.UserName = kullanıcıAdı;
            user.Bio = bio;
            user.Avatar = uniqueFileName;
            
            _context.Users.Update(user);
            return _context.SaveChanges() > 0;
        }
        
        
        
        public void IncreaseUserHitRate(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }


       
        
        public bool DeleteUser(int id)
        {
            User deAktif = Users.Where(x => x.UserID == id).FirstOrDefault();
            deAktif.IsActive = false;
            _context.Users.Update(deAktif);
            return _context.SaveChanges() > 0;
        }
        


        
        public Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> TupleListeler()
        {
            List<Article> articles = _context.Articles.ToList();
            List<ArticleCategory> articleCategories = _context.ArticleCategories.ToList();
            List<Category> categories = _context.Categories.ToList();
            List<UserCategory> userCategories = _context.UserCategories.ToList();
            List<User> users = _context.Users.ToList();

            return Tuple.Create(articles, articleCategories, categories, userCategories, users);
        }
    }
}
