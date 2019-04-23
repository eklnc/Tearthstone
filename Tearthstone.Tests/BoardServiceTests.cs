using NUnit.Framework;
using System;
using Tearthstone.Bootstrapper;
using TearthStone.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TearthStone.Service.Models;
using System.Collections.Generic;
using Tearthstone.Service.Models.Base;
using Tearthstone.Service.Models.Cards;

namespace Tearthstone.Tests
{
    [TestFixture]
    public class BoardServiceTests
    {
        private static IServiceProvider _serviceProvider;
        private IBoardService _boardService;

        [SetUp]
        protected void SetUp()
        {
            var bootstrap = new Bootstrap();
            _serviceProvider = bootstrap.RegisterServices(_serviceProvider);
            _boardService = _serviceProvider.GetService<IBoardService>();
        }

        [Test]
        public void PreparePlayer_TurnUp_WhenPrepearing()
        {
            var player = new PlayerModel { TurnNumber = 0, Mana = 0 };
            _boardService.PreparePlayer(player);
            Assert.IsTrue(player.TurnNumber == 1);
        }

        [Test]
        public void PreparePlayer_ManaUp_WhenPrepearing()
        {
            var player = new PlayerModel { TurnNumber = 0, Mana = 0 };
            _boardService.PreparePlayer(player);
            Assert.IsTrue(player.Mana == 1);
        }

        [Test]
        public void PreparePlayer_ManaDontUp_WhenTurnNumberAboveTen()
        {
            var player = new PlayerModel { TurnNumber = 15, Mana = 0 };
            _boardService.PreparePlayer(player);
            Assert.IsTrue(player.Mana == 10);
        }

        [Test]
        public void DrawCard_HealthBelowOne_WhenBleedingOut()
        {
            var gameModel = new GameModel
            {
                Board = new BoardModel { PlayingPlayerRemaining = new List<CardBase>() },
                PlayingPlayer = new PlayerModel { BleedingOutTurn = 10, Health = 9 }
            };
            _boardService.DrawCard(gameModel);
            Assert.IsTrue(gameModel.PlayingPlayer.Health <= 0);
        }

        [Test]
        public void DrawCard_CardBurn_WhenOverload()
        {
            var gameModel = new GameModel
            {
                Board = new BoardModel { PlayingPlayerRemaining = new List<CardBase> { new OneDamageSpell() } },
                PlayingPlayer = new PlayerModel
                {
                    Hand = new List<CardBase> { new OneDamageSpell(), new OneDamageSpell(), new OneDamageSpell(), new OneDamageSpell(), new OneDamageSpell() },
                    Used = new List<CardBase>()
                }
            };
            _boardService.DrawCard(gameModel);
            Assert.IsTrue(gameModel.PlayingPlayer.Used.Count == 1);
        }

        [Test]
        public void DrawCard_HandListCountUp_WhenEverythingNormal()
        {
            var gameModel = new GameModel
            {
                Board = new BoardModel { PlayingPlayerRemaining = new List<CardBase> { new OneDamageSpell() } },
                PlayingPlayer = new PlayerModel
                {
                    Hand = new List<CardBase> { new OneDamageSpell(), new OneDamageSpell(), new OneDamageSpell(), new OneDamageSpell() }
                }
            };
            int oldCountOfHandList = gameModel.PlayingPlayer.Hand.Count;
            _boardService.DrawCard(gameModel);
            Assert.IsTrue(gameModel.PlayingPlayer.Hand.Count > oldCountOfHandList);
        }
    }
}
