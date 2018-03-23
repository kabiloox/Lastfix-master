using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electronique_Labo.Models;

namespace Electronique_Labo.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db=new ApplicationDbContext();
        public ActionResult Index()
        {
            var vm = new ViewModel()
            {
                Expiriments = db.Expiriments.ToList()
            };
            return View(vm);
        }
        //dffdfdsfdsfs
        public ActionResult chimie()
        {
            var vm = new ViewModel()
            {
                Expiriments = db.Expiriments.ToList()
            };
            return View("chimie",vm);
        }
        //dffdfdsfdsfs
        public ActionResult science()
        {
            var vm = new ViewModel()
            {
                Expiriments = db.Expiriments.ToList()
            };
            return View("Science",vm);
        }
        //dffdfdsfdsfs
        public ActionResult physic()
        {
            var vm = new ViewModel()
            {
                Expiriments = db.Expiriments.ToList()
            };
            return View("physic",vm);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}