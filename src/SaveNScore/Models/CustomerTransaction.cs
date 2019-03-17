using System;

namespace SaveNScore.Models
{
    public class CustomerTransaction
    {
        public int Id { get; set; }
        public int AccountNum { get; set; }
        public double Amount { get; set; }
        public TransTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}