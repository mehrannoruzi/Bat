using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bat.AspNetCore;

public class SingleUploader : TagHelper
{
    public string Id { get; set; }
    public string DefaultMessage { get; set; }
    public string UploadUrl { get; set; }

    /// <summary>
    /// other params from dom including (param name, input id)
    /// </summary>
    public Dictionary<string, string> Params { get; set; }
    public bool Removable { get; set; } = true;
    public string RemoveMessage { get; set; }
    public string RemoveUrl { get; set; }

    /// <summary>
    /// other params from dom including (param name, input id)
    /// </summary>
    public Dictionary<string, string> RemoveParams { get; set; }
    public List<string> AcceptedFiles { get; set; } = new List<string> { "" };
    public int MaxFiles { get; set; } = 10;
    public int MaxFileSize { get; set; } = 500;

    /// <summary>
    /// Url Of Uploaded File
    /// </summary>
    public string FileUrl { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        var div = new TagBuilder("div");
        div.AddCssClass("dropzone single-uploader");
        if (!string.IsNullOrWhiteSpace(Id)) div.Attributes.Add("Id", Id);
        div.Attributes.Add("data-url", UploadUrl);
        div.Attributes.Add("data-remove-url", RemoveUrl);
        if (!string.IsNullOrWhiteSpace(DefaultMessage))
            div.Attributes.Add("data-default-message", DefaultMessage);
        div.Attributes.Add("data-removable", Removable.ToString().ToLower());
        if (!string.IsNullOrWhiteSpace(RemoveMessage))
            div.Attributes.Add("data-remove-message", RemoveMessage);
        if (!string.IsNullOrWhiteSpace(FileUrl))
            div.Attributes.Add("data-file-url", FileUrl);
        if (Params != null && Params.Count > 0) div.Attributes.Add("data-params", JsonConvert.SerializeObject(Params.ToList()));
        if (RemoveParams != null && RemoveParams.Count > 0) div.Attributes.Add("data-remove-params", JsonConvert.SerializeObject(RemoveParams.ToList()));
        div.Attributes.Add("data-max-files", MaxFiles.ToString());
        div.Attributes.Add("data-max-file-size", MaxFileSize.ToString());
        div.Attributes.Add("data-accepted-files", string.Join(",", AcceptedFiles));

        output.Content.AppendHtml(div);
    }
}