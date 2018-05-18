using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;
using Services.Models;

namespace Services.IServices
{
    public interface IHttpProxyClientService
    {
        HttpClient CreateHttpClient();
    }
}
