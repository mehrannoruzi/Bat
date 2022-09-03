namespace Bat.Http;

public static class HttpExtensions
{
    public static string GetIP(this HttpContext httpContext)
    {
        return httpContext?.Connection?.RemoteIpAddress?.ToString();
    }

    public static string GetIPBehindCloud(this HttpContext httpContext)
    {
        var ip = httpContext?.Request?.Headers["CF-Connecting-IP"];
        if (!string.IsNullOrWhiteSpace(ip)) return ip;

        ip = httpContext?.Request?.Headers["True-Client-IP"];
        if (!string.IsNullOrWhiteSpace(ip)) return ip;

        return httpContext?.Connection?.RemoteIpAddress?.ToString();
    }

    public static DeviceLog GetDeviceLog(this HttpContext httpContext)
    {
        var requestDetails = ClientInfo.GetRequestDetails(httpContext);
        var ip = httpContext.GetIPBehindCloud();
        var isMobile = requestDetails == null ? false : requestDetails.IsMobile;
        var os = $"{requestDetails?.OsName} {requestDetails?.OsVersion}";
        var device = $"{requestDetails?.Manufacture} {requestDetails?.Model}";
        var application = $"{requestDetails?.BrowserName} {requestDetails?.BrowserVersion}";

        return new DeviceLog
        {
            IsMobile = isMobile,
            IP = ip,
            Os = os.Length > 25 ? os.Substring(0, 25) : os,
            Device = device.Length > 50 ? device.Substring(0, 50) : device,
            Application = application.Length > 50 ? application.Substring(0, 50) : application
        };
    }

}