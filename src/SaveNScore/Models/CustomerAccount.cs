using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveNScore.Models
{
    public class CustomerAccount
    {
        // make ID and AccountNum a joint primary key
        [Key]
        [Column(Order = 1)]
        public string AccountNum { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }
        public CustomerAccountTypeEnum CustomerAccountType { get; set; }
        public decimal Balance { get; set; }
    }
}