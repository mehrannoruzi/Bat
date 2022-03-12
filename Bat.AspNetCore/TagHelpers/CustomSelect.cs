using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore;

[HtmlTargetElement("custom-select")]
public class CustomSelect : BaseTagHelperModel
{
    public string OptionalLabel { get; set; }

    public IEnumerable<SelectListItem> Items { get; set; } = new List<SelectListItem>();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        output.PreContent.SetHtmlContent($"<div class='form-group group-{Name.ToLower()} {WrapperClass}'>");
        if (LabelVisibility)
        {
            var lbl = new TagBuilder("lable");
            lbl.Attributes.Add("for", Name);
            lbl.Attributes.Add("class", LabelClass);
            lbl.InnerHtml.Append(Label);
            output.Content.AppendHtml(lbl);
        }

        var select = new TagBuilder("select");
        select.Attributes.Add("Id", Id);
        select.Attributes.Add("name", Name);
        if (Readonly) select.Attributes.Add("disabled", "disabled");
        if (!string.IsNullOrWhiteSpace(OptionalLabel))
        {
            var opt = new TagBuilder("option");
            opt.Attributes.Add("value", string.Empty);
            opt.InnerHtml.Append(OptionalLabel);
            select.InnerHtml.AppendHtml(opt);
        }
        foreach (var item in Items)
        {
            var opt = new TagBuilder("option");
            opt.Attributes.Add("value", item.Value);
            opt.InnerHtml.Append(item.Text);
            select.InnerHtml.AppendHtml(opt);
        }
        if (string.IsNullOrWhiteSpace(Class)) select.Attributes.Add("class", "form-control");
        else select.Attributes.Add("class", Class);
        foreach (var attr in context.AllAttributes.Where(x => x.Name.StartsWith("select-")))
            select.Attributes.Add(attr.Name.Replace("select-", ""), attr.Value.ToString());
        output.Content.AppendHtml(select);
        output.PostContent.SetHtmlContent($"</div>");
    }
}