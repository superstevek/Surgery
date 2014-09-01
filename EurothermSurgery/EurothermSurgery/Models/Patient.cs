using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EurothermSurgery.Models
{
    public class Patient : IValidatableObject
    {
        //Unique Identity
        public int PatientId { get; set; }

        //Data Items
        [Required(ErrorMessage="Forename is required.")]
        [MinLength(2,ErrorMessage="Forename must be a minimum of 2 characters.")]
        public string Forename { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [MinLength(3,ErrorMessage="Surname must be a minimum of 3 characters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "A Date of Birth is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name="Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name="Notes")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        //Custom Validator to stop dupicate patients being added
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PatientId == 0 && Registering.Instance.PatientExists(Forename, Surname, DateOfBirth))
            {
                yield return new ValidationResult("Patient already registered.");
            }

        }
    }
}