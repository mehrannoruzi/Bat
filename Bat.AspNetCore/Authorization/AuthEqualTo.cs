using Microsoft.AspNetCore.Mvc.Filters;

namespace Bat.AspNetCore
{
    public class AuthEqualTo : ActionFilterAttribute
    {
        public readonly string ActionName;
        public readonly string ControllerName;

        public AuthEqualTo(string controllerName, string actionName)
        {
            ActionName = actionName;
            ControllerName = controllerName;
        }
    }
}