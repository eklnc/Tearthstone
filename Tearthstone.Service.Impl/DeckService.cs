using System.Collections.Generic;
using Tearthstone.Service.Models.Base;
using Tearthstone.Service.Models.Cards;
using TearthStone.Service.Interfaces;
using TearthStone.Service.Models;

namespace TearthStone.Service.Impl
{
    public class DeckService : IDeckService
    {
        public DeckModel CreateDefaultDeck()
        {
            var deck = new DeckModel
            {
                Id = 1,
                Title = "Default Deck",
                Cards = new List<CardBase> {
                    new ZeroDamageSpell(),new ZeroDamageSpell(),
                    new OneDamageSpell(),new OneDamageSpell(),
                    new TwoDamageSpell(),new TwoDamageSpell(),new TwoDamageSpell(),
                    new ThreeDamageSpell(),new ThreeDamageSpell(),new ThreeDamageSpell(),new ThreeDamageSpell(),
                    new FourDamageSpell(),new FourDamageSpell(),new FourDamageSpell(),
                    new FiveDamageSpell(),new FiveDamageSpell(),
                    new SixDamageSpell(),new SixDamageSpell(),
                    new SevenDamageSpell(),
                    new EightDamageSpell()
                }
            };

            return deck;
        }
    }
}
