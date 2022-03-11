namespace Bat.Core;

public class EmailMessage
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string HtmlTemplate { get; set; }
    public List<Guid> AttachmentsId { get; set; }
    public List<string> AttachmentsLink { get; set; }
}