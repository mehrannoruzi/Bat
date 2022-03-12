using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore;

[HtmlTargetElement("custom-select-for")]
public class CustomSelectFor : FormGroupModel
{
    private readonly IHtmlGenerator _generator;

    public CustomSelectFor(IHtmlGenerator generator)
    {
        _generator = generator;
    }

    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public string OptionalLabel { get; set; }

    public IList<SelectListItem> Items { get; set; } = new List<SelectListItem>();

    public ModelExpression For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        output.PreContent.SetHtmlContent($"<div class='form-group group-{For.Name.ToLower()} {WrapperClass}'>");
        if (LabelVisibility)
        {
            var label = _generator.GenerateLabel(ViewContext, For.ModelExplorer, For.Name, null, new { @class = LabelClass ?? string.Empty });
            output.Content.AppendHtml(label);
        }

        var select = _generator.GenerateSelect(ViewContext, For.ModelExplorer, OptionalLabel, For.Name, Items, new List<string>() { (For.Model ?? "").ToString() }, false, new { @class = Class });
        if (Readonly) select.Attributes.Add("disabled", "disabled");
        foreach (var attr in context.AllAttributes.Where(x => x.Name.StartsWith("select-")))
            select.Attributes.Add(attr.Name.Replace("select-", ""), attr.Value.ToString());
        var validation = _generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, null, null, new { @class = "form-text" });
        output.Content.AppendHtml(select);
        output.Content.AppendHtml(validation);
        output.PostContent.SetHtmlContent($"</div>");
    }
}