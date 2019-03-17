﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveNScore.Models
{
    public class Budget
    {
        public int CustomerID { get; set; }
        public int BudgetID { get; set; }
        public decimal StartAmount { get; set;}
        public decimal RemainingAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}
    }
}