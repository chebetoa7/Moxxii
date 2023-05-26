namespace Moxxii.Api.Body
{
    public class fcmBodyData
    {
        public string to { get; set; }
        public Notification notification { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public string key_1 { get; set; }
        public string key_2 { get; set; }
    }
    public class Notification
    {
        public string body { get; set; }
        public string title { get; set; }
    }
}
