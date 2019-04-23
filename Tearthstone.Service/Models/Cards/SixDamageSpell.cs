using Tearthstone.Service.Models.Base;
using TearthStone.Service.Models;

namespace Tearthstone.Service.Models.Cards
{
    public class SixDamageSpell : SpellBase
    {
        private int _manaCost = 6;

        public override int Id => 7;
        public override string Title => "Six Damage Spell";
        public override string Body => "This card deals damage to the enemy hero equal to its mana cost.";
        public override int ManaCost { get => _manaCost; set => _manaCost = value; }

        public override string CardAction(GameModel gameModel)
        {
            gameModel.WaitingPlayer.Health -= _manaCost;
            return "Rakibe " + _manaCost + " vurdun.";
        }
    }
}
