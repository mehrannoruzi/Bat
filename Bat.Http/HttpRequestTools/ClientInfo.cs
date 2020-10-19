using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Bat.Http
{
    public static class ClientInfo
    {
        public static string GetIP(HttpContext httpContext)
        {
            return httpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        public static string GetIP(IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                if (httpContextAccessor != null) return httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                var host = Dns.GetHostEntry(Dns.GetHostName());
                var ip = host.AddressList[2].ToString();
                if (!string.IsNullOrWhiteSpace(ip)) return ip;

                return host.AddressList.ToList().Where(p => p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();
            }
            catch
            {
                return "127.0.0.1";// string.Empty;
            }
        }

        public static void GetRequestOs(string userAgent, out string os, out string version)
        {
            version = string.Empty;
            if (userAgent.Contains("Android")) { os = "Android"; version = GetRequestOsVersion(userAgent, os); }
            else if (userAgent.Contains("iPad")) { os = "IOS"; version = GetRequestOsVersion(userAgent, "OS"); }
            else if (userAgent.Contains("iPhone")) { os = "IOS"; version = GetRequestOsVersion(userAgent, "OS"); }
            else if (userAgent.Contains("Linux") && userAgent.Contains("KFAPWI")) os = "Linux";
            else if (userAgent.Contains("RIM Tablet") || (userAgent.Contains("BB") && userAgent.Contains("Mobile"))) { os = "Black Berry"; version = GetRequestOsVersion(userAgent, os); }
            else if (userAgent.Contains("Windows Phone")) { os = "Windows Phone"; version = GetRequestOsVersion(userAgent, os); }
            else if (userAgent.Contains("Mac OS")) { os = "Mac OS"; version = GetRequestOsVersion(userAgent, "OS"); }
            else if (userAgent.Contains("Windows NT 5.1") || userAgent.Contains("Windows NT 5.2")) os = "Windows XP";
            else if (userAgent.Contains("Windows NT 6.0")) os = "Windows Vista";
            else if (userAgent.Contains("Windows NT 6.1")) os = "Windows 7";
            else if (userAgent.Contains("Windows NT 6.2")) os = "Windows 8";
            else if (userAgent.Contains("Windows NT 6.3")) os = "Windows 8.1";
            else if (userAgent.Contains("Windows NT 10")) os = "Windows 10";
            else os = "Unknown";
        }

        public static string GetRequestOsVersion(string userAgent, string os)
        {
            if (os == null) throw new ArgumentNullException(nameof(os));

            var version = string.Empty;
            var temp = userAgent.Substring(userAgent.IndexOf(os) + os.Length).TrimStart();
            if (string.IsNullOrWhiteSpace(temp) | temp.StartsWith(".")) return version;

            foreach (var character in temp)
            {
                var validCharacter = false;
                if (int.TryParse(character.ToString(), out int test))
                {
                    version += character;
                    validCharacter = true;
                }

                if (character == '.' || character == '_')
                {
                    version += '.';
                    validCharacter = true;
                }

                if (validCharacter == false) break;
            }

            return version;
        }

        public static void GetRequestBrowser(string userAgent, out string browser, out string version)
        {
            version = "0.0";
            browser = "Unknown";
            var result = userAgent.Substring(userAgent.LastIndexOf(")") + 1).Trim();

            if (result.Contains("Edge"))
            {
                browser = "Edge";
                version = GetRequestBrowserVersion(userAgent, browser);
            }
            else if (result.Contains("OPR"))
            {
                browser = "Opera";
                version = GetRequestBrowserVersion(userAgent, "OPR");
            }
            else if (result.Contains("Firefox"))
            {
                browser = "Firefox";
                version = GetRequestBrowserVersion(userAgent, browser);
            }
            else if (result.Contains("UCBrowser"))
            {
                browser = "UCBrowser";
                version = GetRequestBrowserVersion(userAgent, browser);
            }
            else if (result.Contains("SamsungBrowser"))
            {
                browser = "SamsungBrowser";
                version = GetRequestBrowserVersion(userAgent, browser);
            }
            else if (result.Contains("Chrome"))
            {
                browser = "Chrome";
                version = GetRequestBrowserVersion(userAgent, browser);
            }
            else if (result.Contains("Safari"))
            {
                browser = "Safari";
                version = GetRequestBrowserVersion(userAgent, browser);
            }
        }

        public static string GetRequestBrowserVersion(string userAgent, string browser)
        {
            var version = string.Empty;
            if (browser == "Unknown") return "0.0";

            var temp = userAgent.Substring(userAgent.IndexOf(browser) + browser.Length + 1).TrimStart();
            if (string.IsNullOrWhiteSpace(temp) | temp.StartsWith(".")) return version;

            foreach (var character in temp)
            {
                var validCharacter = false;
                if (int.TryParse(character.ToString(), out int test))
                {
                    version += character;
                    validCharacter = true;
                }

                if (character == '.' || character == '_')
                {
                    version += '.';
                    validCharacter = true;
                }

                if (validCharacter == false) break;
            }

            return version;
        }

        public static string GetRequestDeviceModel(string userAgent)
        {
            var model = userAgent[userAgent.IndexOf("(")..userAgent.IndexOf(")")];
            var result = model.Split(";");
            //if (result.Length == 2) model = result[1];
            //else model = result[2];

            return result.Last();
        }

        public static string GetRequestDeviceManufacture(string os)
        {
            string manufacture;
            switch (os)
            {
                case "Unity":
                case "Windows XP":
                case "Windows Vista":
                case "Windows 7":
                case "Windows 8":
                case "Windows 8.1":
                case "Windows 10":
                case "Windows Phone":
                    manufacture = "Microsoft";
                    break;
                case "IOS":
                case "Mac OS":
                    manufacture = "Apple";
                    break;
                case "Black Berry":
                    manufacture = "Black Berry";
                    break;

                default:
                    manufacture = "Unknown";
                    break;
            }

            return manufacture;
        }

        public static RequestDetails GetRequestDetails(HttpContext httpContext)
        {
            var result = new RequestDetails();
            try
            {
                var os = string.Empty;
                var osVersion = string.Empty;
                var browser = string.Empty;
                var browserVersion = string.Empty;
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                GetRequestOs(userAgent, out os, out osVersion);
                GetRequestBrowser(userAgent, out browser, out browserVersion);

                result.OsName = os;
                result.OsVersion = osVersion;
                result.BrowserName = browser;
                result.BrowserVersion = browserVersion;
                result.IsMobile = userAgent.Contains("Mobile");
                result.Model = GetRequestDeviceModel(userAgent);
                result.Manufacture = GetRequestDeviceManufacture(os);
                result.IP = GetIP(httpContext);
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
