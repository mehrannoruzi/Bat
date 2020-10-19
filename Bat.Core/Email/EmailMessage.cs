using System;
using System.Collections.Generic;

namespace Bat.Core
{
    public class EmailMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string HtmlTemplate { get; set; }
        public List<Guid> AtachmentsId { get; set; }
        public List<string> AtachmentsLink { get; set; }
    }
}
