namespace Data
{
    public interface IBall
    {
        double X { get; set; }
        double Y { get; set; }
        double Radius { get; set; }
        double VelocityX { get;}
        double VelocityY { get; }
        double Mass { get; set; }

        void Move(double timeToMove);

        void SetVelocity(double vx, double vy);

    }

}