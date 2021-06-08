using Core_SatelliteBlogSitesi.Models.DATA;
using Core_SatelliteBlogSitesi.Models.DATA.Interfaces_and_Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Controllers
{
    public class UserCategoryController : Controller
    {
        private IUserCategoryRepository _repository;
        public UserCategoryController(IUserCategoryRepository repository)
        {
            _repository = repository;
        }



        [Route("/KonuTakipEt/{konuid}")]
        public IActionResult KonuTakipEt()
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                int userID = (int)HttpContext.Session.GetInt32("userID");
                int konuID = Convert.ToInt32(RouteData.Values["konuid"]);
                _repository.KonuTakipEt(userID, konuID);
                return RedirectToAction("Anasayfa", "User", new { id = userID });
            }
            else
                return RedirectToAction("Anasayfa", "Ziyaretci");

        }



        [Route("/KonuTakiptenCıkar/{konuid}")]
        public IActionResult KonuTakiptenÇıkar()
        {
            if (HttpContext.Session.GetInt32("userID") != null)
            {
                int userID = (int)HttpContext.Session.GetInt32("userID");
                int konuID = Convert.ToInt32(RouteData.Values["konuid"]);
                _repository.KonuTakiptenÇıkar(userID, konuID);
                return RedirectToAction("Anasayfa", "User", new { id = userID });
            }
            else
                return RedirectToAction("Anasayfa", "Ziyaretci");

        }
    }
}
