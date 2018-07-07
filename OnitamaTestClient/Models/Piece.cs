using OnitamaTestClient.Enums;

namespace OnitamaTestClient.Models {
    public class Piece {
        public PlayerIdentityEnum Owner { get; set; }
        public PieceTypeEnum Type { get; set; }
        public Position PositionOnBoard { get; set; }
    }
}