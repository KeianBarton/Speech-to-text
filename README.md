## Getting Started

This project is designed to explore and test the implementations of speech to text services. Currently the following services are tested:

* Bing Speech
* Watson Speech to Text
* AWS Transcribe 

There is also a blog post at https://sacode.co.uk/speech-to-text-service-comparisons/

## Prerequisites

* Visual Studio 2017 with the AWS toolkit
* Visual Studio Code with the Angular6 CLI installed.
* An AWS Account.
* An Azure Account.
* An IBM Account.

## Installing

The project is fairly simple to run, open up visual studio and head to the appsettings.json file. Within it you will need to set your keys from "Azure Bing to Speech" and your "IBM Watson" keys to. AWS should be configured through the AWS toolkits AWS Solution Explorer Window. If you're not using a proxy then make sure to set UseProxy to false too.
Then start the VS project, if all is successful you should see a swagger window open.

Next open the client project in Visual Studio Code and within the integrated terminal run

```
yarn start
```

Now when you go to http://localhost:4200 you should get a nice sound recording application. Record some speech and watch the application do its magic!


## Authors

* [Scott Alexander](https://www.linkedin.com/in/scott-robert-alexander/)
* [Kate Short](https://www.linkedin.com/in/kate-short/)
* [Keian Barton](https://www.linkedin.com/in/keianbarton/)

Future Plans

...