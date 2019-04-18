using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaveNScore.Models
{
    public class Customer
    {
        [Key]
        [Editable(false)]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public String UserID { get; set; }

        [Required(ErrorMessage = "Required")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        public String LastName { get; set; }
    }
}