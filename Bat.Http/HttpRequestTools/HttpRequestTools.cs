namespace Bat.Http;

public static class HttpRequestTools
{
    public static bool IsAjaxRequest(this HttpRequest request)
    {
        if (request.Headers != null) return request.Headers["X-Requested-With"] == "XMLHttpRequest";

        return false;
    }


    public static async Task<T> GetAsync<T>(string url)
    {
        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> GetAsync(string url)
    {
        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody;
    }

    public static async Task<T> GetAsync<T>(string url, string mediaType = "application/json") where T : class
    {
        using var httpClient = new HttpClient();
        var response = new HttpResponseMessage();
        httpClient.BaseAddress = new Uri(url);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return result.DeSerializeJson<T>();
    }

    public static async Task<T> GetAsync<T>(string url, object parameter, Type objectType)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter.GetClassFields(objectType))
                param += $"{item.Name}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> GetAsync(string url, object parameter, Type objectType)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter.GetClassFields(objectType))
                param += $"{item.Name}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> parameter)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> GetAsync(string url, Dictionary<string, string> parameter)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> parameter, Dictionary<string, string> header) where T : class
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> GetAsync(string url, Dictionary<string, string> parameter, Dictionary<string, string> header)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> parameter, Dictionary<string, string> header, bool byPassServerSertificate, int timeOutSecond) where T : class
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> GetAsync(string url, Dictionary<string, string> parameter, Dictionary<string, string> header, bool byPassServerSertificate, int timeOutSecond)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }


    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url)
    {
        var response = await httpClient.GetAsync(url);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> parameter)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var response = await httpClient.GetAsync(completeUrl);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> GetAsync(HttpClient httpClient, string url, Dictionary<string, string> parameter)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var response = await httpClient.GetAsync(completeUrl);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, object parameter, Type objectType)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter.GetClassFields(objectType))
                param += $"{item.Name}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var response = await httpClient.GetAsync(completeUrl);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> parameter, Dictionary<string, string> header) where T : class
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> GetAsync(HttpClient httpClient, string url, Dictionary<string, string> parameter, Dictionary<string, string> header)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> parameter, Dictionary<string, string> header, bool byPassServerSertificate, int timeOutSecond) where T : class
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> GetAsync(HttpClient httpClient, string url, Dictionary<string, string> parameter, Dictionary<string, string> header, bool byPassServerSertificate, int timeOutSecond)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param.Substring(0, param.Length - 1)}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }




    public static async Task<T> PostAsync<T>(string url, object contentValues, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentValues.SerializeToJson(new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles }), resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PostAsync(string url, object contentValues, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentValues.SerializeToJson(new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles }), resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> PostAsync<T>(string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PostAsync(string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> PostAsync<T>(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PostAsync(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> PostFormAsync<T>(string url, Dictionary<string, string> formBody, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var formData = new MultipartFormDataContent();
        if (formBody.IsNotNull())
            foreach (var item in formBody)
                formData.Add(new StringContent(item.Value, resultEncoding ?? Encoding.UTF8), item.Key);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = formData;
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PostFormAsync(string url, Dictionary<string, string> formBody, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var formData = new MultipartFormDataContent();
        if (formBody.IsNotNull())
            foreach (var item in formBody)
                formData.Add(new StringContent(item.Value, resultEncoding ?? Encoding.UTF8), item.Key);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = formData;
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody;
    }

    public static async Task<T> PostFormAsync<T>(string url, Dictionary<string, string> formBody, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond)
    {
        var formData = new MultipartFormDataContent();
        if (formBody.IsNotNull())
            foreach (var item in formBody)
                formData.Add(new StringContent(item.Value, resultEncoding ?? Encoding.UTF8), item.Key);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = formData;
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PostFormAsync(string url, Dictionary<string, string> formBody, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond)
    {
        var formData = new MultipartFormDataContent();
        if (formBody.IsNotNull())
            foreach (var item in formBody)
                formData.Add(new StringContent(item.Value, resultEncoding ?? Encoding.UTF8), item.Key);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = formData;
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody;
    }


    public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PostAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PostAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }




    public static async Task<T> PutAsync<T>(string url, object contentValues, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentValues.SerializeToJson(new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles }), resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PutAsync(string url, object contentValues, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentValues.SerializeToJson(new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles }), resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> PutAsync<T>(string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PutAsync(string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> PutAsync<T>(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PutAsync(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }


    public static async Task<T> PutAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PutAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> PutAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> PutAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
        request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }




    public static async Task<T> DeleteAsync<T>(string url, object contentValues = null, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentValues.IsNotNull()) request.Content = new StringContent(contentValues.SerializeToJson(new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles }), resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> DeleteAsync(string url, object contentValues = null, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentValues.IsNotNull()) request.Content = new StringContent(contentValues.SerializeToJson(new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles }), resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> DeleteAsync<T>(string url, string contentJsonString = null, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> DeleteAsync(string url, string contentJsonString = null, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> DeleteAsync<T>(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> DeleteAsync(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }


    public static async Task<T> DeleteAsync<T>(HttpClient httpClient, string url, string contentJsonString = null, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> DeleteAsync(HttpClient httpClient, string url, string contentJsonString = null, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> DeleteAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<string> DeleteAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        HttpClientHandler handler = new();
        if (byPassServerSertificate)
        {
            handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }




    public static async Task<string> PostXMLAsync(string url, string contentXmlString, Dictionary<string, string> header = null, Encoding resultEncoding = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentXmlString, resultEncoding ?? Encoding.UTF8, "text/xml; charset=utf-8");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<T> PostXMLAsync<T>(string url, string contentXmlString, Dictionary<string, string> header = null, Encoding resultEncoding = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
        request.Content = new StringContent(contentXmlString, resultEncoding ?? Encoding.UTF8, "text/xml; charset=utf-8");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody.DeSerializeJson<T>();
    }

}