using Services.Models;

namespace Services.IServices
{
    public interface IAzureSTTService
    {
        SpeechRecognitionResult ParseSpeectToText(string[] args);
    }
}