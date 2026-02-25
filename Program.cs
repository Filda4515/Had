using System.Diagnostics;

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

            var head = new Pixel(Console.WindowWidth / 2, Console.WindowHeight / 2, ConsoleColor.Red);
            var berry = GenerateBerry(rand, ConsoleColor.Cyan);
            var speedBerry = GenerateBerry(rand, ConsoleColor.White);
            var poisonBerry = GenerateBerry(rand, ConsoleColor.Magenta);

            var currentMovement = Direction.Right;
            var body = new List<Pixel>();

            DrawBorder();

            while (!gameover)
            {
                DrawPixel(head);
                DrawPixel(berry);
                DrawPixel(poisonBerry);
                DrawPixel(speedBerry);
                DrawScore(score);
                var stopwatch = Stopwatch.StartNew();
                Direction lastMovement = currentMovement;
                while (stopwatch.ElapsedMilliseconds <= gamespeed)
                {
                    currentMovement = ReadMovement(lastMovement)??currentMovement;
                }

                body.Add(new Pixel(head.XPos, head.YPos, ConsoleColor.Green));

                switch (currentMovement)
                {
                    case Direction.Up:
                        head.YPos--;
                        break;
                    case Direction.Down:
                        head.YPos++;
                        break;
                    case Direction.Left:
                        head.XPos--;
                        break;
                    case Direction.Right:
                        head.XPos++;
                        break;
                }

                if (head.XPos == Console.WindowWidth - 1 || head.XPos == 0 || head.YPos == Console.WindowHeight - 1 || head.YPos == 0)
                    gameover = true;

                for (int i = 0; i < body.Count; i++)
                {
                    DrawPixel(body[i]);
                    if (body[i].XPos == head.XPos && body[i].YPos == head.YPos)
                        gameover = true;
                }

                if (BerryCollision(berry, head))
                {
                    score++;
                    berry = GenerateBerry(rand, ConsoleColor.Cyan);
                }
                if (BerryCollision(speedBerry, head))
                {
                    if (gamespeed>150) gamespeed -= 25;
                    speedBerry = GenerateBerry(rand, ConsoleColor.White);
                }
                if (BerryCollision(poisonBerry, head))
                {
                    gameover = true;
                }

                if (body.Count > score)
                {
                    var tail = body[0];
                    Console.SetCursorPosition(tail.XPos, tail.YPos);
                    Console.Write(" ");
                    body.RemoveAt(0);
                }
            }

            PrintEndScreen(score);

            Console.ReadKey();
            Console.Clear();
            Environment.Exit(0);
        }

        static private bool BerryCollision(Pixel berry, Pixel head) => berry.XPos == head.XPos && berry.YPos == head.YPos;

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

        static void DrawPixel(Pixel pixel)
        {
            Console.SetCursorPosition(pixel.XPos, pixel.YPos);
            Console.ForegroundColor = pixel.ScreenColor;
            Console.Write("■");
            Console.SetCursorPosition(0, 0);
        }

        static void DrawBorder()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");

                Console.SetCursorPosition(i, Console.WindowHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");

                Console.SetCursorPosition(Console.WindowWidth - 1, i);
                Console.Write("■");
            }
        }

        static void DrawScore(int score)
        {
            Console.Title = $"score: {score}";
        }

        static void PrintEndScreen(int score)
        {
            Console.SetCursorPosition(2, Console.WindowHeight / 2);
            Console.WriteLine($"Kupujte na alze, je to fajn");
            Console.SetCursorPosition(2, Console.WindowHeight / 2 + 1);
            Console.WriteLine($"Game over, Score: {score - 5}");
            Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2 + 1);
        }

        struct Pixel(int xPos, int yPos, ConsoleColor color)
        {
            public int XPos { get; set; } = xPos;
            public int YPos { get; set; } = yPos;
            public ConsoleColor ScreenColor { get; set; } = color;
        }

        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        static Pixel GenerateBerry(Random rand, ConsoleColor color)
        {
            return new Pixel(rand.Next(1, Console.WindowWidth - 2), rand.Next(1, Console.WindowHeight - 2), color);
        }
    }
}
