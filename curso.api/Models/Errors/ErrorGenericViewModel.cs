using System.Collections.Generic;

namespace curso.api.Models.Errors
{
    public class ErrorGenericViewModel
    {
        public IEnumerable<string> Errors { get; set; }

        public ErrorGenericViewModel(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
