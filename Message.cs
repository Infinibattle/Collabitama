namespace OnitamaTestClient
{
    public class Message
    {
        public MessageType Type { get; set; }
        public string JsonPayload { get; set; }
    }

    public enum MessageType {
        //bot to game
        MovePiece,
        Pass,

        //game to bot
        GameInfo,
        NewGameState,
        InvalidMove
    }
}
