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
            //TEST SEGMENT
            if(ca.UserID != User.Identity.GetUserId())
            {
                Console.WriteLine("CustomerAccountController-Post-Create UserID is null");
                ca.UserID = User.Identity.GetUserId();

                if (ca.UserID != User.Identity.GetUserId())
                {
                    Console.WriteLine("CustomerAccountController-Post-Create UserID assignment failed");
                }
                else
                {
                    Console.WriteLine("UserID assigned as: " + ca.UserID);
                }
            }
            //END TEST SEGMENT

            //If model is valid, add entry to table and save changes to the db.
            if(ModelState.IsValid)
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

            /*TEST SEGMENT
            var customerAcc = db.CustomersAccounts;
            var userAcc = customerAcc.Where(a => a.UserID == uid && a.AccountNum == id);
            */
            var uid = User.Identity.GetUserId();
            CustomerAccount acc = await db.CustomersAccounts.FindAsync(uid,id);
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

        public async Task<ActionResult> Details(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Find all Transactions matching the account number (id)
            var ctList = db.CustomerTransactions.Where(a => a.AccountNum == id);

            //Return as a list
            return View(await ctList.ToListAsync());
        }

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

        public async Task<ActionResult> CreateGoal()
        {
            var uid = User.Identity.GetUserId();
            
            //Get Customer Accounts tied to UserID
            var customerAccounts = db.CustomersAccounts.Where(u => u.UserID == uid);
            List<CustomerAccount> customerAccs = await customerAccounts.ToListAsync();
            List<SelectListItem> caList = new List<SelectListItem>();

            //For each account number tied to the User's UserID, save the account number
            foreach (var acc in customerAccs)
            {
                caList.Add(new SelectListItem { Text = acc.AccountNum, Value = acc.AccountNum });
            }

            //Save account numbers for display in CreateGoal Dropdownlist
            ViewData["accounts"] = caList;

            return View();
        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGoal([Bind(Include = "AccountNum,GoalType, GoalPeriod,StartValue,LimitValue,Description")] Goal userGoal)
        {
            if (ModelState.IsValid)
            {
                var uid = User.Identity.GetUserId();
                userGoal.UserID = uid;
                db.Goals.Add(userGoal);
                userGoal.StartDate = (DateTime)userGoal.StartDate;
                userGoal.EndDate = (DateTime)userGoal.EndDate;
                await db.SaveChangesAsync();

                RedirectToAction("AccountGoals", "Account", userGoal.AccountNum);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        */
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> AccountDetails(String id)
        {
            var model = new AccountDetailsViewModel();

            //if (String.IsNullOrEmpty(id))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //Find all Transactions matching the account number (id)
            var ctList = db.CustomerTransactions.Where(a => a.AccountNum == id);

            //Return as a list
            model.CustomerTransactions = await ctList.ToListAsync();

            // create set of datapoints
            List<DataPoint> lineDataPoints = new List<DataPoint>();


            lineDataPoints.Add(new DataPoint(1388514600000, 102.1));
            lineDataPoints.Add(new DataPoint(1391193000000, 104.83));
            lineDataPoints.Add(new DataPoint(1393612200000, 104.04));
            lineDataPoints.Add(new DataPoint(1396290600000, 104.87));
            lineDataPoints.Add(new DataPoint(1398882600000, 105.71));
            lineDataPoints.Add(new DataPoint(1401561000000, 108.37));
            lineDataPoints.Add(new DataPoint(1404153000000, 105.23));
            lineDataPoints.Add(new DataPoint(1406831400000, 100.05));
            lineDataPoints.Add(new DataPoint(1409509800000, 95.85));
            lineDataPoints.Add(new DataPoint(1412101800000, 86.08));
            lineDataPoints.Add(new DataPoint(1414780200000, 76.99));
            lineDataPoints.Add(new DataPoint(1417372200000, 60.7));
            lineDataPoints.Add(new DataPoint(1420050600000, 47.11));
            lineDataPoints.Add(new DataPoint(1422729000000, 54.79));
            lineDataPoints.Add(new DataPoint(1425148200000, 52.83));
            lineDataPoints.Add(new DataPoint(1427826600000, 57.54));
            lineDataPoints.Add(new DataPoint(1430418600000, 62.51));
            lineDataPoints.Add(new DataPoint(1433097000000, 61.31));
            lineDataPoints.Add(new DataPoint(1435689000000, 54.34));
            lineDataPoints.Add(new DataPoint(1438367400000, 45.69));
            lineDataPoints.Add(new DataPoint(1441045800000, 46.28));
            lineDataPoints.Add(new DataPoint(1443637800000, 46.96));
            lineDataPoints.Add(new DataPoint(1446316200000, 43.11));
            lineDataPoints.Add(new DataPoint(1448908200000, 36.57));
            lineDataPoints.Add(new DataPoint(1451586600000, 29.78));
            lineDataPoints.Add(new DataPoint(1454265000000, 31.03));
            lineDataPoints.Add(new DataPoint(1456770600000, 37.34));
            lineDataPoints.Add(new DataPoint(1459449000000, 40.75));
            lineDataPoints.Add(new DataPoint(1462041000000, 45.94));

            model.LineChartDataPoints = lineDataPoints;

            List<PieDataPoint> pieDataPoints = new List<PieDataPoint>();

            pieDataPoints.Add(new PieDataPoint("Housing", 25));
            pieDataPoints.Add(new PieDataPoint("Utilities", 10));
            pieDataPoints.Add(new PieDataPoint("Food", 10));
            pieDataPoints.Add(new PieDataPoint("Recreation", 5));
            pieDataPoints.Add(new PieDataPoint("Savings", 10));
            pieDataPoints.Add(new PieDataPoint("Personal Care", 10));
            pieDataPoints.Add(new PieDataPoint("Health", 5));
            pieDataPoints.Add(new PieDataPoint("Debt", 5));
            pieDataPoints.Add(new PieDataPoint("Transportation", 10));
            pieDataPoints.Add(new PieDataPoint("Others", 10));

            model.PieChartDataPoints = pieDataPoints;

            return View(model);
        }
    }

}