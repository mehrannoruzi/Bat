For use Bat.Http just do it :

1- Install Bat.Http on your project

2- Use it in bussiness logic
for example :

    public class BaseService : IBaseService
    {
        private readonly HttpClient _httpClient;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public void UseBatHttpSample()
        {
            var newObject = new
            {
                Id = 1,
                FirstName = "Mehran",
                LastName = "Norouzi"
            };
        
            var requestHeader = new Dictionary<string, string>
            {
                {"Authorization", "Bearer fjfksjfk-sdklsdjfsddsjkljsdklfj_sakd" }
            };

            string IP = ClientInfo.GetIP(HttpContext);
            RequestDetails requestDetails = ClientInfo.GetRequestDetails(HttpContext);

            var result1 = await HttpRequestTools.GetAsync(httpClient: _httpClient, url: "https://someSite.com");
            var result2 = await HttpRequestTools.PostAsync(httpClient: _httpClient, url: "https://someSite.com", 
                        contentJsonString: newObject.SerializeToJson(), header: requestHeader);
            var result3 = await HttpRequestTools.PutAsync(httpClient: _httpClient, url: "https://someSite.com", 
                        contentJsonString: newObject.SerializeToJson(), header: requestHeader);
            var result4 = await HttpRequestTools.DeleteAsync(httpClient: _httpClient, url: "https://someSite.com", 
                        contentJsonString: newObject.SerializeToJson(), header: requestHeader);

        }
    }