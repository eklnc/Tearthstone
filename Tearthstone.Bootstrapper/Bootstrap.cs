using Microsoft.Extensions.DependencyInjection;
using System;
using TearthStone.Service.Impl;
using TearthStone.Service.Interfaces;

namespace Tearthstone.Bootstrapper
{
    public class Bootstrap
    {
        public IServiceProvider RegisterServices(IServiceProvider serviceProvider)
        {
            var collection = new ServiceCollection();
            collection.AddLogging();
            collection.AddSingleton<IGameService, GameService>();
            collection.AddSingleton<IBoardService, BoardService>();
            collection.AddSingleton<IDeckService, DeckService>();
            collection.AddSingleton<IPlayerService, PlayerService>();
            collection.AddSingleton<ICardService, CardService>();
            serviceProvider = collection.BuildServiceProvider();
            return serviceProvider;
        }

        public void DisposeServices(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                return;
            }
            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
