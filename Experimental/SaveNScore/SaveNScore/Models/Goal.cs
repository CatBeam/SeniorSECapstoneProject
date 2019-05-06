﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SaveNScore.Models
{
    /*TODO: Add logic to alter web form depending what GoalType
     * the User toggles.
     * If GoalType = Recurring
     *  Then the GoalPeriod dropdown should become visible and
     *  EndDate should be fixed
     * If GoalType =  SaveByDate
     *  Then GoalPeriod shouldn't be visible, set to null, and
     *  the User should enter the EndDate
     */


    public class Goal : IValidatableObject
    {
        //[ForeignKey("CustomerAccount")]
        public string UserID { get; set; }


        public string AccountNum { get; set; }
        [Key]
        public int GoalID { get; set; }

        [DisplayName("Goal Type")]
        [Required(ErrorMessage ="Required")]
        public GoalTypeEnum GoalType { get; set; }

        [DisplayName("Goal Period")]
        public GoalPeriodEnum GoalPeriod { get; set; }

        [DisplayName("Starting Amount")]
        [Range(0, (double)Int32.MaxValue)]
        [Required(ErrorMessage = "Required")]
        public decimal StartValue { get; set; }

        [DisplayName("Limit Amount")]
        [Range(0, (double)Int32.MaxValue)]
        [Required(ErrorMessage = "Required")]
        public decimal LimitValue { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Required")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Completion Date")]
        public DateTime EndDate { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }

        public bool Completed { get; set; }

        #region Validations
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            DateTime today = DateTime.Now;
            if (StartValue > LimitValue)
            {
                yield return new ValidationResult("Limiting Value must be greater than Starting Value");
            }

            if (StartValue < 0)
            {
                yield return new ValidationResult("The Starting Value cannot be a negative number");
            }


        }
        #endregion Validations

    }

    public enum GoalTypeEnum
    {
        [Display(Name = "Save By Date")]
        SaveByDate,

        Recurring
    }
    
    public enum GoalPeriodEnum
    {
        Weekly, Monthly, Yearly, Single
    }
}