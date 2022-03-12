using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore;

public class CustomButton : TagHelper
{
    public string Text { get; set; }

    public string Icon { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        var classValue = string.Empty;
        var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
        if (classAttr != null)
        {
            classValue = classAttr.Value.ToString();
            output.Attributes.Remove(classAttr);
        }
        output.Attributes.Add("class", $"btn {classValue} btn-action");
        output.Content.SetHtmlContent($"<span class='text'>{Text}</span>");
        output.Content.AppendHtml($"<div class='icon'><i class='{Icon}'></i></div>");
    }
}