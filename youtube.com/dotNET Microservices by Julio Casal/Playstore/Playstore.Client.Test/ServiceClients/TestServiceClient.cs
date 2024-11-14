using System.Net.Http;
using System.Net.Http.Headers;
using Playstore.Client.ServiceClients;
using Playstore.Client.Services;

namespace Playstore.Client.Test.ServiceClients;

public class TestServiceClient : AuthorizedServiceClient
{
    public TestServiceClient(HttpClient httpClient, ITokenStorageService tokenStorageService) 
        : base(httpClient, tokenStorageService)
    {
    }

    public HttpHeaders DefaultRequestHeaders()
    {
        return _httpClient.DefaultRequestHeaders;
    }
}