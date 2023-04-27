namespace Moxxii.Api.Body
{
    public class fcmBody
    {
        public Data data { get; set; }
        public string to { get; set; }
    }
    public class Data
    {
        public string title { get; set; }
        public string image { get; set; }
        public string message { get; set; }
    }
}
