﻿using SaveNScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveNScore.ViewModels
{
    public class AccountDetailsViewModel
    {
        public List<CustomerTransaction> CustomerTransactions { get; set; }
        // datapoints for line chart
        public List<DataPoint> LineChartDataPoints { get; set; }
        // datapoints for pie chart
        public List<PieDataPoint> PieChartDataPoints { get; set; }
    }
}