using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SaveNScore.Models
{
    public class Budget
    {
        [DisplayName("You shouldn't be seeing this.")] // TEST
        public int CustomerID { get; set; }

        [DisplayName("ID")]
        public int BudgetID { get; set; }
        
        [DisplayName("Start Amount")]
        [Range(0, int.MaxValue)]
        public decimal StartAmount { get; set;}

        [Range(0, int.MaxValue)]
        [DisplayName("Remaining Amount")]
        public decimal RemainingAmount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set;}

        [DisplayName("Description")]
        public string BudgetDescription { get; set; }
    }
}