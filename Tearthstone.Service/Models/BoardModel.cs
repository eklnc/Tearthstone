using System.Collections.Generic;
using Tearthstone.Service.Models.Base;

namespace TearthStone.Service.Models
{
    public class BoardModel
    {
        public List<CardBase> PlayingPlayerRemaining { get; set; }
        public List<CardBase> WaitingPlayerRemaining { get; set; }
    }
}
