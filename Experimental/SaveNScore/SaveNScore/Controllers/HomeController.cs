using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaveNScore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult CustomerAccount()
        {
            //var userID = User.Identity.GetUserId(); //TEST: Show Unique UserID
            ViewBag.Message = "Accounts Page.";
            return View("Index", "CustomerAccount");
        }

        public ActionResult CustomerTransaction()
        {
            return View("Index", "CustomerTransaction");
        }
    }
}