﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SaveNScore.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using SaveNScore.Models;
using System.Net;

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
        
    }
}