using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Tearthstone.Service.Models.Base;
using TearthStone.Service.Interfaces;
using TearthStone.Service.Models;

namespace TearthStone.Service.Impl
{
    public class BoardService : IBoardService
    {
        private readonly ILogger<BoardService> _logger;
        private readonly IDeckService _deckService;

        public BoardService(ILoggerFactory loggerFactory, IDeckService deckService)
        {
            _logger = loggerFactory.CreateLogger<BoardService>();
            _deckService = deckService;
        }
        
        public void PreparePlayer(PlayerModel player)
        {
            player.TurnNumber++;
            if (player.TurnNumber < 10)
            {
                player.Mana = player.TurnNumber;
            }
            else
            {
                player.Mana = 10;
            }
        }

        public void DrawCard(GameModel gameModel)
        {
            if (gameModel.Board.PlayingPlayerRemaining.Count == 0)
            {
                gameModel.PlayingPlayer.BleedingOutTurn++;
                gameModel.PlayingPlayer.Health -= gameModel.PlayingPlayer.BleedingOutTurn;
                _logger.LogCritical("Destende hiç kart olmadığından can değerin " + gameModel.PlayingPlayer.BleedingOutTurn + " azaldı.");
                return;
            }

            var rnd = new Random();
            var selectedIndex = rnd.Next(gameModel.Board.PlayingPlayerRemaining.Count);
            var selectedCard = gameModel.Board.PlayingPlayerRemaining[selectedIndex];
            gameModel.Board.PlayingPlayerRemaining.Remove(selectedCard);

            if (gameModel.PlayingPlayer.Hand.Count == 5)
            {
                gameModel.PlayingPlayer.Used.Add(selectedCard);
                _logger.LogCritical("Elinde zaten 5 kart olduğu için " + selectedCard.Title + " kartı yanmıştır.");
                return;
            }

            gameModel.PlayingPlayer.Hand.Add(selectedCard);

            _logger.LogInformation(selectedCard.Title + " kartını çektin.");
        }
    }
}
