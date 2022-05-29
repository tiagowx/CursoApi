using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using curso.api.Models.Validations;
using curso.api.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace curso.api.Models.Filters
{
    public class ValidationModelStateCustom : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context) {
            if (!context.ModelState.IsValid)
            {
                var validateFieldViewModel = new ValidateFieldViewModelOutput(context.ModelState
                        .SelectMany(sm => sm.Value.Errors)
                        .Select(s => s.ErrorMessage));
                        context.Result = new BadRequestObjectResult(validateFieldViewModel);
            }
        }
    }
}