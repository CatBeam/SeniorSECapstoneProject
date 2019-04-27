using Microsoft.AspNet.Identity;
using SaveNScore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SaveNScore.Controllers
{
    public class GoalController : Controller
    {

        //Create DB Instance
        private ApplicationDbContext db = new ApplicationDbContext();
        
        
        // GET: Goal
        public async Task<ActionResult> Index()
        {
            //Get UserID, Query all goals tied to UserID
            var uid = User.Identity.GetUserId();
            var userGoals = db.Goals.Where(u => u.UserID == uid);

            return View(await userGoals.ToListAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GoalType, GoalPeriod,StartDate,EndDate,StartValue,LimitValue,Description")] Goal userGoal)
        {
            if (ModelState.IsValid)
            {
                //Tie new goal to UserID, Add to DB, and Save
                String uid = User.Identity.GetUserId().ToString();
                userGoal.UserID = uid;
                db.Goals.Add(userGoal);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", "Goal");
            }

            return View(userGoal);
        }
    }
}