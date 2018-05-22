using System.Net;
using System.Net.Http;

namespace Services.IServices
{
    public interface IHttpProxyClientService
    {
        HttpClient CreateHttpClient();
        HttpWebRequest CreateHttpWebRequest(string requestUri);
    }
}
