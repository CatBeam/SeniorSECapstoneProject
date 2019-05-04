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
    public class CustomerAccountController : Controller
    {
        //Create DB Instance
        private ApplicationDbContext db = new ApplicationDbContext();


        /*START OF HOME/CUSTOMERACCOUNT METHODS */

        // GET: CustomerAccount
        public async Task<ActionResult> Index()
        {
            //Create DB CustomerAccount Table Instance
            var customerAccs = db.CustomersAccounts;

            //QUERY: Get all CustomerAccounts tied to this UserID
            var uid = User.Identity.GetUserId();
            var userAccs = customerAccs.Where(u => u.UserID == uid);

            return View(await userAccs.ToListAsync());
        }

        public ActionResult Create()
        {
            return View();
        }


        //TODO: Exceptions for PK and other DB violations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AccountNum,AccountType,Balance")] CustomerAccount ca)
        {
            //If model is valid, add entry to table and save changes to the db.
            if (ModelState.IsValid)
            {
                String uid = User.Identity.GetUserId(); //Hardcode to USERID
                ca.UserID = uid;
                db.CustomersAccounts.Add(ca);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "CustomerAccount");
            }

            return View(ca);
        }

        //id = AccountNum 
        //Note: since the User id is also apart of the PK, we used in the FindAsync()
        public async Task<ActionResult> Delete(String id)
        {
            //If Account Number cannot be determined, error out
            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var uid = User.Identity.GetUserId();
            CustomerAccount acc = await db.CustomersAccounts.FindAsync(uid, id);
            return View(acc);
        }


        //Going to have to figure out something with this param being an int, credit card numbers will overflow
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            //If Account Number cannont be determined, error out
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var uid = User.Identity.GetUserId();

            //Find CustomerAccount to delete, delete it, save DB changes
            CustomerAccount accToDelete = await db.CustomersAccounts.FindAsync(uid, id.ToString());
            db.CustomersAccounts.Remove(accToDelete);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "CustomerAccount");
        }
        
        /*START OF HOME/CUSTOMERACCOUNT/ACCOUNTGOALS METHODS */


        [HttpGet]
        [ActionName("AccountGoals")] //In case we change the name of the function later on
        public async Task<ActionResult> AccountGoals(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Find all Goals matching the account number
            var goalsList = db.Goals.Where(a => a.AccountNum == id);

            //Return goals as a list
            return View(await goalsList.ToListAsync());
        }

        public async Task<ActionResult> CreateSingleGoal()
        {
            var uid = User.Identity.GetUserId();

            //Get User's Accounts as SelectListItem
            ViewData["accounts"] = await UserUtility.GetUserAccountsList(db, uid);

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSingleGoal([Bind(Include = "AccountNum,StartDate,EndDate,StartValue,LimitValue,Description")] Goal userGoal)
        {
            if (ModelState.IsValid)
            {
                //Get User's ID and attach to Goal
                var uid = User.Identity.GetUserId();
                userGoal.UserID = uid;
                userGoal.GoalType = GoalTypeEnum.SaveByDate;
                userGoal.GoalPeriod = GoalPeriodEnum.Single;
                userGoal.Completed = false;

                //Add Goal to DB and Save Changes
                db.Goals.Add(userGoal);
                await db.SaveChangesAsync();
                
                return Redirect("AccountGoals/" + userGoal.AccountNum);
            }

            /*If Model is NOT valid*/
            //Get User's Accounts as SelectListItem
            ViewData["accounts"] = await UserUtility.GetUserAccountsList(db, User.Identity.GetUserId());

            //Return to view for retry
            return View(userGoal);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> DeleteGoal(int? id)
        {
            Goal goal = await db.Goals.FindAsync(id);
            return View(goal);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteGoal(int id)
        {
            //Find Goal to Delete
            var goalsTable = db.Goals;
            Goal goalToDelete = await goalsTable.FindAsync(id);
            
            //Extract account number for redirect
            string accountNumber = goalToDelete.AccountNum;

            //Remove Goal and save changes
            db.Goals.Remove(goalToDelete);
            await db.SaveChangesAsync();

            return RedirectToAction("AccountGoals/" + accountNumber, "CustomerAccount");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> Details(String id)
        {
            var model = new AccountDetailsViewModel();

            //if (String.IsNullOrEmpty(id))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //Find all Transactions matching the account number (id)
            // TODO: Sort initial list from oldest to newest
            var ctList = db.CustomerTransactions
                .Where(a => a.AccountNum == id)
                .OrderBy(data => data.TransactionDate);

            //Return as a list
            model.CustomerTransactions = await ctList.ToListAsync();

            // create set of datapoints

            List<DataPoint> lineDataPoints = new List<DataPoint>();

            // TODO: Get starting balance from database
            var startingBalance = db.CustomersAccounts
                .Where(a => a.AccountNum == id)
                .Select(a => a.Balance)
                .FirstOrDefault();
            var currBalance = startingBalance;
            var tempDate = new DateTime();
            var utcDate = new DateTime(1969, 12, 31);
            var transCategories = db.TransactionCategories;

            tempDate = model.CustomerTransactions[0].TransactionDate;

            foreach (var customerTransaction in model.CustomerTransactions)
            {
                if (tempDate.Date != customerTransaction.TransactionDate.Date)
                {
                    lineDataPoints.Add(new DataPoint((tempDate.Subtract(utcDate).TotalMilliseconds), currBalance));
                    tempDate = customerTransaction.TransactionDate;
                }
                    if (customerTransaction.TransactionType == TransactionTypeEnum.Credit)
                    {
                        currBalance = currBalance + customerTransaction.Amount;
                    }
                    else if (customerTransaction.TransactionType == TransactionTypeEnum.Debit)
                    {
                        currBalance = currBalance - customerTransaction.Amount;
                    }  
            }

            lineDataPoints.Add(new DataPoint((tempDate.Subtract(utcDate).TotalMilliseconds), currBalance));

            model.LineChartDataPoints = lineDataPoints;

            List<PieDataPoint> pieDataPoints = new List<PieDataPoint>();

            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Housing), 25.0));
            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Utilities), 10.0));
            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Food), 10.0));
            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Transportation), 10.0));
            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Recreation), 10.0));
            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Savings), 10.0));
            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Personal), 10.0));
            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Health), 10.0));
            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Debt), 10.0));
            pieDataPoints.Add(new PieDataPoint(nameof(SpendingCategory.Other), 10.0));

            model.PieChartDataPoints = pieDataPoints;
            List<Decimal> amtByCat = new List<Decimal>();
            for (int i = 0; i<10; i++)
            {
                amtByCat.Add(0);
            }

            // loop through transactions, update amount for each category
            foreach (var customerTransaction in model.CustomerTransactions)
            {
                if (customerTransaction.TransactionType == TransactionTypeEnum.Debit)
                {
                    var category = transCategories
                        .Where(t => t.TransDescription == customerTransaction.Description)
                        .Select(t => t.SpendingCategory)
                        .FirstOrDefault();

                    int categoryInt = Convert.ToInt32(category);
                    amtByCat[categoryInt] += customerTransaction.Amount;
                }
            }

            // calcuate total spending
            decimal sum = amtByCat.Sum();

            // calculate spending percentage by category
            // add percentages to datapoints

            List<Double> percentByCat = new List<double>(10);
            for (int i = 0; i < 10; i++)
            {
                percentByCat.Add(0);
            }

            for (int i = 0; i<10; i++)
            {
                percentByCat[i] = Math.Round(((double)(amtByCat[i] / sum) * 100), 2);
                pieDataPoints[i].Y = percentByCat[i];

            }

            return View(model);
        }
    }

}