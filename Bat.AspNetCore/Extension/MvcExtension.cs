using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bat.AspNetCore;

public static class MvcExtension
{
    public static List<SelectListItem> ToSelectListFromDescription(this Enum @enum)
    {
        var values = from Enum e in Enum.GetValues(@enum.GetType()) select new { ID = e, Name = e.GetDescription() };
        return values.Select(x => new SelectListItem
        {
            Value = x.ID.ToString(),
            Text = x.Name.ToString()
        }).ToList();
    }

    public static List<SelectListItem> ToSelectListFromDescriptionAttribute(this Enum @enum)
    {
        var values = from Enum e in Enum.GetValues(@enum.GetType()) select new { ID = e, Name = e.GetDescription() };
        return values.Select(x => new SelectListItem
        {
            Value = x.ID.ToString(),
            Text = x.Name.ToString()
        }).ToList();
    }

    public static List<SelectListItem> ToSelectListItems(this IDictionary<object, object> keyValues)
        => keyValues.Select(x => new SelectListItem
        {
            Value = x.Key.ToString(),
            Text = x.Value.ToString()
        }).ToList();

    public static string GetModelError(this ModelStateDictionary modelState)
        => string.Join("|", modelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList());

}