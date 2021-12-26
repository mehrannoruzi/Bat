using Microsoft.AspNetCore.Http;

namespace Bat.AspNetCore
{
    public class LargeFileSaveModel
    {
        public string Root { get; set; } = "~";

        public object Id { get; set; }

        public bool IncludeDayInPath { get; set; } = true;

        public string FileNamePrefix { get; set; }

        //public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor; //defalut is low

        public HttpRequest Request { set; get; }

        public string UrlPrefix { get; set; }
    }
}