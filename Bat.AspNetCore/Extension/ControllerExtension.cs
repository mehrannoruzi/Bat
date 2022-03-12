using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bat.AspNetCore;

public static class ControllerExtension
{
    public static string RenderViewToString<TModel>(this Controller controller, string viewNameOrPath, TModel model)
    {
        if (string.IsNullOrEmpty(viewNameOrPath)) viewNameOrPath = controller.ControllerContext.ActionDescriptor.ActionName;
        controller.ViewData.Model = model;

        using StringWriter writer = new();
        try
        {
            IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            ViewEngineResult viewResult = null;
            if (viewNameOrPath.EndsWith(".cshtml"))
                viewResult = viewEngine.GetView(viewNameOrPath, viewNameOrPath, false);
            else
                viewResult = viewEngine.FindView(controller.ControllerContext, viewNameOrPath, false);

            if (!viewResult.Success) return $"A view with the name '{viewNameOrPath}' could not be found";
            ViewContext viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                writer,
                new HtmlHelperOptions()
            );

            viewResult.View.RenderAsync(viewContext);
            return writer.GetStringBuilder().ToString();
        }
        catch (Exception exc)
        {
            return $"Exception - {exc.Message}";
        }
    }

    public static async Task<string> RenderViewToStringAsync<TModel>(this Controller controller, string viewNameOrPath, TModel model)
    {
        if (string.IsNullOrEmpty(viewNameOrPath)) viewNameOrPath = controller.ControllerContext.ActionDescriptor.ActionName;
        controller.ViewData.Model = model;

        using StringWriter writer = new();
        try
        {
            IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            ViewEngineResult viewResult = null;
            if (viewNameOrPath.EndsWith(".cshtml"))
                viewResult = viewEngine.GetView(viewNameOrPath, viewNameOrPath, false);
            else
                viewResult = viewEngine.FindView(controller.ControllerContext, viewNameOrPath, false);

            if (!viewResult.Success) return $"A view with the name '{viewNameOrPath}' could not be found";
            ViewContext viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return writer.GetStringBuilder().ToString();
        }
        catch (Exception exc)
        {
            return $"Exception - {exc.Message}";
        }
    }
}