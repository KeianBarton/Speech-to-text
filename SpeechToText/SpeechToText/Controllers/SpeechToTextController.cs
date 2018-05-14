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
        public IActionResult ParseSpeectToTextAzure([FromBody] ClientWavObject clientInput)
        {
            var input = new[]
            {
                "https://speech.platform.bing.com/speech/recognition/interactive/cognitiveservices/v1?language=en-US&format=detailed",
                clientInput.Base64String
            };
            var result = _azureSTTService.ParseSpeectToText(input);

            if (result == null) return BadRequest("An unknown error has occured");

            return Ok(result.JSONResult);
        }

        [HttpPost]
        [Route("parseWatson")]
        public IActionResult ParseSpeectToTextWatson([FromBody] ClientWavObject clientInput)
        {
            var input = new[] {clientInput.Base64String};
            var result = _watsonSttService.ParseSpeectToText(input);

            if (result == null) return BadRequest("An unknown error has occured");

            return Ok(result.JSONResult);
        }

        public class ClientWavObject
        {
            public string Base64String { get; set; }
        }
    }
}