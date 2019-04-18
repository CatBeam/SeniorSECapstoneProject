using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaveNScore.Models
{
    //TODO: Code in at User Creation
    //Change everything but UserID to readonly?
    public class Achievement
    {
        [Key, Column(Order = 0)]
        public string UserID { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("Achievement Number")]
        public int AchievementNum { get; set; }

        [DisplayName("Completion Status")]
        public bool Completed { get; set; }

        public string Description { get; set; }

    }
}