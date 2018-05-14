using Services.Models;

namespace Services.IServices
{
    public interface IWatsonSTTService
    {
        SpeechRecognitionResult ParseSpeectToText(string[] args);
    }
}