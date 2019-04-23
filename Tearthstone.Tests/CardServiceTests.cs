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
    public class CardServiceTests
    {
        private static IServiceProvider _serviceProvider;
        private ICardService _cardService;

        [SetUp]
        protected void SetUp()
        {
            var bootstrap = new Bootstrap();
            _serviceProvider = bootstrap.RegisterServices(_serviceProvider);
            _cardService = _serviceProvider.GetService<ICardService>();
        }

        [Test]
        public void PlayDamageSpellCard_HealthDecrease_WhenHit()
        {
            var gameModel = new GameModel {
                Board = new BoardModel(),
                PlayingPlayer = new PlayerModel { Used = new List<CardBase> { new FiveDamageSpell() } },
                WaitingPlayer = new PlayerModel { Health = 5 },
            };
            _cardService.PlayCard(gameModel);
            Assert.IsTrue(gameModel.WaitingPlayer.Health == 0);
        }
    }
}
