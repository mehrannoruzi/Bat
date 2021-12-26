using Microsoft.AspNetCore.Http;

namespace Bat.AspNetCore
{
    public class FileSaveModel
    {
        public object Id { get; set; }
        public string Root { get; set; } = "~";
        public bool IncludeDayInPath { get; set; } = true;
        public string FileNamePrefix { get; set; }
        public bool SaveInMultipleSize { get; set; } = false;
        //public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor; //defalut is low
        public IFormFile FormFile { set; get; }
        public string UrlPrefix { get; set; }
    }
}