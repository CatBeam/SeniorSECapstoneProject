using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaveNScore.Models
{
    public class Goal
    {
        [DisplayName("Shouldn't be seeing me...")] //TESTER
        public int UserID { get; set; }

        [DisplayName("Shouldn't be seeing me...")] //TESTER
        public int GoalID { get; set; }

        [DisplayName("Description")]
        public string GoalDescription { get; set; }

        [DisplayName("Goal Type")]
        public GoalType Type { get; set; }

        [DisplayName("Starting Value")]
        public decimal StartValue { get; set; }

        [DisplayName("Spending Limit")]
        public decimal GoalValue { get; set; }

        [DisplayName("Shouldn't be seeing me...")] //TESTER
        private bool ValidGoal { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("End Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }

    public enum GoalType
    {
        SaveByDate, RecurringSave, RecurringSpend
        // SaveByDate = Save X dollars before mm/dd/yyyy
        // Recurring = Save/Spend X dollars each week/month/year
    }


}