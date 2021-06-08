using Core_SatelliteBlogSitesi.Models.DATA;
using Core_SatelliteBlogSitesi.Models.Methods;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;

namespace Core_SatelliteBlogSitesi.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }


        [HttpPost]
        public IActionResult Insert(string email, bool kvkk, bool sozlesme)
        {
            if (email.Trim() != "" && kvkk == false && sozlesme == false)
            {
                TempData["mesaj"] = _userRepository.InsertUser(email);
            }
            return RedirectToAction("Anasayfa", "Ziyaretci");
        }


        public IActionResult Activation()
        {
            if (RouteData.Values["id"] != null)
            {
                ViewBag.message = _userRepository.Activation(RouteData.Values["id"].ToString());
            }
            return View();
        }


        [HttpPost]
        public IActionResult GirisYap(string email)
        {
            User user = _userRepository.Users.Where(x => x.Email == email).FirstOrDefault();

            if (user != null)
            {
                if (user.IsActive)
                {
                    _userRepository.GirisYap(email);
                    HttpContext.Session.SetInt32("userID", user.UserID);
                }

                else if (!user.IsActive)
                {
                    GeneralMethods.SendActivationMail(user);
                }
            }

            return RedirectToAction("Anasayfa", "Ziyaretci");
        }


        [Route("CıkısYap/User")]
        public IActionResult CıkısYap()
        {
            if (HttpContext.Session.GetInt32("userID") != null)
                HttpContext.Session.Remove("userID");

            return RedirectToAction("Anasayfa", "Ziyaretci");
        }




        [Route("/Anasayfa")]
        public IActionResult Anasayfa()
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {

                Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> tuple = _userRepository.TupleListeler();

                ViewBag.id = HttpContext.Session.GetInt32("userID");

                return View(tuple);

            }
            else
                return RedirectToAction("Anasayfa", "Ziyaretci");

        }


        [Route("/@{kullanıcıadı}")]
        public IActionResult Profil()
        {

            User userProfil = _userRepository.Users.Where(x => x.UserName == (string)RouteData.Values["kullanıcıadı"]).FirstOrDefault();

            if (HttpContext.Session.GetInt32("userID") != null)
            {
                if (HttpContext.Session.GetInt32("userID") != userProfil.UserID)
                {
                    userProfil.HitRate++;
                    _userRepository.IncreaseUserHitRate(userProfil);
                }

                ViewBag.id = HttpContext.Session.GetInt32("userID");
            }
            else
            {
                userProfil.HitRate++;
                _userRepository.IncreaseUserHitRate(userProfil);
            }

            ViewBag.kullanıcıadı = RouteData.Values["kullanıcıadı"];

            Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> tuple = _userRepository.TupleListeler();

            return View(tuple);
        }

        [Route("/@me")]
        public IActionResult AtMe()
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                string userName = _userRepository.Users.Where(x => x.UserID == HttpContext.Session.GetInt32("userID")).Select(x => x.UserName).FirstOrDefault();
                return RedirectToAction("Profil", "User", new { kullanıcıadı = userName, id = RouteData.Values["id"] });
            }
            else
                return RedirectToAction("Anasayfa", "Ziyaretci");

        }

        [HttpGet]
        [Route("/User/Ayarlar")]
        public IActionResult Ayarlar(int id)
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                ViewBag.id = HttpContext.Session.GetInt32("userID");
                Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> tuple = _userRepository.TupleListeler();
                return View(tuple);
            }

            else
                return RedirectToAction("Anasayfa", "Ziyaretci");

        }

        [HttpPost]
        [Route("/User/Ayarlar")]
        public IActionResult Ayarlar(string isim, string soyisim, string kullanıcıAdı, string bio, IFormFile avatar)
        {
            _userRepository.UpdateUser(isim, soyisim, kullanıcıAdı, bio, avatar, (int)HttpContext.Session.GetInt32("userID"));

            ViewBag.id = HttpContext.Session.GetInt32("userID");

            Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> tuple = _userRepository.TupleListeler();

            return View(tuple);
        }



        public IActionResult Hesabısil()
        {
            _userRepository.DeleteUser((int)HttpContext.Session.GetInt32("userID"));
            return RedirectToAction("Anasayfa", "Ziyaretci");
        }
    }
}
