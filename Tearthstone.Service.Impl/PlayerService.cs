using Microsoft.Extensions.Logging;
using System;
using TearthStone.Service.Interfaces;
using TearthStone.Service.Models;

namespace TearthStone.Service.Impl
{
    public class PlayerService : IPlayerService
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly ICardService _cardService;

        public PlayerService(ILoggerFactory loggerFactory, ICardService cardService)
        {
            _logger = loggerFactory.CreateLogger<PlayerService>();
            _cardService = cardService;
        }
        
        public void PlayCard(int cardIndex, GameModel gameModel)
        {
            var playedCard = gameModel.PlayingPlayer.Hand[cardIndex];

            if (gameModel.PlayingPlayer.Mana < playedCard.ManaCost)
            {
                _logger.LogError(playedCard.Title + " kartını mana değerin yeterli olmağı için oynayamazsın.");
                return;
            }

            gameModel.PlayingPlayer.Mana -= playedCard.ManaCost;
            gameModel.PlayingPlayer.Used.Add(playedCard);
            gameModel.PlayingPlayer.Hand.Remove(playedCard);
            
            _cardService.PlayCard(gameModel);
        }
    }
}
