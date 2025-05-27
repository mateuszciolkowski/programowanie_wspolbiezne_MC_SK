namespace Data
{
    public class Ball : IBall
    {
        public Ball(double x, double y, double radius, double velocityX, double velocityY, double mass)
        {

            X = x - radius;
            Y = y - radius;
            Radius = radius;
            VelocityX = velocityX;
            VelocityY = velocityY;
            Mass = mass;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public double VelocityX { get;  private set; }
        public double VelocityY { get;  private set; }
        public double Mass { get; set; }

        private readonly object _ballLock = new object();


        public void Move(double timeToMove)
        {
            this.X += this.VelocityX * timeToMove;
            this.Y += this.VelocityY * timeToMove;

        }
        public void SetVelocity(double vx, double vy)
        {
            lock (_ballLock)
            {
                VelocityX = vx;
                VelocityY = vy;
            }
        }

    }
}