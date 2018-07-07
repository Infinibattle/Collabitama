using OnitamaTestClient.Enums;

namespace OnitamaTestClient.Models {
    public class Card {
        public CardType Type { get; set; }
        public Position[] Targets { get; set; }
    }
}