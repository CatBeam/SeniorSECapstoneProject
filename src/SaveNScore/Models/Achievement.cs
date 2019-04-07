using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveNScore.Models
{
    public class Achievement
    {
        public int UserID { get; set; }
        public int AchievementID { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }


    }
}