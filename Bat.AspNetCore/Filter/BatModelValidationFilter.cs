using System;
using Bat.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bat.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BatModelValidationAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new Response<object> { Message = context.ModelState.GetModelError() });
                return Task.CompletedTask;
            }
            else
                return base.OnActionExecutionAsync(context, next);
        }

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    if (!context.ModelState.IsValid)
        //    {
        //        //var errorMessage = string.Empty;
        //        //foreach (var error in context.ModelState)
        //        //    errorMessage += $"Key:{error.Key} Value:{error.Value} | ";
        //        //FileLoger.Message(context.ModelState.GetModelError());

        //        context.Result = new BadRequestObjectResult(context.ModelState);
        //    }
        //}
    }
}