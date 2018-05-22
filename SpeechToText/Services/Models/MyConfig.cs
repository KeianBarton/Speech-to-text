namespace Services.Models
{
    public class MyConfig
    {
        public bool UseProxy { get; set; }
        public string ProxyHost { get; set; }
        public int ProxyPort { get; set; }
        public string BingSubscriptionKey { get; set; }
        public string WatsonUsername { get; set; }
        public string WatsonPassword { get; set; }
    }
}
