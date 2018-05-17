using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Services.Classes
{
    public static class HttpProxyClient
    {
        public static HttpClient CreateHttpClient()
        {
            try
            {
                var proxy = new WebProxy()
                {
                    Address = new Uri("http://127.0.0.1:3128"),
                    //BypassOnLocal = false,
                    UseDefaultCredentials = true
                };

                // Now create a client handler which uses that proxy

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
