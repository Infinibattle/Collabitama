using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnitamaTestClient.Enums;

namespace OnitamaTestClient.Models {
    public class GameState {
        public List<Card> MyHand { get; set; }
        public List<Card> OpponentsHand { get; set; }
        public List<Piece> Pieces { get; set; }
        public List<Piece> MyPieces => Pieces.Where(p => p.Owner == CurrentlyPlaying).ToList();
        public Card FifthCard { get; set; }
        public PlayerIdentityEnum CurrentlyPlaying { get; set; }

        public int BoardRevision { get; set; }

        public string PrintBoard() {
            const int dimX = 5;
            const int dimY = 5;

            var stringBuilder = new StringBuilder();

            //upper board deliminations
            stringBuilder.Append(" + ");
            for (var i = 0; i < dimX; i++) stringBuilder.Append($" {i} ");

            stringBuilder.Append(" + ");
            stringBuilder.Append("\n");

            // board
            for (var y = dimY - 1; y >= 0; y--) {
                stringBuilder.Append($" {y} ");
                for (var x = 0; x < dimX; x++) {
                    var piece = Pieces.Find(p => p.PositionOnBoard.X == x && p.PositionOnBoard.Y == y);
                    if (piece == null) {
                        stringBuilder.Append(" . ");
                        continue;
                    }

                    var marking = piece.Type == PieceTypeEnum.Pawn ? "p" : "k";
                    if (piece.Owner == CurrentlyPlaying) marking = marking.ToUpper();

                    stringBuilder.Append($" {marking} ");
                }

                stringBuilder.Append($" {y} ");
                stringBuilder.Append("\n");
            }

            //lower board deliminations
            stringBuilder.Append(" + ");
            for (var i = 0; i < dimX; i++) stringBuilder.Append($" {i} ");

            stringBuilder.Append(" + ");

            stringBuilder.Append("\n");

            return stringBuilder.ToString();
        }

        public string PrintHand(List<Card> hand) {
            var stringBuilder = new StringBuilder();
            foreach (var card in hand) {
                stringBuilder.Append(PrintCard(card));
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }

        public string PrintCard(Card card) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Card: " + card.Type + "\n");
            foreach (var target in card.Targets) stringBuilder.Append("(" + target.X + ", " + target.Y + ")  ");

            return stringBuilder.ToString();
        }
    }
}