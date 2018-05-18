using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Services.IServices;
using Services.Models;

namespace Services.Services
{
    public class HttpProxyClientService : IHttpProxyClientService
    {
        private readonly IOptions<MyConfig> _config;

        public HttpProxyClientService(IOptions<MyConfig> config)
        {
            _config = config;
        }

        public HttpClient CreateHttpClient()
        {
            try
            {
                var useProxy = _config.Value.UseProxy;
                if (!useProxy)
                {
                    return new HttpClient();
                }

                var proxyHost = _config.Value.ProxyHost;
                var proxyPort = _config.Value.ProxyPort.ToString();

                var proxy = new WebProxy()
                {
                    Address = new Uri("http://" + proxyHost + ":" + proxyPort),
                    UseDefaultCredentials = true
                };

                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxy,
                };

                var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return null;
        }

    }
}
