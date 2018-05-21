using System;
using System.Collections.Generic;
using System.Text;
using Services.Models;

namespace Services.IServices
{
    public interface IBingSpeechService
    {
        SpeechRecognitionResult ParseSpeectToText(string[] args);
    }
}
