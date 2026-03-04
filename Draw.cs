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

        public static void Border()
        {
            Console.ForegroundColor = ConsoleColor.White;
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

        public static void Score(int score)
        {
            Console.Title = $"Score: {score}";
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