namespace Bat.AspNetCore;

public interface IViewRenderService
{
    Task<string> RenderViewToString(string viewName, object model);
}