using Collabitama.Client.Enums;

namespace Collabitama.Client.Models {
    public abstract class Move {
        private Move(CardType usedCard) {
            UsedCard = usedCard;
        }

        public CardType UsedCard { get; }

        public class Pass : Move {
            public Pass(CardType usedCard) : base(usedCard) { }
        }

        public class Play : Move {
            public Play(CardType usedCard, Position from, Position to)
                : base(usedCard) {
                From = from;
                To = to;
            }

            public Position From { get; }
            public Position To { get; }
        }
    }
}