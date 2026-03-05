namespace Had
{
    public abstract class Berry(ConsoleColor color)
    {
        public Pixel Position { get; protected set; }
        protected ConsoleColor Color = color;

        public void Generate(Random rand)
        {
            Position = new Pixel(
                rand.Next(1, Console.WindowWidth - 2),
                rand.Next(1, Console.WindowHeight - 2),
                Color
            );
        }

        public bool IsCollidingWith(Pixel other) => Position.IsCollidingWith(other);

        public abstract void ApplyEffect(GameState state, Random rand);
    }

    public class NormalBerry : Berry
    {
        public NormalBerry() : base(ConsoleColor.Cyan) { }

        public override void ApplyEffect(GameState state, Random rand)
        {
            state.Score++;
            Generate(rand);
            Draw.Pixel(Position);
        }
    }

    public class SpeedBerry : Berry
    {
        public SpeedBerry() : base(ConsoleColor.White) { }

        public override void ApplyEffect(GameState state, Random rand)
        {
            if (state.GameSpeed > 150) state.GameSpeed -= 25;
            Generate(rand);
            Draw.Pixel(Position);
        }
    }

    public class PoisonBerry : Berry
    {
        public PoisonBerry() : base(ConsoleColor.Magenta) { }

        public override void ApplyEffect(GameState state, Random rand)
        {
            state.GameOver = true;
        }
    }
}