using System;
using System.Collections.Generic;
using Tearthstone.Service.Models.Base;
using TearthStone.Service.Interfaces;
using TearthStone.Service.Models;

namespace TearthStone.Service.Impl
{
    public class GameService : IGameService
    {
        private readonly IDeckService _deckService;
        
        public GameService(IDeckService deckService)
        {
            _deckService = deckService;
        }

        public GameModel StartGame()
        {
            var defaultDeck = _deckService.CreateDefaultDeck();

            var cardGameModel = new GameModel
            {
                Board = new BoardModel
                {
                    PlayingPlayerRemaining = new List<CardBase>(),
                    WaitingPlayerRemaining = new List<CardBase>()
                },
                PlayingPlayer = CreatePlayer(1, "Player1", defaultDeck),
                WaitingPlayer = CreatePlayer(2, "Player2", defaultDeck)
            };

            cardGameModel.Board.PlayingPlayerRemaining = CreateRemainingHand(defaultDeck, cardGameModel.PlayingPlayer.Hand);
            cardGameModel.Board.WaitingPlayerRemaining = CreateRemainingHand(defaultDeck, cardGameModel.WaitingPlayer.Hand);

            return cardGameModel;
        }

        public PlayerModel CreatePlayer(int id, string name, DeckModel defaultDeck)
        {
            var player = new PlayerModel
            {
                Id = id,
                Name = name,
                SelectedDeck = defaultDeck,
                Hand = CreateStartHand(defaultDeck),
                Used = new List<CardBase>()
            };

            return player;
        }

        public List<CardBase> CreateStartHand(DeckModel deck)
        {
            var selectedCards = new List<CardBase>();
            var randomList = new List<string>();
            var rnd = new Random();

            while (randomList.Count < 3)
            {
                int randomNumber = rnd.Next(0, 20);
                if (!randomList.Contains(randomNumber.ToString()))
                {
                    randomList.Add(randomNumber.ToString());

                    selectedCards.Add(deck.Cards[randomNumber]);
                }
            }

            return selectedCards;
        }

        public List<CardBase> CreateRemainingHand(DeckModel defaultDeck, List<CardBase> handCards)
        {
            var remainingCards = new List<CardBase>();
            remainingCards.AddRange(defaultDeck.Cards);

            foreach (var card in handCards)
            {
                remainingCards.Remove(card);
            }

            return remainingCards;
        }
    }
}
