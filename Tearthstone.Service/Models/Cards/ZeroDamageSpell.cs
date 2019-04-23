using Tearthstone.Service.Models.Base;
using TearthStone.Service.Models;

namespace Tearthstone.Service.Models.Cards
{
    public class ZeroDamageSpell : SpellBase
    {
        private int _manaCost = 0;

        public override int Id => 1;
        public override string Title => "Zero Damage Spell";
        public override string Body => "This card deals damage to the enemy hero equal to its mana cost.";
        public override int ManaCost { get => _manaCost; set => _manaCost = value; }

        public override string CardAction(GameModel gameModel)
        {
            gameModel.WaitingPlayer.Health -= _manaCost;
            return "Rakibe " + _manaCost + " vurdun.";
        }
    }
}
