using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore
{
    [HtmlTargetElement("custom-checkbox")]
    public class CustomCheckBox : BaseTagHelperModel
    {
        public bool Checked { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            output.PreContent.SetHtmlContent($"<div class='form-group  group-{Name.ToLower()} {WrapperClass}'> <div class='i-checks'><label class='{LabelClass}><input type='checkbox' name='{LabelClass}'>");
            var chb = new TagBuilder("input");
            chb.Attributes.Add("type", "checkbox");
            chb.Attributes.Add("name", Name);
            if (Checked) chb.Attributes.Add("checked", "checked");
            if (!string.IsNullOrWhiteSpace(Id)) chb.Attributes.Add("id", Id);
            if (Readonly) chb.Attributes.Add("disabled", "disabled");
            if (string.IsNullOrWhiteSpace(Class)) chb.Attributes.Add("class", "form-control");
            else chb.Attributes.Add("class", Class);
            foreach (var attr in context.AllAttributes.Where(x => x.Name.StartsWith("chb-")))
                chb.Attributes.Add(attr.Name.Replace("chb-", ""), attr.Value.ToString());
            output.Content.AppendHtml(chb);
            output.Content.AppendHtml($"<i></i>{Label}</label></div>");

            output.PostContent.SetHtmlContent($"</div>");
        }
    }
}
