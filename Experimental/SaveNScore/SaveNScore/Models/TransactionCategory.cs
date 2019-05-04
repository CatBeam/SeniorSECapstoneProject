using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaveNScore.Models
{
    public class TransactionCategory
    {
        [Key]
        [Editable(false)]
        public int Id { get; set; }

        [DisplayName("Transaction Description")]
        [Required(ErrorMessage = "Required")]
        public String TransDescription{ get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Spending Category")]
        public SpendingCategory SpendingCategory { get; set; }
    }
    public enum SpendingCategory
    {
        Housing,
        Utilities,
        Food,
        Transportation,
        Recreation,
        Savings,
        Personal,
        Health,
        Debt,
        Other
    }
}