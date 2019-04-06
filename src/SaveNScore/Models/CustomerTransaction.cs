using System;

namespace SaveNScore.Models
{
    public class CustomerTransaction
    {
        public int Id { get; set; }
        public String AccountNum { get; set; }
        public double Amount { get; set; }
        public TransTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }
    }
}