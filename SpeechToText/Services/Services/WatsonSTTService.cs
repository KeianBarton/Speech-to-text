using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Services.IServices;
using Services.Models;

namespace Services.Services
{
    internal class WatsonSTTService : IWatsonSTTService
    {
        private static readonly string username = "d99e7579-fcc3-403c-bb6d-ac4c86eec841";
        private static readonly string password = "plRwOk0IuiQb";
        private static readonly string model = "en-US_NarrowbandModel";

        public SpeechRecognitionResult ParseSpeectToText(string[] args)
        {
            var base64String = args[0];
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(
                            username + ":" + password)));

                var bytes = Convert.FromBase64String(base64String);
                var content = new StreamContent(new MemoryStream(bytes));
                content.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");

                var response = client
                    .PostAsync(
                        "https://stream.watsonplatform.net/speech-to-text/api/v1/recognize?continuous=true&model=" +
                        model,
                        content).Result;
                if (!response.IsSuccessStatusCode) return null;
                var res = response.Content.ReadAsStringAsync().Result;
                return new SpeechRecognitionResult
                {
                    JSONResult = res,
                    StatusCode = 200
                };
            }

            return null;
        }
    }
}