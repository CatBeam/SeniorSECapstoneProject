using System;
using Newtonsoft.Json;

namespace SaveNScore.Models
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartAmount { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}