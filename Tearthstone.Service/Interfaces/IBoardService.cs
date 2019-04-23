using System.Collections.Generic;
using Tearthstone.Service.Models.Base;
using TearthStone.Service.Models;

namespace TearthStone.Service.Interfaces
{
    public interface IBoardService
    {
        void PreparePlayer(PlayerModel player);
        void DrawCard(GameModel gameModel);
    }
}
