using Data;

namespace Logic
{
    public class BallLogic : IBallLogic
    {
        public IBall CreateBall(double x, double y, double radius, double velocityX, double velocityY)
                => new Ball(x, y, radius, velocityX, velocityY);

        public void Move(IBall ball, double timeToMove)
        {
            ball.X += ball.VelocityX * timeToMove;
            ball.Y += ball.VelocityY * timeToMove;
        }

        public void Bounce(IBall ball, double width, double height)
        {
            double radius = ball.Radius / 2;

            // Odbicie od lewej i prawej krawędzi
            if (ball.X - radius <= 0 && ball.VelocityX < 0)
                ball.VelocityX = -ball.VelocityX;
            else if (ball.X + radius >= width && ball.VelocityX > 0)
                ball.VelocityX = -ball.VelocityX;

            // Odbicie od góry i dołu
            if (ball.Y - radius <= 0 && ball.VelocityY < 0)
                ball.VelocityY = -ball.VelocityY;
            else if (ball.Y + radius >= height && ball.VelocityY > 0)
                ball.VelocityY = -ball.VelocityY;
        }
        public void BounceBeetwenBalls(IBall ball1, IBall ball2)
        {
            double dx = ball2.X - ball1.X;
            double dy = ball2.Y - ball1.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            double minDist = ball1.Radius / 2 + ball2.Radius / 2;

            if (distance < minDist && distance > 0.01)
            {
                // normalny wektor
                double nx = dx / distance;
                double ny = dy / distance;

                // relatywna prędkość
                double dvx = ball1.VelocityX - ball2.VelocityX;
                double dvy = ball1.VelocityY - ball2.VelocityY;

                // ile z tej prędkości idzie wzdłuż normalnej
                double impactSpeed = dvx * nx + dvy * ny;

                if (impactSpeed < 0) return; // kulki się oddalają

                // Odbicie sprężyste (dla kul o jednakowej masie)
                double impulse = 2 * impactSpeed / 2; // masa 1 dla każdej kulki

                ball1.VelocityX -= impulse * nx;
                ball1.VelocityY -= impulse * ny;

                ball2.VelocityX += impulse * nx;
                ball2.VelocityY += impulse * ny;
            }
        }
    }
}
