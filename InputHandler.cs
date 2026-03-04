using System.Diagnostics;

namespace Had
{
    public static class InputHandler
    {
        public static Direction GetNextDirection(int gameSpeed, Direction currentDirection)
        {
            var stopwatch = Stopwatch.StartNew();
            Direction nextDirection = currentDirection;

            while (stopwatch.ElapsedMilliseconds <= gameSpeed)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if ((key == ConsoleKey.UpArrow || key == ConsoleKey.W) && currentDirection != Direction.Down)
                    {
                        nextDirection = Direction.Up;
                    }
                    else if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && currentDirection != Direction.Up)
                    {
                        nextDirection = Direction.Down;
                    }
                    else if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.A) && currentDirection != Direction.Right)
                    {
                        nextDirection = Direction.Left;
                    }
                    else if ((key == ConsoleKey.RightArrow || key == ConsoleKey.D) && currentDirection != Direction.Left)
                    {
                        nextDirection = Direction.Right;
                    }
                }
            }

            return nextDirection;
        }
    }
}
