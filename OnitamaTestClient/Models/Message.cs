using OnitamaTestClient.Enums;

namespace OnitamaTestClient.Models
{
    public class Message
    {
        public MessageType Type { get; set; }
        public string JsonPayload { get; set; }
    }
}
