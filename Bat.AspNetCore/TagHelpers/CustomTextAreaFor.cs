using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore;

[HtmlTargetElement("custom-textarea-for", Attributes = "for")]
public class CustomTextAreaFor : FormGroupModel
{
    private readonly IHtmlGenerator _generator;

    public CustomTextAreaFor(IHtmlGenerator generator)
    {
        _generator = generator;
    }

    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public ModelExpression For { get; set; }

    public byte Rows { get; set; }

    public byte Cols { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        output.PreContent.SetHtmlContent($"<div class='form-group group-{For.Name.ToLower()} {WrapperClass}'>");
        if (LabelVisibility)
        {
            var label = _generator.GenerateLabel(ViewContext, For.ModelExplorer, For.Name, null, new { @class = LabelClass ?? string.Empty });
            output.Content.AppendHtml(label);
        }

        var textArea = _generator.GenerateTextArea(ViewContext, For.ModelExplorer, For.Name, Rows, Cols, new { @class = Class });
        if (Readonly) textArea.Attributes.Add("readonly", "readonly");
        foreach (var attr in context.AllAttributes.Where(x => x.Name.StartsWith("text-area-")))
            textArea.Attributes.Add(attr.Name.Replace("text-area-", ""), attr.Value.ToString());
        var validation = _generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, null, null, new { @class = "form-text" });
        output.Content.AppendHtml(textArea);
        output.Content.AppendHtml(validation);
        output.PostContent.SetHtmlContent($"</div>");
    }
}