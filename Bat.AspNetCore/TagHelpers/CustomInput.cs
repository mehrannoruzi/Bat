using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore
{
    [HtmlTargetElement("custom-input")]
    public class CustomInput : BaseTagHelperModel
    {
        public InputType Type { set; get; } = InputType.text;

        public string PlaceHolder { get; set; } = "";

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

            var input = new TagBuilder("input"); //_generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.Model, null, null);
            input.Attributes.Add("type", Type.ToString());
            input.Attributes.Add("name", Name);
            input.Attributes.Add("id", Id);
            if (!string.IsNullOrWhiteSpace(Value)) input.Attributes.Add("value", Id);
            input.Attributes.Add("placeholder", PlaceHolder);
            if (Readonly) input.Attributes.Add("readonly", "readonly");
            if (string.IsNullOrWhiteSpace(Class)) input.Attributes.Add("class", "form-control");
            else input.Attributes.Add("class", Class);
            foreach (var attr in context.AllAttributes.Where(x => x.Name.StartsWith("input-")))
                input.Attributes.Add(attr.Name.Replace("input-", ""), attr.Value.ToString());

            output.Content.AppendHtml(input);
            output.PostContent.SetHtmlContent($"</div>");
        }
    }
}
