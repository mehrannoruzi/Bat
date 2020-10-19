using System;
using Bat.Core;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Bat.Http
{
    public static class HttpRequestTools
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request.Headers != null) return request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }



        public static string Post(string url, Dictionary<string, string> values, Encoding resultEncoding = null)
        {
            using (var client = new WebClient())
            {
                var collection = new NameValueCollection();
                foreach (var item in values)
                    collection.Add(item.Key, item.Value);

                return resultEncoding == null
                        ? Encoding.UTF8.GetString(client.UploadValues(url, collection))
                        : resultEncoding.GetString(client.UploadValues(url, collection));
            }
        }

        public static T Post<T>(string url, Dictionary<string, string> values, Encoding resultEncoding = null) where T : class
        {
            using (var client = new WebClient())
            {
                var collection = new NameValueCollection();
                foreach (var item in values)
                    collection.Add(item.Key, item.Value);

                var response = resultEncoding == null
                                ? Encoding.UTF8.GetString(client.UploadValues(url, collection))
                                : resultEncoding.GetString(client.UploadValues(url, collection));
                return response.DeSerializeJson<T>();
            }
        }

        public static T PostJson<T>(string url, object values, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
        {
            using (var httpClient = new HttpClient())
            {
                string responseBody = string.Empty;
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    request.Content = new StringContent(values.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json");
                    var response = httpClient.SendAsync(request).Result;
                    if (header.IsNotNull())
                        foreach (var item in header)
                            request.Headers.Add(item.Key, item.Value);
                    responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody.DeSerializeJson<T>();
                }
                catch (Exception e)
                {
                    throw new Exception(responseBody, e);
                }
            }
        }

        public static string PostJson(string url, object values, Dictionary<string, string> header = null, Encoding resultEncoding = null)
        {
            using (var httpClient = new HttpClient())
            {
                string responseBody = string.Empty;
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    request.Content = new StringContent(values.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json");
                    var response = httpClient.SendAsync(request).Result;
                    if (header.IsNotNull())
                        foreach (var item in header)
                            request.Headers.Add(item.Key, item.Value);
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception e)
                {
                    throw new Exception(responseBody, e);
                }
            }
        }

        public static T PostJson<T>(string url, string jsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
        {
            using (var httpClient = new HttpClient())
            {
                string responseBody = string.Empty;
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    request.Content = new StringContent(jsonString, resultEncoding ?? Encoding.UTF8, "application/json");
                    if (header.IsNotNull())
                        foreach (var item in header)
                            request.Headers.Add(item.Key, item.Value);

                    var response = httpClient.SendAsync(request).Result;
                    responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody.DeSerializeJson<T>();
                }
                catch (Exception e)
                {
                    throw new Exception(responseBody, e);
                }
            }
        }

        public static string PostJson(string url, string jsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null)
        {
            using (var httpClient = new HttpClient())
            {
                string responseBody = string.Empty;
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    request.Content = new StringContent(jsonString, resultEncoding ?? Encoding.UTF8, "application/json");
                    if (header.IsNotNull())
                        foreach (var item in header)
                            request.Headers.Add(item.Key, item.Value);

                    var response = httpClient.SendAsync(request).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception e)
                {
                    throw new Exception(responseBody, e);
                }
            }
        }

        public static async Task<T> PostJsonAsync<T>(string url, object values, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
        {
            using (var httpClient = new HttpClient())
            {
                string responseBody = string.Empty;
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    request.Content = new StringContent(values.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json");
                    var response = httpClient.SendAsync(request).Result;
                    if (header.IsNotNull())
                        foreach (var item in header)
                            request.Headers.Add(item.Key, item.Value);
                    responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody.DeSerializeJson<T>();
                }
                catch (Exception e)
                {
                    throw new Exception(responseBody, e);
                }
            }
        }

        public static async Task<string> PostJsonAsync(string url, object values, Dictionary<string, string> header = null, Encoding resultEncoding = null)
        {
            using (var httpClient = new HttpClient())
            {
                string responseBody = string.Empty;
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    request.Content = new StringContent(values.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json");
                    var response = httpClient.SendAsync(request).Result;
                    if (header.IsNotNull())
                        foreach (var item in header)
                            request.Headers.Add(item.Key, item.Value);
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    throw new Exception(responseBody, e);
                }
            }
        }

        public static async Task<T> PostJsonAsync<T>(string url, string jsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
        {
            using (var httpClient = new HttpClient())
            {
                string responseBody = string.Empty;
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    request.Content = new StringContent(jsonString, resultEncoding ?? Encoding.UTF8, "application/json");
                    if (header.IsNotNull())
                        foreach (var item in header)
                            request.Headers.Add(item.Key, item.Value);
                    var response = httpClient.SendAsync(request).Result;
                    responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody.DeSerializeJson<T>();
                }
                catch (Exception e)
                {
                    throw new Exception(responseBody, e);
                }
            }
        }

        public static async Task<string> PostJsonAsync(string url, string jsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null)
        {
            using (var httpClient = new HttpClient())
            {
                string responseBody = string.Empty;
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    request.Content = new StringContent(jsonString, resultEncoding ?? Encoding.UTF8, "application/json");
                    if (header.IsNotNull())
                        foreach (var item in header)
                            request.Headers.Add(item.Key, item.Value);
                    var response = httpClient.SendAsync(request).Result;
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    throw new Exception(responseBody, e);
                }
            }
        }



        public static string PostXMLData(string destinationUrl, string requestXml, NameValueCollection headerParameter = null, Encoding resultEncoding = null)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(destinationUrl);
                var bytes = resultEncoding.IsNull() ? Encoding.UTF8.GetBytes(requestXml) : resultEncoding.GetBytes(requestXml);
                request.ContentType = "text/xml; charset=utf-8";
                request.ContentLength = bytes.Length;
                if (headerParameter.IsNotNull()) request.Headers.Add(headerParameter);
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    return new StreamReader(responseStream).ReadToEnd();
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<string> PostXMLDataAsync(string destinationUrl, string requestXml, NameValueCollection headerParameter = null, Encoding resultEncoding = null)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(destinationUrl);
                var bytes = resultEncoding.IsNull() ? Encoding.UTF8.GetBytes(requestXml) : resultEncoding.GetBytes(requestXml);
                request.ContentType = "text/xml; charset=utf-8";
                request.ContentLength = bytes.Length;
                if (headerParameter.IsNotNull()) request.Headers.Add(headerParameter);
                request.Method = "POST";
                Stream requestStream = await request.GetRequestStreamAsync();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                var response = await request.GetResponseAsync();
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    return new StreamReader(responseStream).ReadToEnd();
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public static string Get(string url, Encoding resultEncoding = null)
        {
            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                return client.DownloadString(url);
            }
        }

        public static T Get<T>(string url, Encoding resultEncoding = null)
        {
            using (var client = new WebClient())
            {
                string result = string.Empty;
                try
                {
                    client.Encoding = resultEncoding ?? Encoding.UTF8;
                    result = client.DownloadString(url);
                    return result.DeSerializeJson<T>();
                }
                catch (Exception e)
                {
                    throw new Exception(result, e);
                }
            }
        }

        public static string Get(string url, object parameter, Type parameterType, Encoding resultEncoding = null)
        {
            var param = string.Empty;
            foreach (var item in parameter.GetClassField(parameterType))
                param += $"{item.Name}={item.Value}&";
            var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                return client.DownloadString(completeUrl);
            }
        }

        public static T Get<T>(string url, object parameter, Type objectType, Encoding resultEncoding = null)
        {
            var param = string.Empty;
            foreach (var item in parameter.GetClassField(objectType))
                param += $"{item.Name}={item.Value}&";
            var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                return client.DownloadString(completeUrl).DeSerializeJson<T>();
            }
        }

        public static string Get(string url, Dictionary<string, string> parameter, Encoding resultEncoding = null)
        {
            var param = string.Empty;
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
            var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                return client.DownloadString(completeUrl);
            }
        }

        public static T Get<T>(string url, Dictionary<string, string> parameter, Encoding resultEncoding = null)
        {
            var param = string.Empty;
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
            var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                return client.DownloadString(completeUrl).DeSerializeJson<T>();
            }
        }

        public static async Task<string> GetAsync(string url, Encoding resultEncoding = null)
        {
            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                return await client.DownloadStringTaskAsync(url);
            }
        }

        public static async Task<T> GetAsync<T>(string url, Encoding resultEncoding = null)
        {
            using (var client = new WebClient())
            {
                string result = string.Empty;
                try
                {
                    client.Encoding = resultEncoding ?? Encoding.UTF8;
                    result = await client.DownloadStringTaskAsync(url);
                    return result.DeSerializeJson<T>();
                }
                catch (Exception e)
                {
                    throw new Exception(result, e);
                }
            }
        }

        public static async Task<T> GetAsync<T>(string url, string mediaType = "application/json") where T : class
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                    response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    //return await response.Content.ReadAsAsync<T>(); ==> <PackageReference Include="Microsoft.AspNet.WebApi.Client" Version="5.2.7" />
                    var result = await response.Content.ReadAsStringAsync();
                    return result.DeSerializeJson<T>();
                }
                catch (Exception e)
                {
                    throw new Exception(response.Content.ToString(), e);
                }
            }
        }

        public static async Task<T> GetAsync<T>(string url, object parameter, Type objectType, Encoding resultEncoding = null)
        {
            var param = string.Empty;
            foreach (var item in parameter.GetClassField(objectType))
                param += $"{item.Name}={item.Value}&";
            var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                var response = await client.DownloadStringTaskAsync(completeUrl);
                return response.DeSerializeJson<T>();
            }
        }

        public static async Task<string> GetAsync(string url, object parameter, Type objectType, Encoding resultEncoding = null)
        {
            var param = string.Empty;
            foreach (var item in parameter.GetClassField(objectType))
                param += $"{item.Name}={item.Value}&";
            var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                return await client.DownloadStringTaskAsync(completeUrl);
            }
        }

        public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> parameter, Encoding resultEncoding = null)
        {
            var param = string.Empty;
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
            var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                var response = await client.DownloadStringTaskAsync(completeUrl);
                return response.DeSerializeJson<T>();
            }
        }

        public static async Task<string> GetAsync(string url, Dictionary<string, string> parameter, Encoding resultEncoding = null)
        {
            var param = string.Empty;
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
            var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

            using (var client = new WebClient())
            {
                client.Encoding = resultEncoding ?? Encoding.UTF8;
                return await client.DownloadStringTaskAsync(completeUrl);
            }
        }
    }
}
