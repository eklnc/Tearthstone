using System.Collections.Generic;
using Tearthstone.Service.Models.Base;
using TearthStone.Service.Models;

namespace TearthStone.Service.Interfaces
{
    public interface IGameService
    {
        GameModel StartGame();
        PlayerModel CreatePlayer(int id, string name, DeckModel deck);
        List<CardBase> CreateStartHand(DeckModel deck);
        List<CardBase> CreateRemainingHand(DeckModel defaultDeck, List<CardBase> handCards);
    }
}
