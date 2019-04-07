using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SaveNScore.Models;

namespace SaveNScore.Controllers
{
    public class BudgetController : Controller
    {
        //Make Db Instance
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Budget
        public async Task<ActionResult> Index()
        {
            //var budgets = db.Budgets.Include(b => b.PrimaryHolder);
            //Make Budget Table instance
            var budgets = db.Budgets;
            //search ID Code Here
            return View(await budgets.ToListAsync());
        }

        // GET: Budget/Details/{id}
        public async Task<ActionResult> Details(int? id)
        {
            //Note: Change to Error message and redirect?
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Note: Change to raw query if needed
            Budget budget = await db.Budgets.FindAsync(id);

            if (budget == null)
                return HttpNotFound();

            return View(budget);
        }

        // GET: Budget/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Budget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomerID,BudgetID,StartAmount,StartDate,EndDate")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                db.Budgets.Add(budget);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(budget);
        }

        // GET: Budget/Edit/{id}
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Budget budget = await db.Budgets.FindAsync(id);

            //Note: Change to Error message and redirect?		
            if (budget == null)
                return HttpNotFound();

            return View(budget);
        }

        //POST: Budget/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id)
        {
            string[] bindingFields = new string[] { "BudgetID", "StartAmount", "RemainingAmount", "StartDate", "EndDate" };

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var budgetToAlter = await db.Budgets.FindAsync(id);

            //Note: Change to Error message and redirect?	
            if (budgetToAlter == null)
                return HttpNotFound();

            if (TryUpdateModel(budgetToAlter, bindingFields))
            {
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
                {
                    var entry = e.Entries.Single();
                    var vals = (Budget)entry.Entity;
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
            return View(budgetToAlter);
        }

    }
}