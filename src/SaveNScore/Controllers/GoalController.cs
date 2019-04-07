using SaveNScore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SaveNScore.Controllers
{
    public class GoalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User's Goals
        public async Task<ActionResult> Index()
        {
            //Get User's goals from the DB, convert to list, ret to view
            var goals = db.Goals;
            return View(await goals.ToListAsync());
        }


        // GET: User's Goal X details
        public async Task<ActionResult> Details(int? id)
        {
            //Note: Change to Error message and redirect?
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Note: Change to raw query if needed
            Goal goal = await db.Goals.FindAsync(id);

            if (goal == null)
                return HttpNotFound();

            return View(goal);
        }

        // GET: Goal/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Goal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GoalDescription,GoalValue,StartValue,StartDate,EndDate")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                //Assign User's ID to param 'goal' here
                db.Goals.Add(goal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(goal); //Send goal back to the view
        }
        // GET: Goal/Edit/{id}
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Goal goal = await db.Goals.FindAsync(id);

            //Note: Change to Error message and redirect?		
            if (goal == null)
                return HttpNotFound();

            return View(goal);
        }

        //POST: Goal/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id)
        {
            string[] bindingFields = new string[] { "UserID", "GoalID", "StartValue", "GoalValue", "StartDate", "EndDate" };

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var goalToAlter = await db.Goals.FindAsync(id);

            //Note: Change to Error message and redirect?	
            if (goalToAlter == null)
                return HttpNotFound();

            if (TryUpdateModel(goalToAlter, bindingFields))
            {
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
                {
                    var entry = e.Entries.Single();
                    var vals = (Goal)entry.Entity;
                    var dbEntry = entry.GetDatabaseValues();

                    if (dbEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Cannot save changes, things have gone horribly, horribly, horribly wrong somewhere.");
                    }
                    //ADD ELSE CLAUSE FOR MANUAL UPDATE
                    //else
                    //{
                    //}				
                }
            }
            return View(goalToAlter);
        }

    }
}