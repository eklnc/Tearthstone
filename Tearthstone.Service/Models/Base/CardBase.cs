using TearthStone.Service.Models;

namespace Tearthstone.Service.Models.Base
{
    public abstract class CardBase
    {
        public abstract int Id { get; }
        public abstract string Title { get; }
        public abstract string Body { get; }
        public abstract int ManaCost { get; set; }
        public abstract string CardAction(GameModel gameModel);
    }
}
