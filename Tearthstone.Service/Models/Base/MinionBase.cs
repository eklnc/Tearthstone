namespace Tearthstone.Service.Models.Base
{
    public abstract class MinionBase : CardBase
    {
        public abstract int Health { get; set; }
        public abstract int Attack { get; set; }
    }
}
