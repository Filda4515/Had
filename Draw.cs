namespace Had
{
    public static class Draw
    {
        public static void Pixel(Pixel pixel, char character ='■')
        {
            Console.SetCursorPosition(pixel.XPos, pixel.YPos);
            Console.ForegroundColor = pixel.ScreenColor;
            Console.Write(character);
            Console.SetCursorPosition(0, 0);
        }

        public static void Border(char character = '■')
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(character);
                Console.SetCursorPosition(i, Console.WindowHeight - 1);
                Console.Write(character);
            }

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(character);
                Console.SetCursorPosition(Console.WindowWidth - 1, i);
                Console.Write(character);
            }
        }

        public static void Score(int score)
        {
            Console.Title = $"Score: {score}";
        }

        public static void IntroScreen(GameState state)
        {
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
                    state.GameSpeed = 250;
                    break;
            }
        }

        public static void EndScreen(int score)
        {
            Console.Clear();
            Console.SetCursorPosition(2, Console.WindowHeight / 2);
            Console.WriteLine("Kupujte na alze, je to fajn");
            Console.SetCursorPosition(2, Console.WindowHeight / 2 + 1);
            Console.WriteLine($"Game over, Score: {score - 5}");
        }
    }
}