using Microsoft.AspNet.Identity;
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
    public class CustomerTransactionController : Controller
    {

        //Create DB Instance
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: CustomerTransaction
        public async Task<ActionResult> Index()
        {
            //Create DB CustomerTransactions Table Instance
            //Order table by Processing Date
            var customerTrans = db.CustomerTransactions.OrderBy(d => d.TransactionDate); ;
            
            //QUERY: Get all CustomerTransactions tied to this UserID
            var uid = User.Identity.GetUserId();
            var userTransactions = customerTrans.Where(u => u.UserID == uid);

            return View(await userTransactions.ToListAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AccountNum,Amount,TransactionType,Description")]CustomerTransaction ctrans)
        {
            //If model-state is valid, record data hidden from User
            //Create Entry, Save DB changes
            if(ModelState.IsValid)
            {
                var uid = User.Identity.GetUserId();
                ctrans.UserID = uid.ToString();
                ctrans.TransactionDate = DateTime.Now;

                //THIS TWO LINES ARE FOR TESTING, AND SHOULD BE ERASED
                //ONCE THE DB AUTO INC'S TransactionID
                Random random = new Random();
                ctrans.TransactionID = random.Next(0, Int32.MaxValue-1);


                db.CustomerTransactions.Add(ctrans);

                //STUB: Add Logic updating whichever account the transaction is applied to here.

                await db.SaveChangesAsync();
                return RedirectToAction("Index", "CustomerTransaction");
            }

            return View(ctrans);
        }

        //id = TransactionID
        public async Task<ActionResult> Delete(int? id)
        {
            //If Transaction doesn't exist, error out
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Query the DB CustomerTransactions Table
            //Find Target transaction and send to view
            CustomerTransaction targetTransaction = await db.CustomerTransactions.FindAsync(id);
            return View(targetTransaction);
        }

        //id = TransactionID
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Find Transaction to be deleted, delete it, save DB changes
            CustomerTransaction transToDelete = await db.CustomerTransactions.FindAsync(id);
            db.CustomerTransactions.Remove(transToDelete);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "CustomerTransaction");
        }

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