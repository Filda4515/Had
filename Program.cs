using System.Diagnostics;
using System.Xml.Linq;

namespace Had
{
    class Program
    {
        static void Main()
        {
            var gamespeed = 500;
            var score = 5;
            var gameover = false;

            Random rand = new();

            Console.WriteLine("Controls: Arrows or WASD");
            Console.WriteLine("Choose difficulty (Easy|Medium|Hard):");
            var difficulty = Console.ReadLine();
            Console.Clear();
            switch (difficulty?.ToLower())
            {
                case "e":
                case "easy":
                    Console.SetWindowSize(64, 32);
                    break;
                case "m":
                case "medium":
                    Console.SetWindowSize(32, 16);
                    break;
                case "h":
                case "hard":
                default:
                    Console.SetWindowSize(32, 16);
                    gamespeed = 250;
                    break;
            }

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
            var body = new List<Pixel>();

            Draw.Border();

            while (!gameover)
            {
                snake.DrawSnake();
                foreach (var berry in berries)
                {
                    Draw.Pixel(berry.Position);
                }
                Draw.Score(score);
                var stopwatch = Stopwatch.StartNew();
                Direction lastMovement = currentMovement;
                while (stopwatch.ElapsedMilliseconds <= gamespeed)
                {
                    currentMovement = ReadMovement(lastMovement) ?? currentMovement;
                }

                snake.Move(currentMovement);

                if (snake.Body.Count > score)
                {
                    Draw.Pixel(snake.Body[0], ' ');
                    body.RemoveAt(0);
                }

                if (snake.CheckWallCollision() || snake.CheckSelfCollision())
                {
                    gameover = true;
                }

                foreach (var berry in berries)
                {
                    if (berry.IsCollidingWith(snake.Head))
                    {
                        berry.ApplyEffect(ref score, ref gamespeed, ref gameover, rand);
                    }
                }
            }

            Draw.EndScreen(score);

            Console.ReadKey();
            Console.Clear();
            Environment.Exit(0);
        }

        static Direction? ReadMovement(Direction movement)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if ((key == ConsoleKey.UpArrow || key == ConsoleKey.W) && movement != Direction.Down)
                {
                    return Direction.Up;
                }
                else if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && movement != Direction.Up)
                {
                    return Direction.Down;
                }
                else if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.A) && movement != Direction.Right)
                {
                    return Direction.Left;
                }
                else if ((key == ConsoleKey.RightArrow || key == ConsoleKey.D) && movement != Direction.Left)
                {
                    return Direction.Right;
                }
            }
            return null;
        }
    }
}