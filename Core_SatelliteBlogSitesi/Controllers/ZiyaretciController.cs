//using Core_SatelliteBlogSitesi.Models;
using Core_SatelliteBlogSitesi.Models.DATA;
using Core_SatelliteBlogSitesi.Models.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Controllers
{
    public class ZiyaretciController : Controller
    {
        private IUserRepository _userRepository;
        public ZiyaretciController(IUserRepository repository)
        {
            _userRepository = repository;
        }
        
        [HttpGet]
        public IActionResult Anasayfa()
        {
            Tuple<List<Article>, List<ArticleCategory>, List<Category>, List<UserCategory>, List<User>> tuple = _userRepository.TupleListeler();
            return View(tuple);
        }
    }
}
