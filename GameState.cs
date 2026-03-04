namespace Had
{
    public class GameState(int score, int gameSpeed, bool gameOver)
    {
        public int Score { get; set; } = score;
        public int GameSpeed { get; set; } = gameSpeed;
        public bool GameOver { get; set; } = gameOver;
    }
}
