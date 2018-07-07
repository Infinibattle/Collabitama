using Collabitama.Client.Enums;

namespace Collabitama.Client.Models {
    public class Piece {
        public PlayerIdentityEnum Owner { get; set; }
        public PieceTypeEnum Type { get; set; }
        public Position PositionOnBoard { get; set; }
    }
}