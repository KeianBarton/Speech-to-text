# Speech-to-text

Implementations

Azure Bing Speech Api  : Free Tier
Documentation : https://docs.microsoft.com/en-gb/azure/cognitive-services/speech/home

Example Response structure (JSON Response):

OK
{
  "RecognitionStatus": "Success",
  "Offset": 22500000,
  "Duration": 21000000,
  "NBest": [{
    "Confidence": 0.941552162,
    "Lexical": "find a funny movie to watch",
    "ITN": "find a funny movie to watch",
    "MaskedITN": "find a funny movie to watch",
    "Display": "Find a funny movie to watch."
  }]
} 

	
Watson Speech-To-Text : Free Tier
Documentation: https://www.ibm.com/watson/developercloud/speech-to-text/api/v1/

Example Response structure (JSON Response):
{
  "results": [
    {
      "word_alternatives": [
        {
          "start_time": 0.09,
          "alternatives": [
            {
              "confidence": 1.0,
              "word": "latest"
            }
          ],
          "end_time": 0.6
        },
        {
          "start_time": 0.6,
          "alternatives": [
            {
              "confidence": 1.0,
              "word": "weather"
            }
          ],
          "end_time": 0.85
        },
        . . .
        {
          "start_time": 6.85,
          "alternatives": [
            {
              "confidence": 0.9988,
              "word": "on"
            }
          ],
          "end_time": 7.0
        },
        {
          "start_time": 7.0,
          "alternatives": [
            {
              "confidence": 0.9953,
              "word": "Sunday"
            }
          ],
          "end_time": 7.71
        }
      ],
      "keywords_result": {
        "colorado": [
          {
            "normalized_text": "Colorado",
            "start_time": 6.26,
            "confidence": 0.999,
            "end_time": 6.85
          }
        ],
        "tornadoes": [
          {
            "normalized_text": "tornadoes",
            "start_time": 4.7,
            "confidence": 0.964,
            "end_time": 5.52
          }
        ]
      },
      "alternatives": [
        {
          "timestamps": [
            [
              "the",
              0.03,
              0.09
            ],
            [
              "latest",
              0.09,
              0.6
            ],
            . . .
            [
              "on",
              6.85,
              7.0
            ],
            [
              "Sunday",
              7.0,
              7.71
            ]
          ],
          "confidence": 0.968,
          "transcript": "the latest weather report a line of severe thunderstorms with several possible tornadoes is approaching Colorado on Sunday "
        }
      ],
      "final": true
    }
  ],
  "result_index": 0
}