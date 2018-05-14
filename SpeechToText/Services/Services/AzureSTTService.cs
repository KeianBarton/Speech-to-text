using System;
using System.IO;
using System.Net;
using Services.Classes;
using Services.IServices;
using Services.Models;

namespace Services.Services
{
    internal class AzureSTTService : IAzureSTTService
    {
        private static AzureAuthentication _authentication;

        public SpeechRecognitionResult ParseSpeectToText(string[] args)
        {
            // Note: Sign up at https://azure.microsoft.com/en-us/try/cognitive-services/ to get a subscription key.  
            // Navigate to the Speech tab and select Bing Speech API. Use the subscription key as Client secret below.
            if (_authentication == null) _authentication = new AzureAuthentication("b04ce66251c44fd98629ff9d5b9d446f");

            var requestUri = args[0]; /*.Trim(new char[] { '/', '?' });*/

            var host = @"speech.platform.bing.com";
            var contentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";

            /*
             * Input your own audio file or use read from a microphone stream directly.
             */
            var base64String = args[1];

            string responseString;

            MemoryStream ms;

            var token = _authentication.GetAccessToken();
            Console.WriteLine("Token: {0}\n", token);
            Console.WriteLine("Request Uri: " + requestUri + Environment.NewLine);

            HttpWebRequest request;
            request = (HttpWebRequest) WebRequest.Create(requestUri);
            request.SendChunked = true;
            request.Accept = @"application/json;text/xml";
            request.Method = "POST";
            request.ProtocolVersion = HttpVersion.Version11;
            request.Host = host;
            request.ContentType = contentType;
            request.Headers["Authorization"] = "Bearer " + token;

            if (!string.IsNullOrEmpty(base64String))
            {
                try
                {
                    var bytes = Convert.FromBase64String(base64String);
                    var content = new MemoryStream(bytes);
                    byte[] buffer = null;
                    var bytesRead = 0;
                    using (var requestStream = request.GetRequestStream())
                    {
                        /*
                        * Read 1024 raw bytes from the input audio file.
                         */

                        buffer = new byte[checked((uint) Math.Min(1024, (int) content.Length))];
                        while ((bytesRead = content.Read(buffer, 0, buffer.Length)) != 0)
                            requestStream.Write(buffer, 0, bytesRead);

                        // Flush
                        requestStream.Flush();


                        using (var response = request.GetResponse())
                        {
                            var statusCode = ((HttpWebResponse) response).StatusCode.ToString();
                            int.TryParse(statusCode, out var statusCodeInt);

                            using (var sr = new StreamReader(response.GetResponseStream()))
                            {
                                responseString = sr.ReadToEnd();
                            }

                            return new SpeechRecognitionResult
                            {
                                StatusCode = statusCodeInt,
                                JSONResult = responseString
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                }
            }

            return null;
        }
    }
}