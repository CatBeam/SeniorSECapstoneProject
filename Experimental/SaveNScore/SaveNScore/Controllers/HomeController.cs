using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SaveNScore.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net;
using SaveNScore.ViewModels;
using Newtonsoft.Json;

namespace SaveNScore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            //Create DB CustomerAccount Table Instance
            var customerAccs = db.CustomersAccounts;

            //QUERY: Get all CustomerAccounts tied to this UserID
            var uid = User.Identity.GetUserId();
            var userAccs = customerAccs.Where(u => u.UserID == uid);

            return View(await userAccs.ToListAsync());
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