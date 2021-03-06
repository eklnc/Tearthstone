﻿using Tearthstone.Service.Models.Base;
using TearthStone.Service.Models;

namespace Tearthstone.Service.Models.Cards
{
    public class FiveDamageSpell : SpellBase
    {
        private int _manaCost = 5;

        public override int Id => 6;
        public override string Title => "Five Damage Spell";
        public override string Body => "This card deals damage to the enemy hero equal to its mana cost.";
        public override int ManaCost { get => _manaCost; set => _manaCost = value; }

        public override string CardAction(GameModel gameModel)
        {
            gameModel.WaitingPlayer.Health -= _manaCost;
            return "Rakibe " + _manaCost + " vurdun.";
        }
    }
}
