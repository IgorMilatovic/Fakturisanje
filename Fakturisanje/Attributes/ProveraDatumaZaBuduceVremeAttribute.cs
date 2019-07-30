using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fakturisanje.Attributes
{
    public class ProveraDatumaZaBuduceVremeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var vreme = Convert.ToDateTime(value);

            if (vreme > DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }

            else
            {
                return ValidationResult.Success;
            }
        }
    }
}