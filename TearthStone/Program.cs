using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Tearthstone.Bootstrapper;
using TearthStone.Service.Interfaces;

namespace TearthStone
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            var bootstrap = new Bootstrap();
            _serviceProvider = bootstrap.RegisterServices(_serviceProvider);

            var logger = _serviceProvider.GetService<ILoggerFactory>().AddConsole().CreateLogger<Program>();

            var cardGameService = _serviceProvider.GetService<IGameService>();
            var game = cardGameService.StartGame();
            logger.LogInformation("Tearthstone başlıyor.");
            var boardService = _serviceProvider.GetService<IBoardService>();
            var playerService = _serviceProvider.GetService<IPlayerService>();
            while (game.PlayingPlayer.Health > 0 && game.WaitingPlayer.Health > 0)
            {
                boardService.PreparePlayer(game.PlayingPlayer);

                // state
                Console.WriteLine("*************************" + game.PlayingPlayer.Name + "*************************");
                logger.LogInformation("Tur: " + (game.PlayingPlayer.TurnNumber + game.WaitingPlayer.TurnNumber));
                logger.LogInformation(game.PlayingPlayer.Name + " oyuncusunun can değeri: " + game.PlayingPlayer.Health);
                logger.LogInformation(game.WaitingPlayer.Name + " oyuncusunun can değeri: " + game.WaitingPlayer.Health);
                // draw card
                logger.LogInformation(game.PlayingPlayer.Name + " kart çekiyor.");
                boardService.DrawCard(game);
                logger.LogInformation("Kalan kart sayınız: " + game.Board.PlayingPlayerRemaining.Count);
                // is dead? playing player
                if (game.PlayingPlayer.Health <= 0)
                {
                    logger.LogInformation(game.PlayingPlayer.Name + " kart çekerken canı kalmadığından ötürü oyunu kaybetti. Öldüğü can değeri: " + game.PlayingPlayer.Health);
                    logger.LogInformation(game.WaitingPlayer.Name + " kazandı.Kazandığı can değeri: " + game.WaitingPlayer.Health + ". Tebrikler...");
                    break;
                }

                var isEnd = false;
                while (!isEnd)
                {
                    // show hand cards
                    logger.LogWarning("Kullanilabilir Mana: " + game.PlayingPlayer.Mana);
                    logger.LogInformation("Kartlarınız aşağıdaki gibidir.");
                    for (var i = 0; i < game.PlayingPlayer.Hand.Count; i++)
                    {
                        var card = game.PlayingPlayer.Hand[i];
                        logger.LogWarning("KartNo: " + i + "| Mana:" + card.ManaCost + " | " + card.Title + " | " + card.Body);
                    }
                    
                    logger.LogInformation("Aşağıdaki durumlardan birini seçiniz.");
                    logger.LogInformation("0: Bir kart oyna");
                    logger.LogInformation("1: Turu bitir");

                    int consoleValue = -1;
                    bool isAvailableValue = false;
                    while (!isAvailableValue)
                    {
                        var val = Console.ReadLine();
                        isAvailableValue = int.TryParse(val, out consoleValue);
                        if (!isAvailableValue)
                        {
                            logger.LogInformation("Yanlış bir değer girdiniz. Lütfen tekrar giriniz.");
                        }
                        else if (isAvailableValue && !(consoleValue == 0 || consoleValue == 1))
                        {
                            isAvailableValue = false;
                            logger.LogInformation("Yanlış bir değer girdiniz. Lütfen tekrar giriniz.");
                        }
                    }

                    // end turn
                    if (consoleValue == 1)
                    {
                        isEnd = true;
                        var tempPlayer = game.PlayingPlayer;
                        game.PlayingPlayer = game.WaitingPlayer;
                        game.WaitingPlayer = tempPlayer;
                        Console.WriteLine("*********************************************************");
                        Console.WriteLine();
                    }
                    // play card
                    else
                    {
                        logger.LogInformation("Lütfen kart seçebilmek için yukarıdaki kartların başındaki numaralardan birini yazınız.");
                        int selectedCardIndex = -1;
                        isAvailableValue = false;
                        while (!isAvailableValue)
                        {
                            var val = Console.ReadLine();
                            isAvailableValue = int.TryParse(val, out selectedCardIndex);
                            if (!isAvailableValue)
                            {
                                logger.LogInformation("Yanlış bir değer girdiniz. Lütfen tekrar giriniz.");
                            }
                            else if (isAvailableValue && selectedCardIndex > game.PlayingPlayer.Hand.Count - 1)
                            {
                                isAvailableValue = false;
                                logger.LogInformation("Yanlış bir değer girdiniz. Lütfen tekrar giriniz.");
                            }
                        }
                        playerService.PlayCard(selectedCardIndex, game);
                        // is dead? waiting player
                        if (game.WaitingPlayer.Health <= 0)
                        {
                            logger.LogInformation(game.WaitingPlayer.Name + ", " + game.PlayingPlayer.Name + " tarafıdnan " + game.PlayingPlayer.Used[game.PlayingPlayer.Used.Count - 1].Title + " kartı ile öldürüldü. Öldüğü can değeri: " + game.WaitingPlayer.Health);
                            logger.LogInformation(game.PlayingPlayer.Name + " kazandı. Kazandığı can değeri: " + game.PlayingPlayer.Health + ". Tebrikler...");
                            break;
                        }
                    }
                }
            }

            logger.LogInformation("Oyun sonlandırıldı. Tearthstone oynadığınız için teşekkürler. Oyunu kapatmak için bir tuşa basın.");
            bootstrap.DisposeServices(_serviceProvider);
            Console.ReadKey();
        }
    }
}
