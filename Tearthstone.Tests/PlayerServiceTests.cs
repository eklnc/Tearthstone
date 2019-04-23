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
    public class PlayerServiceTests
    {
        private static IServiceProvider _serviceProvider;
        private IPlayerService _playerService;

        [SetUp]
        protected void SetUp()
        {
            var bootstrap = new Bootstrap();
            _serviceProvider = bootstrap.RegisterServices(_serviceProvider);
            _playerService = _serviceProvider.GetService<IPlayerService>();
        }

        [Test]
        public void PlayCard_NotPlaying_WhenManaCostTooMuch()
        {
            var gameModel = new GameModel
            {
                PlayingPlayer = new PlayerModel { Mana = 1, Hand = new List<CardBase> { new FiveDamageSpell() } }
            };
            int oldManaOfPlayingPlayer = gameModel.PlayingPlayer.Mana;
            _playerService.PlayCard(0, gameModel);
            Assert.IsTrue(gameModel.PlayingPlayer.Mana == oldManaOfPlayingPlayer);
        }

        [Test]
        public void PlayCard_ManaValueDecrease_WhenEverythingNormal()
        {
            var gameModel = new GameModel
            {
                PlayingPlayer = new PlayerModel { Mana = 5, Hand = new List<CardBase> { new FiveDamageSpell() }, Used = new List<CardBase>() },
                WaitingPlayer = new PlayerModel { Health = 10 }
            };
            _playerService.PlayCard(0, gameModel);
            Assert.IsTrue(gameModel.PlayingPlayer.Mana == 0);
        }
    }
}
