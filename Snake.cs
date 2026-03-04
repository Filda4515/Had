namespace Had
{
    public class Snake
    {
        public Pixel Head { get; private set; }
        public List<Pixel> Body { get; private set; }

        public Snake()
        {
            Head = new Pixel(Console.WindowWidth / 2, Console.WindowHeight / 2, ConsoleColor.Red);
            Body = [];
        }

        public void Move(Direction direction)
        {
            Body.Add(new Pixel(Head.XPos, Head.YPos, ConsoleColor.Green));

            Pixel newHead = Head;
            switch (direction)
            {
                case Direction.Up:
                    newHead.YPos--;
                    break;
                case Direction.Down:
                    newHead.YPos++;
                    break;
                case Direction.Left:
                    newHead.XPos--;
                    break;
                case Direction.Right:
                    newHead.XPos++;
                    break;
            }
            Head = newHead;
        }

        public bool CheckWallCollision()
        {
            return Head.XPos == Console.WindowWidth - 1 || Head.XPos == 0 ||
                   Head.YPos == Console.WindowHeight - 1 || Head.YPos == 0;
        }

        public bool CheckSelfCollision()
        {
            foreach (var part in Body)
            {
                if (part.IsCollidingWith(Head)) return true;
            }
            return false;
        }

        public void DrawSnake()
        {
            Draw.Pixel(Head);
            foreach (var part in Body)
            {
                Draw.Pixel(part);
            }
        }
    }
}
