namespace Moxxii.Api.Body
{
    public class fcmBody
    {
        public Notification_ notification { get; set; }
        public string to { get; set; }
    }
    public class Notification_
    {
        public string body { get; set; }
        public string title { get; set; }
    }

   
}
