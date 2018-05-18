using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using Services.Classes;
using Services.IServices;
using Services.Models;

namespace Services.Services
{
    class AzureSTTService : IAzureSTTService
    {

        private static AzureAuthentication _authentication;

        public SpeechRecognitionResult ParseSpeectToText(string[] args)
        {

            // Note: Sign up at https://azure.microsoft.com/en-us/try/cognitive-services/ to get a subscription key.  
            // Navigate to the Speech tab and select Bing Speech API. Use the subscription key as Client secret below.
            if (_authentication == null)
            {
                _authentication = new AzureAuthentication("65d5cd5f1db5453a8ba1168b9bbce7a5");
            }

            string requestUri = args[0];/*.Trim(new char[] { '/', '?' });*/

            string host = @"speech.platform.bing.com";
            string contentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";

            /*
             * Input your own audio file or use read from a microphone stream directly.
             */
            
            //string audioFile = args[1];
            string audioBase64 = args[1];
            
            
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

            WebProxy myproxy = new WebProxy("127.0.0.1", 3128) {BypassProxyOnLocal = false};
            request.Proxy = myproxy;
            
            if (!string.IsNullOrEmpty(audioBase64))
            {
                var bytes = Convert.FromBase64String(audioBase64);
                using (ms = new MemoryStream(bytes))
                {
                    /*
                        * Open a request stream and write 1024 byte chunks in the stream one at a time.
                        */
                    byte[] buffer = null;
                    int bytesRead = 0;
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        /*
                         * Read 1024 raw bytes from the input audio file.
                         */
                        buffer = new Byte[checked((uint) Math.Min(1024, (int) ms.Length))];
                        while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                        }

                        // Flush
                        requestStream.Flush();
                    }

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    
                    using (WebResponse response = request.GetResponse())
                    {
                        var statusCode = ((HttpWebResponse) response).StatusCode.ToString();
                        int.TryParse(statusCode, out int statusCodeInt);

                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            responseString = sr.ReadToEnd();
                        }
                        
                        sw.Stop();
                        return new SpeechRecognitionResult()
                        {
                            StatusCode = statusCodeInt,
                            JSONResult = responseString,
                            ExternalServiceTimeInMilliseconds = sw.ElapsedMilliseconds
                        };
                    }   
                }
                
            }

            return null;
        }
    
    }
}
