namespace Logic
{
    using Data;
    public class BallLogic : IBallLogic
    {
        public IBall CreateBall(double x, double y, double radius, double velocityX, double velocityY, double mass)
            => new Ball(x, y, radius, velocityX, velocityY, mass);

        public void Move(IBall ball, double timeToMove)
        {
            ball.X += ball.VelocityX * timeToMove;
            ball.Y += ball.VelocityY * timeToMove;
        }

        public void Bounce(IBall ball, double width, double height)
        {
            double radius = ball.Radius / 2;
            if (ball.X - radius <= 0 && ball.VelocityX < 0 || ball.X + radius >= width && ball.VelocityX > 0)
                ball.VelocityX = -ball.VelocityX;
            if (ball.Y - radius <= 0 && ball.VelocityY < 0 || ball.Y + radius >= height && ball.VelocityY > 0)
                ball.VelocityY = -ball.VelocityY;
        }

        public void BounceBeetwenBalls(IBall ball1, IBall ball2)
        {
            double dx = ball2.X - ball1.X;
            double dy = ball2.Y - ball1.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            double minDist = (ball1.Radius + ball2.Radius) / 2;
            if (distance <= 0 || distance >= minDist) return;

            double overlap = minDist - distance;
            double nx = dx / distance;
            double ny = dy / distance;

            ball1.X -= nx * overlap / 2;
            ball1.Y -= ny * overlap / 2;
            ball2.X += nx * overlap / 2;
            ball2.Y += ny * overlap / 2;

            double m1 = ball1.Mass, m2 = ball2.Mass;
            double v1x = ball1.VelocityX, v2x = ball2.VelocityX;
            double v1y = ball1.VelocityY, v2y = ball2.VelocityY;

            ball1.VelocityX = (v1x * (m1 - m2) + 2 * m2 * v2x) / (m1 + m2);
            ball2.VelocityX = (v2x * (m2 - m1) + 2 * m1 * v1x) / (m1 + m2);
            ball1.VelocityY = (v1y * (m1 - m2) + 2 * m2 * v2y) / (m1 + m2);
            ball2.VelocityY = (v2y * (m2 - m1) + 2 * m1 * v1y) / (m1 + m2);
        }
    }
}
