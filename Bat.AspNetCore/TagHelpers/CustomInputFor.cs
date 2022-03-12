using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore;

[HtmlTargetElement("custom-input-for")]
public class CustomInputFor : FormGroupModel
{
    private readonly IHtmlGenerator _generator;

    public CustomInputFor(IHtmlGenerator generator)
    {
        _generator = generator;
    }

    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public InputType Type { set; get; }

    public string Placeholder { get; set; } = "";

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

        var input = Type == InputType.password
            ? _generator.GeneratePassword(ViewContext, For.ModelExplorer, For.Name, For.Model, new { @class = Class })
            : _generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.Model, null, new { @class = Class });
        if (Type != InputType.text && Type != InputType.password)
        {
            input.Attributes.Remove("type");
            input.Attributes.Add("type", Type.ToString());
        }

        if (Readonly) input.Attributes.Add("readonly", "readonly");
        if (!string.IsNullOrWhiteSpace(Placeholder)) input.Attributes.Add("placeholder", Placeholder);
        foreach (var attr in context.AllAttributes.Where(x => x.Name.StartsWith("input-")))
            input.Attributes.Add(attr.Name.Replace("input-", ""), attr.Value.ToString());
        var validation = _generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, null, null, new { @class = "form-text" });
        output.Content.AppendHtml(input);
        output.Content.AppendHtml(validation);
        output.PostContent.SetHtmlContent($"</div>");
    }
}