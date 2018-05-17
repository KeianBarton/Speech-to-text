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

        private readonly IAmazonUploader _amazonUploaderService;

        public AWSService(IAmazonUploader amazonUploader)
        {
            _amazonUploaderService = amazonUploader;
        }

        public async Task<SpeechRecognitionResult> ParseSpeectToText(string[] args)
        {
           var res = await _amazonUploaderService.Tester();
            return null;
        }
    }
}