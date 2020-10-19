using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bat.AspNetCore
{
    public interface IViewRenderService
    {
        Task<string> RenderViewToString(string viewName, object model);
    }

    public class ViewRenderService : IViewRenderService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public ViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            _env = env;
            _serviceProvider = serviceProvider;
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<string> RenderViewToString(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var stringWriter = new StringWriter())
            {
                var viewResult = _razorViewEngine.GetView(_env.WebRootPath, viewName, false);
                if (viewResult.View == null) throw new ArgumentNullException($"{viewName} does not match any available view");
                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };
                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider), stringWriter, new HtmlHelperOptions());
                await viewResult.View.RenderAsync(viewContext);
                return stringWriter.ToString();
            }
        }
    }
}
