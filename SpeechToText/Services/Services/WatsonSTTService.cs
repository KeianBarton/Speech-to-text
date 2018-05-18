﻿using System;
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
using Services.Classes;
using Services.IServices;
using Services.Models;

namespace Services.Services
{
    public class WatsonSTTService : IWatsonSTTService
    {
        private readonly IHttpProxyClientService _httpProxyClientService;

        private static string username = "d99e7579-fcc3-403c-bb6d-ac4c86eec841";
        private static string password = "plRwOk0IuiQb";
        private static string model = "en-US_NarrowbandModel";

        public WatsonSTTService(IHttpProxyClientService httpProxyClientService)
        {
            _httpProxyClientService = httpProxyClientService;
        }

        public SpeechRecognitionResult ParseSpeectToText(string[] args)
        {
            var file = args[0];
            using (var client = _httpProxyClientService.CreateHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(
                            username + ":" + password)));
    
//                var content = new StreamContent(new FileStream(file, FileMode.Open));
                var bytes = Convert.FromBase64String(file);
                var content = new StreamContent(new MemoryStream(bytes));
                content.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");

                Stopwatch sw = new Stopwatch();
                sw.Start();
                
                var response = client
                    .PostAsync("https://stream.watsonplatform.net/speech-to-text/api/v1/recognize?continuous=true&model=" + model,
                        content).Result;

                if (!response.IsSuccessStatusCode) return null;
                var res = response.Content.ReadAsStringAsync().Result;
                
                sw.Stop();
                
                return new SpeechRecognitionResult()
                {
                    JSONResult = res,
                    StatusCode = 200,
                    ExternalServiceTimeInMilliseconds = sw.ElapsedMilliseconds
                };
            }
        }
    }
}