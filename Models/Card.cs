using System;
using System.Collections.Generic;
using System.Linq;
using OnitamaTestClient.Models.Enums;

namespace OnitamaTestClient.Models {
    public class Card {
        public CardType Type { get; set; }
        public Position[] Targets { get; set; }
    }
}