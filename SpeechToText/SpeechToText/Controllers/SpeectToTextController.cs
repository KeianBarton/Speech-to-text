using Microsoft.AspNetCore.Mvc;
using Services.IServices;


namespace STTRest.Controllers
{
    [Route("api/controller")]
    public class SpeectToTextController : Controller
    {

        private readonly IAzureSTTService _azureSTTService;

        public SpeectToTextController(IAzureSTTService azureSTTService)
        {
            _azureSTTService = azureSTTService;
        }

        [HttpPost]
        [Route("parse")]
        public IActionResult ParseSpeectToText()
        {
            var input = new string []{"https://speech.platform.bing.com/speech/recognition/interactive/cognitiveservices/v1?language=en-US&format=detailed", "Data/miller_larry.wav"};
            var result = _azureSTTService.ParseSpeectToText(input);

            if (result == null)
            {
                return BadRequest("An unknown error has occured");
            }
            
            return Ok(result.JSONResult);
        }
    }
}