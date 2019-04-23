using System.Collections.Generic;
using Tearthstone.Service.Models.Base;

namespace TearthStone.Service.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public int Health { get; set; } = 30;
        public int Mana { get; set; } = 0;
        public int TurnNumber { get; set; } = 0;
        public string Name { get; set; }
        public DeckModel SelectedDeck { get; set; }
        public List<CardBase> Hand { get; set; }
        public List<CardBase> Used { get; set; }
        public int BleedingOutTurn { get; set; } = 0;
    }
}
