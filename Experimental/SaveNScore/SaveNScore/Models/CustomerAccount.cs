using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity; //For testing with USER IDs
using System.Web.Mvc;
using SaveNScore.Controllers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SaveNScore.Models
{
    //TODO: Details Page -> List of all Account Transactions for the selected Account Number
    public class CustomerAccount
    {
        //[ForeignKey("Customer")]
        
        [Key, Column(Order = 0)]
        public string UserID { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Account Number")]
        public string AccountNum { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Account Type")]
        public CustomerAccountTypeEnum AccountType { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Column(TypeName = "money")]
        public decimal Balance { get; set; }

    }

    public enum CustomerAccountTypeEnum
    {
        Checking,
        Savings,
        CreditCard
    }

}