using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface IAzureSTTService
    {
        void ParseSpeectToText(string[] args);
    }
}
