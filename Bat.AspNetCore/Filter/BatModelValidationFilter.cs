using System;
using Bat.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bat.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BatModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //var errorMessage = string.Empty;
                //foreach (var error in context.ModelState)
                //    errorMessage += $"Key:{error.Key} Value:{error.Value} | ";
                FileLoger.Message(context.ModelState.GetModelError());

                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
            
        //}
    }
}