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
            var input = new string []{"https://speech.platform.bing.com/recognize", "C:\\Users\\scott.alexander\\Downloads\\carlin_pc.wav"};
            _azureSTTService.ParseSpeectToText(input);
            return Ok();
        }
    }
}