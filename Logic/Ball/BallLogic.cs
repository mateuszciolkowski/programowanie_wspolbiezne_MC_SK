namespace Logic
{
    using System.Diagnostics;
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

        public void BounceBetweenBalls(IBall ball1, IBall ball2)
        {
            double dx = ball2.X - ball1.X;
            double dy = ball2.Y - ball1.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            double minDist = ball1.Radius + ball2.Radius;

            // Kolizja tylko gdy odległość < suma promieni
            if (distance == 0 || distance >= minDist)
                return;

            // Normalizacja wektora (n)
            double nx = dx / distance;
            double ny = dy / distance;

            // Wektory prędkości
            double v1x = ball1.VelocityX;
            double v1y = ball1.VelocityY;
            double v2x = ball2.VelocityX;
            double v2y = ball2.VelocityY;

            // Skalarne rzutowanie prędkości na normalny wektor (n)
            double v1n = v1x * nx + v1y * ny;
            double v2n = v2x * nx + v2y * ny;

            // Jeśli kulki się oddalają, nie odbijamy
            if (v1n - v2n <= 0)
                return;

            double m1 = ball1.Mass;
            double m2 = ball2.Mass;

            // Nowe prędkości wzdłuż normalnej (1D elastic collision)
            double v1nAfter = (v1n * (m1 - m2) + 2 * m2 * v2n) / (m1 + m2);
            double v2nAfter = (v2n * (m2 - m1) + 2 * m1 * v1n) / (m1 + m2);

            // Zmiana prędkości tylko wzdłuż normalnej
            double dv1n = v1nAfter - v1n;
            double dv2n = v2nAfter - v2n;

            ball1.VelocityX += dv1n * nx;
            ball1.VelocityY += dv1n * ny;
            ball2.VelocityX += dv2n * nx;
            ball2.VelocityY += dv2n * ny;

            // Korekta pozycji żeby uniknąć nakładania się kul
            double overlap = minDist - distance;
            double correction = overlap / 2;

            ball1.X -= correction * nx;
            ball1.Y -= correction * ny;
            ball2.X += correction * nx;
            ball2.Y += correction * ny;
        }


    }
}
