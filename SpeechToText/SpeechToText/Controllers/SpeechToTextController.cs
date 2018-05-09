using Microsoft.AspNetCore.Mvc;
using Services.IServices;


namespace STTRest.Controllers
{
    [Route("api/controller")]
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
        public IActionResult ParseSpeectToTextAzure()
        {
            var input = new string []{"https://speech.platform.bing.com/speech/recognition/interactive/cognitiveservices/v1?language=en-US&format=detailed", "Data/miller_larry.wav"};
            var result = _azureSTTService.ParseSpeectToText(input);

            if (result == null)
            {
                return BadRequest("An unknown error has occured");
            }
            
            return Ok(result.JSONResult);
        }
        
        [HttpPost]
        [Route("parseWatson")]
        public IActionResult ParseSpeectToTextWatson()
        {
            var input = new string []{"Data/miller_larry.wav"};
            var result = _watsonSttService.ParseSpeectToText(input);

            if (result == null)
            {
                return BadRequest("An unknown error has occured");
            }
            
            return Ok(result.JSONResult);
        }
    }
}