using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaveNScore.Models
{
    // Checks that any decimal value input from the User is valid
    // Usage: [DecimalRange( MinValue = #.##, MaxValue = #.## )]
    public class DecimalRange : ValidationAttribute
    {
        //TODO: FIX VALIDATION LOGIC
        private decimal _MaxValue;
        private decimal _MinValue;

        public string GetErrorMessage()
        {
            return $"This value must be a positive number.";
        }


        public DecimalRange(Int32 max)
        {
            this._MaxValue = (decimal)0.01;
            this._MinValue = (decimal)max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            decimal ObjVal = (decimal)value;
            if ((ObjVal >= _MinValue) && (ObjVal <= _MaxValue))
            {
                return ValidationResult.Success;
            }


            var errorMessage = FormatErrorMessage(GetErrorMessage());
            return new ValidationResult(errorMessage);

            

            /*
            if (String.IsNullOrEmpty(value.ToString()))
                return false;

            decimal ObjValue = (decimal)value;
            return (ObjValue >= this.MinValue && ObjValue <= this.MaxValue);
            */
        }
    }
}