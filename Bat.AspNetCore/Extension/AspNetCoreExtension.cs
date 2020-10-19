using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Bat.AspNetCore
{
    public static class AspNetCoreExtension
    {
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