using System.Collections.Generic;

namespace curso.api.Models.Validations
{
    public class ValidateFieldViewModelOutput
    {
        public IEnumerable<string> Errors { get; set; }

        public ValidateFieldViewModelOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
