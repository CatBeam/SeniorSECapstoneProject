using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaveNScore.Models;

namespace SaveNScore.Controllers
{
    public class CustomersController : Controller
    {
        // NEEDFIX: References deleted Customer table - 4/7/19
        /*
        public Customer CurrentCustomer()
        {
            var customer = new Customer() { FirstName = "Steve", LastName = "Kesteverson" };
            return (customer);
        }
        
        // GET: Users
        public ActionResult _DisplayCurrentCustomer()
        {
            // Sample user hardcoded, will later pull from elsewhere
            var customer = CurrentCustomer();
            
            return View(customer);

            // Other actions which can be returned: (not exhaustive)
            // return Content("Hello World!");
            // return HTTPNotFound();
            // return new EmptyResult();
            // return RedirectToAction( "Index", "Home", {params})
        }
        */

        public ActionResult EditId(int id)
        {
            return Content("id=1");
        }
    }
}