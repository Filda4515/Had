using System;

namespace Had
{
    public struct Pixel(int xPos, int yPos, ConsoleColor color)
    {
        public int XPos { get; set; } = xPos;
        public int YPos { get; set; } = yPos;
        public ConsoleColor ScreenColor { get; set; } = color;

        public bool IsCollidingWith(Pixel other) => XPos == other.XPos && YPos == other.YPos;
    }
}