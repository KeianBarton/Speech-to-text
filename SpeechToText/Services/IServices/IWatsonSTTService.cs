using System;
using System.Collections.Generic;
using System.Text;
using Services.Models;

namespace Services.IServices
{
    public interface IWatsonSTTService
    {
        SpeechRecognitionResult ParseSpeectToText(string[] args);
    }
}
