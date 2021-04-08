using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bat.AspNetCore
{
    public static class AspNetCoreExtension
    {
        public async static Task<string> ReadRequestBody(this HttpRequest request)
        {
            try
            {
                request.Body.Position = 0;
                request.Body.Seek(0, SeekOrigin.Begin);
                var buffer = new byte[(long)request.ContentLength];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                var body = Encoding.UTF8.GetString(buffer);
                return body;
            }
            finally
            {
                request.Body.Position = 0;
                request.Body.Seek(0, SeekOrigin.Begin);
            }
        }

        public static void FillWithHttpRequest<TDestination>(this TDestination destinationObject, HttpRequest sourceRequest) where TDestination : class
        {
            var destinationProperties = destinationObject.GetType().GetProperties();
            if (sourceRequest.Method.ToLower() == "post")
            {
                var sourceObject = sourceRequest.Form;
                System.Reflection.PropertyInfo tempProperty;
                foreach (var key in sourceObject)
                {
                    tempProperty = destinationProperties.FirstOrDefault(x => x.CanWrite && x.Name == key.Key);
                    if (tempProperty != null) tempProperty.SetValue(destinationObject, sourceObject[key.Key]);
                }
            }
            else
            {
                var sourceObject = sourceRequest.Query;
                System.Reflection.PropertyInfo tempProperty;
                foreach (var key in sourceObject)
                {
                    tempProperty = destinationProperties.FirstOrDefault(x => x.CanWrite && x.Name == key.Key);
                    if (tempProperty != null) tempProperty.SetValue(destinationObject, sourceObject[key.ToString()]);
                }
            }
        }

    }
}