namespace Data
{
    public interface IBall
    {
        double X { get; set; }
        double Y { get; set; }
        double Radius { get; set; }
        double VelocityX { get; set; }
        double VelocityY { get; set; }
        double Mass { get; set; }
    }
}