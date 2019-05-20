using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOs
{
    public class UserInputValidator : IValidatableObject
    {
        [Required]
        [MinLength(2)]
        public string MyStringValue { get; set; }

        public int MyIntValue { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MyIntValue < 18)
            {
                yield return new ValidationResult("MyIntValue must be greather than or equal to 18");
            }
        }
 
    }
}
