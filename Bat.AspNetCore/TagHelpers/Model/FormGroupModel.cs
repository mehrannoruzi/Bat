using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore
{
    public class FormGroupModel : TagHelper
    {
        [HtmlAttributeName("wrapper-class")]
        public string WrapperClass { get; set; }

        [HtmlAttributeName("label-visibility")]
        public bool LabelVisibility { get; set; } = true;

        [HtmlAttributeName("label-class")]
        public string LabelClass { get; set; }

        public string Class { get; set; } = "form-control";

        public bool Readonly { get; set; }
    }
}