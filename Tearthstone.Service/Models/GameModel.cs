namespace TearthStone.Service.Models
{
    public class GameModel
    {
        public BoardModel Board { get; set; }
        public PlayerModel PlayingPlayer { get; set; }
        public PlayerModel WaitingPlayer { get; set; }
    }
}
