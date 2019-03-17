using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaveNScore.Models;
using SaveNScore.ViewModels;

namespace SaveNScore.Controllers
{
    public class CustomerTransactionsController : Controller
    {
        // GET: CustomerTransactions
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (pageIndex.HasValue)
                pageIndex = 1;

            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "Date";

            return Content(string.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        [Route("CustomerTransactions/ByTransDate/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}/{day:regex(\\d{2}):range(1, 31)}")]

        public ActionResult ByTransDate(int year, int month, int day)
        {
            // add code to get and return all transactions on a certain day

            return Content(year + "/" + month + "/" + day);
        }

        public ActionResult _TransactionList()
        {
            var user = new CustomersController().CurrentCustomer();

            var transactions = new List<CustomerTransaction>
            {
                new CustomerTransaction {AccountNum = 1, Amount = 14.99, Date = DateTime.Now, Type = TransTypeEnum.Debit},
                new CustomerTransaction {AccountNum = 1, Amount = 20.00, Date = DateTime.Now, Type = TransTypeEnum.Credit}
            };

            var viewModel = new CustomerTransactionListViewModel
            {
                Customer = user,
                CustomerTransactions = transactions
            };

            return View(viewModel);
        }

    }
}