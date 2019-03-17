using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaveNScore.Models;

namespace SaveNScore.Controllers
{
    public class BudgetController : Controller
    {
        //TEST BUDGETS
        IList<Models.Budget> testBudgets = new List<Models.Budget>(){
            new Models.Budget() { BudgetID = 01, CustomerID = 01, StartDate = new DateTime(2018, 12, 25), EndDate = new DateTime(2019,12,25), StartAmount = (Decimal)1500, RemainingAmount = (Decimal)1000 },
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
            new Models.Budget() {BudgetID = 02, CustomerID = 01, StartDate = new DateTime(2019,1,1), EndDate = new DateTime(2019,1,31), StartAmount = (Decimal)750, RemainingAmount =  (Decimal)786.56},
        };
        // GET: Budget
        public ActionResult Index()
        {
            return View(testBudgets);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Budget newBudget)
        {
            testBudgets.Add(newBudget);
            return RedirectToAction("Index");
        }
    }
}