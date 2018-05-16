using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;
using Services.Classes;
using Services.IServices;
using Services.Models;

namespace Services.Services
{
    public class AWSService : IAWSService
    {
        //AWSService(ICoreAmazonS3 )
        //{

        //}
        public SpeechRecognitionResult ParseSpeectToText(string[] args)
        {
            return null;
        }
    }
}