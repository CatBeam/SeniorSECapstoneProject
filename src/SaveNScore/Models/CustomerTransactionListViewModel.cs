using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaveNScore.Models;

namespace SaveNScore.ViewModels
{
    public class CustomerTransactionListViewModel
    {
        public Customer Customer { get; set; }
        public List<CustomerTransaction> CustomerTransactions { get; set; }
    }
}