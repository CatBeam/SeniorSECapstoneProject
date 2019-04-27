using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaveNScore.Models
{
    public class CustomerTransaction
    {

        [Key]
        [Editable(false)]
        public int TransactionID { get; set; }

        //[ForeignKey("CustomerAccount")]
        [DisplayName("Account Number")]
        [Required(ErrorMessage = "Required")]
        public string AccountNum { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Required")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Range(0.01, (double)Int32.MaxValue)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Transaction Type")]
        public TransactionTypeEnum TransactionType { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public enum TransactionTypeEnum
    {
        Debit, Credit
    }
}