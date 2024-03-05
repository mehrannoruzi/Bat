namespace Bat.Http;

public static class HttpRequestTools
{
    public static bool IsAjaxRequest(this HttpRequest request)
    {
        if (request.Headers != null) return request.Headers["X-Requested-With"] == "XMLHttpRequest";

        return false;
    }


    public static async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
    {
        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> GetAsync(string url, CancellationToken cancellationToken = default)
    {
        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> GetAsync<T>(string url, string mediaType = "application/json", CancellationToken cancellationToken = default) where T : class
    {
        using var httpClient = new HttpClient();
        var response = new HttpResponseMessage();
        httpClient.BaseAddress = new Uri(url);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync(cancellationToken);
        return result.DeSerializeJson<T>();
    }

    public static async Task<T> GetAsync<T>(string url, object parameter, Type objectType, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter.GetClassFields(objectType))
                param += $"{item.Name}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> GetAsync(string url, object parameter, Type objectType, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter.GetClassFields(objectType))
                param += $"{item.Name}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> parameter, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> GetAsync(string url, Dictionary<string, string> parameter, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> parameter, Dictionary<string, string> header, CancellationToken cancellationToken = default) where T : class
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> GetAsync(string url, Dictionary<string, string> parameter, Dictionary<string, string> header, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> parameter, Dictionary<string, string> header, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default) where T : class
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> GetAsync(string url, Dictionary<string, string> parameter, Dictionary<string, string> header, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }


    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(url, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> parameter, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var response = await httpClient.GetAsync(completeUrl, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> GetAsync(HttpClient httpClient, string url, Dictionary<string, string> parameter, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var response = await httpClient.GetAsync(completeUrl, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, object parameter, Type objectType, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter.GetClassFields(objectType))
                param += $"{item.Name}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var response = await httpClient.GetAsync(completeUrl, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> parameter, Dictionary<string, string> header, CancellationToken cancellationToken = default) where T : class
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> GetAsync(HttpClient httpClient, string url, Dictionary<string, string> parameter, Dictionary<string, string> header, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> parameter, Dictionary<string, string> header, int timeOutSecond, CancellationToken cancellationToken = default) where T : class
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> GetAsync(HttpClient httpClient, string url, Dictionary<string, string> parameter, Dictionary<string, string> header, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var param = string.Empty;
        if (parameter.IsNotNull())
            foreach (var item in parameter)
                param += $"{item.Key}={item.Value}&";
        var completeUrl = string.IsNullOrWhiteSpace(param) ? url : $"{url}?{param[..^1]}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(completeUrl));
        foreach (var item in header)
            request.Headers.Add(item.Key, item.Value);

        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }




    public static async Task<T> PostAsync<T>(string url, object contentValues, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentValues.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PostAsync(string url, object contentValues, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentValues.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> PostAsync<T>(string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PostAsync(string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> PostAsync<T>(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PostAsync(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> PostFormAsync<T>(string url, Dictionary<string, string> formBody, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var formData = new MultipartFormDataContent();
        if (formBody.IsNotNull())
            foreach (var item in formBody)
                formData.Add(new StringContent(item.Value, resultEncoding ?? Encoding.UTF8), item.Key);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = formData
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PostFormAsync(string url, Dictionary<string, string> formBody, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var formData = new MultipartFormDataContent();
        if (formBody.IsNotNull())
            foreach (var item in formBody)
                formData.Add(new StringContent(item.Value, resultEncoding ?? Encoding.UTF8), item.Key);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = formData
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> PostFormAsync<T>(string url, Dictionary<string, string> formBody, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var formData = new MultipartFormDataContent();
        if (formBody.IsNotNull())
            foreach (var item in formBody)
                formData.Add(new StringContent(item.Value, resultEncoding ?? Encoding.UTF8), item.Key);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = formData
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PostFormAsync(string url, Dictionary<string, string> formBody, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var formData = new MultipartFormDataContent();
        if (formBody.IsNotNull())
            foreach (var item in formBody)
                formData.Add(new StringContent(item.Value, resultEncoding ?? Encoding.UTF8), item.Key);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = formData
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }


    public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PostAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, int timeOutSecond, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PostAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }




    public static async Task<T> PutAsync<T>(string url, object contentValues, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentValues.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PutAsync(string url, object contentValues, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentValues.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> PutAsync<T>(string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PutAsync(string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> PutAsync<T>(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PutAsync(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }


    public static async Task<T> PutAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PutAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> PutAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, int timeOutSecond, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> PutAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
        {
            Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }




    public static async Task<T> DeleteAsync<T>(string url, object contentValues = null, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentValues.IsNotNull()) request.Content = new StringContent(contentValues.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> DeleteAsync(string url, object contentValues = null, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentValues.IsNotNull()) request.Content = new StringContent(contentValues.SerializeToJson(), resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> DeleteAsync<T>(string url, string contentJsonString = null, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> DeleteAsync(string url, string contentJsonString = null, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> DeleteAsync<T>(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> DeleteAsync(string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, bool byPassServerSertificate, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var handler = new HttpClientHandler();
        if (byPassServerSertificate)
        {
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
        }
        using var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }


    public static async Task<T> DeleteAsync<T>(HttpClient httpClient, string url, string contentJsonString = null, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> DeleteAsync(HttpClient httpClient, string url, string contentJsonString = null, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> DeleteAsync<T>(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, int timeOutSecond, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

    public static async Task<(HttpStatusCode httpStatusCode, string response)> DeleteAsync(HttpClient httpClient, string url, string contentJsonString, Dictionary<string, string> header, Encoding resultEncoding, int timeOutSecond, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
        if (contentJsonString.IsNotNull()) request.Content = new StringContent(contentJsonString, resultEncoding ?? Encoding.UTF8, "application/json");
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }




    public static async Task<(HttpStatusCode httpStatusCode, string response)> PostXMLAsync(string url, string contentXmlString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentXmlString, resultEncoding ?? Encoding.UTF8, "text/xml; charset=utf-8")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public static async Task<T> PostXMLAsync<T>(string url, string contentXmlString, Dictionary<string, string> header = null, Encoding resultEncoding = null, CancellationToken cancellationToken = default) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
        {
            Content = new StringContent(contentXmlString, resultEncoding ?? Encoding.UTF8, "text/xml; charset=utf-8")
        };
        if (header.IsNotNull())
            foreach (var item in header)
                request.Headers.Add(item.Key, item.Value);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody.DeSerializeJson<T>();
    }

}