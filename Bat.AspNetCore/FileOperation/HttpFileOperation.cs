using System.Net;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;

namespace Bat.AspNetCore;

public static class HttpFileOperation
{
    public static byte[] ToByteArray(IFormFile file)
    {
        using var target = new MemoryStream();
        file.OpenReadStream().CopyTo(target);
        return target.ToArray();
    }

    public static string ToBase64(IFormFile file)
    {
        using var target = new MemoryStream();
        file.OpenReadStream().CopyTo(target);
        return Convert.ToBase64String(target.ToArray());
    }

    public static string GetPath(string fileNameWithExtension, string root = "~",
            bool includeYearInPath = false, bool includeMonthInPath = false,
            bool includeDayInPath = false, string objectId = null,
            string urlPrefix = null, string fileNamePrefix = null)
    {
        #region Create Directory Address
        var persianDate = PersianDateTime.Now;
        var path = string.Join("/", root);
        if (includeYearInPath) path += "/" + persianDate.Year;
        if (includeMonthInPath) path += "/" + persianDate.Month;
        if (includeDayInPath) path += "/" + persianDate.Day;
        path += (objectId == null ? string.Empty : ("/" + objectId));
        var directoryAddress = Path.Combine(Directory.GetCurrentDirectory(), urlPrefix ?? "", "wwwroot", path.Replace("/", "\\"));
        #endregion

        #region Create File Name
        var trustedFileName = WebUtility.HtmlEncode(fileNameWithExtension);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(trustedFileName);
        var fileName = fileNamePrefix != null ? fileNamePrefix + "_" : string.Empty;
        fileName += fileNameWithoutExtension + "_" + persianDate.Ticks.ToString() + Path.GetExtension(trustedFileName);
        #endregion

        if (!FileOperation.CreateDirectory(directoryAddress)) return null;
        return "wwwroot/" + path + "/" + fileName;
    }


    public static bool Delete(string fullPath)
    {
        File.Delete(fullPath);
        if (File.Exists(fullPath)) return false;

        return true;
    }

    public static string Save(byte[] fileBytes, string fullPath)
    {
        if (fileBytes is null || fileBytes.Length <= 0) return null;

        var file = new FormFile(null, 0, fileBytes.Length, "", "");

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        if (File.Exists(fullPath)) return fullPath;
        return null;
    }

    public static string Save(IFormFile file, string fullPath)
    {
        if (file is null || file.Length <= 0) return null;

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        if (File.Exists(fullPath)) return fullPath.Contains("wwwroot/") ? fullPath.Remove(0, 8) : fullPath;
        return null;
    }

    public static string SaveWithPath(IFormFile file, string path)
    {
        if (file.Length <= 0) return string.Empty;

        var fileName = Path.GetFileNameWithoutExtension(file.FileName)
                        + DateTime.Now.Ticks.ToString()
                        + Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(path, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        if (File.Exists(fullPath)) return fullPath;
        return string.Empty;
    }

    public static async Task<string> SaveLargeFile(LargeFileSaveModel model)
    {
        if (!MultipartRequestHelper.IsMultipartContentType(model.Request.ContentType)) return null;
        var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(model.Request.ContentType), 2097152);
        var reader = new MultipartReader(boundary, model.Request.Body);
        var section = await reader.ReadNextSectionAsync();

        #region Create Directory
        var pDate = PersianDateTime.Now;
        model.Root = string.Join("/", model.Root, pDate.Year, pDate.Month);
        if (model.IncludeDayInPath) model.Root = model.Root + "/" + pDate.Day;
        model.Root = model.Root + (model.Id == null ? string.Empty : ("/" + model.Id.ToString()));
        var dir = Path.Combine(Directory.GetCurrentDirectory(), model.UrlPrefix ?? "", "wwwroot", model.Root.Replace("/", "\\"));
        #endregion

        model.FileNamePrefix = model.FileNamePrefix != null ? model.FileNamePrefix + "_" : string.Empty;
        if (!FileOperation.CreateDirectory(dir)) return null;
        string rep = null;

        while (section != null)
        {
            var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
            var trustedFileName = WebUtility.HtmlEncode(contentDisposition.FileName.Value);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(trustedFileName);
            var fileName = model.FileNamePrefix + fileNameWithoutExtension + pDate.Ticks.ToString() + Path.GetExtension(trustedFileName);
            rep = "~/" + model.Root + "/" + fileName;

            if (hasContentDispositionHeader)
            {
                if (!MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    return null;
                else
                {
                    byte[] streamedFileContent;
                    using (var memoryStream = new MemoryStream())
                    {
                        await section.Body.CopyToAsync(memoryStream);
                        streamedFileContent = memoryStream.ToArray();
                    }

                    using (var targetStream = File.Create(Path.Combine(dir, fileName)))
                    {
                        await targetStream.WriteAsync(streamedFileContent);
                    }
                }
            }
            section = await reader.ReadNextSectionAsync();
        }
        return rep;
    }

}