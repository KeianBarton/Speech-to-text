using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using Services.Classes;
using Services.IServices;
using Services.Models;

namespace Services.Services
{
    public class AWSService : IAWSService
    {

        private readonly IAmazonUploader _amazonUploaderService;
        private readonly IAmazonTranscribeService _amazonTranscribeService;

        public AWSService(IAmazonUploader amazonUploader, IAmazonTranscribeService amazonTranscribeService)
        {
            _amazonUploaderService = amazonUploader;
            _amazonTranscribeService = amazonTranscribeService;
        }

        public async Task<SpeechRecognitionResult> ParseSpeectToText(string[] args)
        {
            if (string.IsNullOrEmpty(args[0])) return null;

            var uploadDetails = await _amazonUploaderService.UploadBase64Wav(args[0]);
            if (string.IsNullOrEmpty(uploadDetails.FileRoute)) return null;

            var transciptionJobName = "Transcribe_" + uploadDetails.FileRoute;
            var request = new StartTranscriptionJobRequest()
            {
                Media = new Media()
                {
                    MediaFileUri = "https://s3." + uploadDetails.BucketRegion + ".amazonaws.com/" + uploadDetails.BucketName + "/" + uploadDetails.FileRoute
                },
                LanguageCode = new LanguageCode(LanguageCode.EnUS),
                MediaFormat = new MediaFormat("Wav"),
                TranscriptionJobName = transciptionJobName
            };

            try
            {
                var res = await _amazonTranscribeService.StartTranscriptionJobAsync(request);

                var jobComplete = false;
                GetTranscriptionJobResponse jobRes = null;
                while (!jobComplete)
                {
                    jobRes = await _amazonTranscribeService.GetTranscriptionJobAsync(new GetTranscriptionJobRequest()
                    {
                        TranscriptionJobName = transciptionJobName
                    });

                    if (jobRes != null && jobRes.TranscriptionJob.TranscriptionJobStatus !=
                        TranscriptionJobStatus.COMPLETED)
                    {
                        System.Threading.Thread.Sleep(5000);
                    }
                    else
                    {
                        jobComplete = true;
                    }
                }

                var jsonRes = "";
                using (var client = HttpProxyClient.CreateHttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client
                    .GetAsync(jobRes.TranscriptionJob.Transcript.TranscriptFileUri).Result;
                    //.GetAsync(testInput).Result;

                    if (!response.IsSuccessStatusCode) return null;
                    jsonRes = response.Content.ReadAsStringAsync().Result;
                    //jsonRes = wc.(jobRes.TranscriptionJob.Transcript.TranscriptFileUri);
                }

                // Once done delete the file
                await _amazonUploaderService.DeleteFile(uploadDetails.FileRoute);

                return new SpeechRecognitionResult()
                {
                    StatusCode = 200,
                    JSONResult =jsonRes
                };

            }
            catch (AmazonTranscribeServiceException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return null;
        }

    }
}