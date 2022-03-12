using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore;

[HtmlTargetElement("custom-checkbox-for", Attributes = "for")]
public class CustomCheckBoxFor : FormGroupModel
{
    private readonly IHtmlGenerator _generator;

    public CustomCheckBoxFor(IHtmlGenerator generator)
    {
        _generator = generator;
    }

    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public ModelExpression For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        output.PreContent.SetHtmlContent($"<div class='form-group group-{For.Name.ToLower()} {WrapperClass}'> <div class='i-checks'><label class='{LabelClass}'>");
        var chb = _generator.GenerateCheckBox(ViewContext, For.ModelExplorer, For.Name, (bool)For.Model, new { @class = Class });
        if (Readonly) chb.Attributes.Add("disabled", "disabled");
        foreach (var attr in context.AllAttributes.Where(x => x.Name.StartsWith("chb-")))
            chb.Attributes.Add(attr.Name.Replace("chb-", ""), attr.Value.ToString());
        var validation = _generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, null, null, new { @class = "form-text" });
        output.Content.AppendHtml(chb);
        output.Content.AppendHtml($"<i></i>{For.Metadata.DisplayName}</label></div>");
        output.Content.AppendHtml(validation);
        output.PostContent.SetHtmlContent($"</div>");
    }
}