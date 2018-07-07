using Collabitama.Client.Enums;

namespace Collabitama.Client.Models
{
    public class Message
    {
        public MessageType Type { get; set; }
        public string JsonPayload { get; set; }
    }
}
