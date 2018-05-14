using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;


namespace STTRest.Controllers
{
    [Route("api/speechToText")]
    public class SpeechToTextController : Controller
    {

        private readonly IAzureSTTService _azureSTTService;
        private readonly IWatsonSTTService _watsonSttService;

        public SpeechToTextController(IAzureSTTService azureSTTService, IWatsonSTTService watsonSttService)
        {
            _azureSTTService = azureSTTService;
            _watsonSttService = watsonSttService;
        }

        [HttpPost]
        [Route("parseAzure")]
        public IActionResult ParseSpeectToTextAzure(ClientWavObject clientInput)
        {
            var sw = new Stopwatch();
            sw.Start();
            
            var input = new string []{"https://speech.platform.bing.com/speech/recognition/interactive/cognitiveservices/v1?language=en-US&format=detailed", "", null};
            var result = _azureSTTService.ParseSpeectToText(input);

            if (result == null)
            {
                return BadRequest("An unknown error has occured");
            }
            
            sw.Stop();
            result.TotalBackendTimeInMilliseconds = sw.ElapsedMilliseconds;
            return Ok(result);
        }
        
        [HttpPost]
        [Route("parseWatson")]
        public IActionResult ParseSpeectToTextWatson([FromBody] ClientWavObject clientInput)
        {

            var sw = new Stopwatch();
            sw.Start();
            
            var input = new string[] {clientInput.Base64String};
            var result = _watsonSttService.ParseSpeectToText(input);

            if (result == null)
            {
                return BadRequest("An unknown error has occured");
            }
            
            sw.Stop();
            result.TotalBackendTimeInMilliseconds = sw.ElapsedMilliseconds;
            return Ok(result);
        }

        public class ClientWavObject
        {
            public string Base64String { get; set; }
        }
    }
}