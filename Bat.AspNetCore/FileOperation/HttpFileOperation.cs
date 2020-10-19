using System;
using Bat.Core;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;

namespace Bat.AspNetCore
{
    public static class HttpFileOperation
    {
        public static byte[] ToByteArray(IFormFile file)
        {
            var target = new MemoryStream();
            file.OpenReadStream().CopyTo(target);
            return target.ToArray();
        }

        public static string ToBase64(IFormFile file)
        {
            var target = new MemoryStream();
            file.OpenReadStream().CopyTo(target);
            return Convert.ToBase64String(target.ToArray());
        }

        public static bool Delete(string fullPath)
        {
            File.Delete(fullPath);
            if (File.Exists(fullPath)) return false;

            return true;
        }

        public static string Save(IFormFile file, string path)
        {
            if (file.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName)
                                + DateTime.Now.Ticks.ToString()
                                + Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                if (File.Exists(fullPath)) return fullPath;
            }
            return string.Empty;
        }

        public static async Task<string> SaveFile(FileSaveModel model)
        {
            if (model.FormFile == null) return null;
            var pDate = PersianDateTime.Now;

            #region Create Directory
            model.Root = string.Join("/", model.Root, pDate.Year, pDate.Month);
            if (model.IncludeDayInPath) model.Root = model.Root + "/" + pDate.Day;
            model.Root = model.Root + (model.Id == null ? string.Empty : ("/" + model.Id.ToString()));
            var dir = Path.Combine(Directory.GetCurrentDirectory(), model.UrlPrefix ?? "", "wwwroot", model.Root.Replace("/", "\\"));
            #endregion

            #region Create File Name
            model.FileNamePrefix = model.FileNamePrefix != null ? model.FileNamePrefix + "_" : string.Empty;
            var trustedFileName = WebUtility.HtmlEncode(model.FormFile.FileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(trustedFileName);
            var fileName = model.FileNamePrefix + fileNameWithoutExtension + pDate.Ticks.ToString() + Path.GetExtension(trustedFileName);
            #endregion

            var rep = "~/" + model.Root + "/" + fileName;
            if (!FileOperation.CreateDirectory(dir)) return null;
            using (var stream = File.Create(Path.Combine(dir, fileName)))
            {
                await model.FormFile.CopyToAsync(stream);
            }

            if (!model.SaveInMultipleSize) return rep;
            using (var stream = new MemoryStream())
            {
                await model.FormFile.CopyToAsync(stream);
                using (var img = Image.FromStream(stream))
                {
                    var aspectRatio = img.Size.Height / (float)img.Size.Width;
                    var sourceWidth = img.Size.Width;
                    foreach (var size in Enum.GetNames(typeof(ImageSize)))
                    {
                        var x = (int)(Enum.Parse(typeof(ImageSize), size));
                        int newWidth = ((int)(Enum.Parse(typeof(ImageSize), size)) * sourceWidth) / 100;
                        int newHeight = Convert.ToInt32(aspectRatio * newWidth);
                        Bitmap bitmap = new Bitmap(newWidth, newHeight);
                        Graphics graph = Graphics.FromImage(bitmap);
                        graph.CompositingQuality = CompositingQuality.HighQuality;
                        graph.SmoothingMode = SmoothingMode.HighQuality;
                        graph.InterpolationMode = model.InterpolationMode;
                        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        graph.DrawImage(img, imageRectangle);
                        bitmap.Save(Path.Combine(dir, size.ToLower() + "-" + fileName));
                        graph.Dispose();
                        bitmap.Dispose();
                    }
                    img.Dispose();
                }
            }
            return rep;
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
}