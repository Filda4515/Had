namespace Had
{
    class Program
    {
        static void Main()
        {
            int score = 5;
            int gameSpeed = 500;
            bool gameOver = false;
            GameState state = new(score, gameSpeed, gameOver);
            Random rand = new();

            Draw.IntroScreen(state);

            Snake snake = new();
            List<Berry> berries =
            [
                new NormalBerry(),
                new SpeedBerry(),
                new PoisonBerry()
            ];
            foreach (var berry in berries)
            {
                berry.Generate(rand);
            }

            var currentMovement = Direction.Right;

            Draw.Border();
            foreach (var berry in berries)
            {
                Draw.Pixel(berry.Position);
            }

            while (!state.GameOver)
            {   
                Draw.Score(state.Score);

                currentMovement = InputHandler.GetNextDirection(state.GameSpeed, currentMovement);

                snake.Move(currentMovement, state);

                if (snake.CheckWallCollision() || snake.CheckSelfCollision())
                {
                    state.GameOver = true;
                }

                foreach (var berry in berries)
                {
                    if (berry.IsCollidingWith(snake.Head))
                    {
                        berry.ApplyEffect(state, rand);
                    }
                }
            }

            Draw.EndScreen(state.Score);

            Console.ReadKey();
            Console.Clear();
            Environment.Exit(0);
        }
    }
}