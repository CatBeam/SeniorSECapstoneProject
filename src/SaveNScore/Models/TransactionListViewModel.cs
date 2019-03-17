using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaveNScore.Models;

namespace SaveNScore.ViewModels
{
    public class TransactionListViewModel
    {
        public Customer Customer { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}