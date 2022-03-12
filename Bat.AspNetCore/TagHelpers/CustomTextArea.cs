using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore;

[HtmlTargetElement("custom-textarea")]
public class CustomTextArea : BaseTagHelperModel
{
    public string PlaceHolder { get; set; } = "";

    public byte Rows { get; set; }

    public byte Cols { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        output.PreContent.SetHtmlContent($"<div class='form-group group-{Name.ToLower()} {WrapperClass}'>");
        if (LabelVisibility)
        {
            var lbl = new TagBuilder("label");
            lbl.Attributes.Add("for", Name);
            lbl.Attributes.Add("class", LabelClass);
            lbl.InnerHtml.Append(Label);
            output.Content.AppendHtml(lbl);
        }

        var textArea = new TagBuilder("textarea");
        textArea.Attributes.Add("name", Name);
        textArea.Attributes.Add("id", Id);
        textArea.Attributes.Add("cols", Cols.ToString());
        textArea.Attributes.Add("rows", Rows.ToString());
        textArea.Attributes.Add("placeholder", PlaceHolder);
        if (Readonly) textArea.Attributes.Add("readonly", "readonly");
        if (string.IsNullOrWhiteSpace(Class)) textArea.Attributes.Add("class", "form-control");
        else textArea.Attributes.Add("class", Class);
        foreach (var attr in context.AllAttributes.Where(x => x.Name.StartsWith("text-area-")))
            textArea.Attributes.Add(attr.Name.Replace("text-area-", ""), attr.Value.ToString());
        output.Content.AppendHtml(textArea);
        output.PostContent.SetHtmlContent($"</div>");
    }
}