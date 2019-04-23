using Microsoft.Extensions.Logging;
using TearthStone.Service.Interfaces;
using TearthStone.Service.Models;

namespace TearthStone.Service.Impl
{
    public class CardService : ICardService
    {
        private readonly ILogger<CardService> _logger;
        public CardService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CardService>();
        }

        public void PlayCard(GameModel gameModel)
        {
            var log = gameModel.PlayingPlayer.Used[gameModel.PlayingPlayer.Used.Count - 1].CardAction(gameModel);
            _logger.LogInformation(log);
        }
    }
}
