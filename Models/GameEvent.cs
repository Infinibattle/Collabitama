using System;
using OnitamaTestClient.Models.Enums;

namespace OnitamaTestClient.Models {
    public abstract class Move {
        public CardType UsedCard { get; }

        private Move(CardType usedCard) {
            this.UsedCard = usedCard;
        }

        public class Pass : Move {
            public Pass(CardType usedCard) : base(usedCard) { }
        }

        public class Play : Move {
            public Position From { get; }
            public Position To { get; }

            public Play(CardType usedCard, Position from, Position to)
                : base(usedCard) {
                this.From = from;
                this.To = to;
            }
        }
    }
}