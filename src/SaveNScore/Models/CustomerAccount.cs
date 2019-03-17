using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveNScore.Models
{
    public class CustomerAccount
    {
        public int Id { get; set; }
        public string AccountNum { get; set; }
        public CustomerAccountTypeEnum CustomerAccountType { get; set; }
        public decimal Balance { get; set; }
    }
}