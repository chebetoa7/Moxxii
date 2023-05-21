namespace Moxxii.Api.Body
{
    public class fcmBody
    {
        public Notification notification { get; set; }
        public string to { get; set; }
    }
    public class Notification
    {
        public string body { get; set; }
        public string title { get; set; }
    }

   
}
