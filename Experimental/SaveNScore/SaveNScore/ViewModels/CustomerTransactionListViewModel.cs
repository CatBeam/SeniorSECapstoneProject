using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SaveNScore.Models  //Namespace kept as Model, class was moved here for organization
{
    public class CustomerTransactionListViewModel
    {
        public Customer Customer { get; set; }
        public List<CustomerTransaction> CustomerTransactions { get; set; }
    }
}