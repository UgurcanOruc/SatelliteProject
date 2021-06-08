using Core_SatelliteBlogSitesi.Models.DATA;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Controllers
{
    public class MakaleController : Controller
    {
        private IArticleRepository _articleRepository;
        private IUserRepository _userRepository;

        public MakaleController(IArticleRepository repository, IUserRepository userrepository)
        {
            _articleRepository = repository;
            _userRepository = userrepository;

        }


        [Route("/{makaleAdı}")]
        public IActionResult MakaleAyrıntıSayfası(int id)
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                if ((int)HttpContext.Session.GetInt32("userID") != _articleRepository.Articles.Where(x => x.ArticleID == id).Select(x => x.UserID).FirstOrDefault())
                {
                    _articleRepository.IncreaseHitRate(id);
                }

                ViewBag.id = HttpContext.Session.GetInt32("userID");
            }

            ViewBag.makaleID = id;

            Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> tuple = _userRepository.TupleListeler();

            return View(tuple);
        }



        [Route("/KonularaGöreMakale/{id}")]
        public IActionResult KonularaGöreMakale(int id)
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                ViewBag.id = HttpContext.Session.GetInt32("userID");
                ViewBag.konuID = RouteData.Values["id"];

                _articleRepository.IncreaseHitRateOfCategory(id);

                Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> tuple = _userRepository.TupleListeler();

                return View(tuple);
            }
            else
                return RedirectToAction("Anasayfa", "Ziyaretci");

        }




        [Route("YeniMakale")]
        public IActionResult YeniMakale()
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
        [Route("YeniMakale")]
        public IActionResult YeniMakale(IFormFile makaleResmi, string makaleBaşlığı, string makaleİçeriği, List<int> konular)
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                _articleRepository.InsertArticle(makaleResmi, makaleBaşlığı, makaleİçeriği, konular, (int)HttpContext.Session.GetInt32("userID"));

                string userName = _userRepository.Users.Where(x => x.UserID == (int)HttpContext.Session.GetInt32("userID")).Select(x => x.UserName).FirstOrDefault();

                return RedirectToAction("Profil", "User", new { kullanıcıadı = userName });
            }
            else
                return RedirectToAction("Anasayfa", "Ziyaretci");


        }


        [Route("makale-duzenleme/{makaleid}")]
        public IActionResult MakaleDüzenle()
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                ViewBag.id = HttpContext.Session.GetInt32("userID");
                ViewBag.makaleID = RouteData.Values["makaleid"];
                Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> tuple = _userRepository.TupleListeler();
                return View(tuple);
            }
            else
                return RedirectToAction("Anasayfa", "Ziyaretci");

        }


        [HttpPost]
        [Route("makale-duzenleme/{makaleid}")]
        public IActionResult MakaleDüzenle(IFormFile makaleResmi, string makaleBaşlığı, string makaleİçeriği, List<int> konular)
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                _articleRepository.UpdateArticle(makaleResmi, makaleBaşlığı, makaleİçeriği, konular, Convert.ToInt32(RouteData.Values["makaleid"]));

                string userName = _userRepository.Users.Where(x => x.UserID == (int)HttpContext.Session.GetInt32("userID")).Select(x => x.UserName).FirstOrDefault();

                return RedirectToAction("Profil", "User", new { kullanıcıadı = userName });
            }
            else
                return RedirectToAction("Anasayfa", "Ziyaretci");

        }



        [Route("MakaleSil/{makaleid}")]
        public IActionResult MakaleSil()
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                _articleRepository.DeleteArticle(Convert.ToInt32(RouteData.Values["makaleid"]));

                User user = _userRepository.GetByID((int)HttpContext.Session.GetInt32("userID"));

                return RedirectToAction("Profil", "User", new { kullanıcıadı = user.UserName });
            }
            else
                return RedirectToAction("Anasayfa", "Ziyaretci");

        }
    }
}
