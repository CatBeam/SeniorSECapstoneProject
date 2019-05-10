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

        // index accounts when logged in
        public async Task<ActionResult> Index()
        {
            //Create DB CustomerAccount Table Instance
            var customerAccs = db.CustomersAccounts;

            //QUERY: Get all CustomerAccounts tied to this UserID
            var uid = User.Identity.GetUserId();
            var userAccs = customerAccs.Where(u => u.UserID == uid);

            return View(await userAccs.ToListAsync());
        }

        // display transactions on index page for single page use
        [HttpGet]
        public async Task<ActionResult> IndexTransactions(string accountId)
        {
            var model = new AccountDetailsViewModel();

            // logic for getting model and data
            var transactions = db.CustomerTransactions;
            var customerAccs = db.CustomersAccounts;


            // check if user is authorized to access account, if they arent dont let them see account info
            var uid = User.Identity.GetUserId();
            if (!customerAccs.Where(u => u.UserID == uid && u.AccountNum == accountId).Any())
            {
                return PartialView();
            }
            else
            {
                var finalTransactions = transactions.Where(t => t.AccountNum == accountId).OrderByDescending(t => t.TransactionDate);
                model.CustomerTransactions = await finalTransactions.ToListAsync();

                return PartialView("_IndexTransactions", model);
            }
        }

        [HttpGet]
        // display achievements for single page use on index
        public async Task<ActionResult> IndexAchievements()
        {
            var uid = User.Identity.GetUserId();
            await UserUtility.UpdateAchievements(User.Identity.GetUserId());
            //Get All User Achievements
            var userAchievements = db.Achievements.Where(u => u.UserID == uid);

            return PartialView("_IndexAchievements", await userAchievements.ToListAsync());
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