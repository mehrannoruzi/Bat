namespace Bat.AspNetCore;

public class LargeFileSaveModel
{
    public object Id { get; set; }
    public string Root { get; set; } = "~";
    public bool IncludeDayInPath { get; set; } = true;
    public string FileNamePrefix { get; set; }
    //public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor; //defalut is low
    public HttpRequest Request { set; get; }
    public string UrlPrefix { get; set; }
}