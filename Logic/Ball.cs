namespace Logic
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }


        public Ball(double x, double y, double radius, double SpeedX, double SpeedY)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public void Move(double timeToMove)
        {
            X += SpeedX * timeToMove;
            Y += SpeedY * timeToMove;
        }

        public void Bounce(double wallX, double wallY)
        {
            // Odbicie od osi X
            if (X <= 0 || X >= wallX)
            {
                SpeedX = -SpeedX;

                // Odbicie od osi Y
                if (Y <= 0 || Y >= wallY)
                {
                    SpeedY = -SpeedY;
                }
            }
        }
    }
}
