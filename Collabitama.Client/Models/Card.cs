using Collabitama.Client.Enums;

namespace Collabitama.Client.Models {
    public class Card {
        public CardType Type { get; set; }
        public Position[] Targets { get; set; }
    }
}