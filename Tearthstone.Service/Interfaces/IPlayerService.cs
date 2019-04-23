using TearthStone.Service.Models;

namespace TearthStone.Service.Interfaces
{
    public interface IPlayerService
    {
        void PlayCard(int cardIndex, GameModel gameModel);
    }
}
