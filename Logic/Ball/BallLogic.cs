﻿namespace Logic
{
    using System.Diagnostics;
    using Data;
    public class BallLogic : IBallLogic
    {
        public IBall CreateBall(double x, double y, double radius, double velocityX, double velocityY, double mass)
            => new Ball(x, y, radius, velocityX, velocityY, mass);

        public void Move(IBall ball, double timeToMove)
        {
            ball.Move(timeToMove);
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
            double minDist = (ball1.Radius + ball2.Radius) / 2;

            if (distance < minDist && distance > 0.01)
            {
                // Normalizacja wektora zderzenia
                double nx = dx / distance;
                double ny = dy / distance;

                // Cofnięcie pozycji kul, żeby nie nakładały się na siebie
                double overlap = minDist - distance;
                ball1.X -= nx * overlap / 2;
                ball1.Y -= ny * overlap / 2;
                ball2.X += nx * overlap / 2;
                ball2.Y += ny * overlap / 2;

                // Składowe prędkości wzdłuż normalnej (kierunek zderzenia)
                double v1n = ball1.VelocityX * nx + ball1.VelocityY * ny;
                double v2n = ball2.VelocityX * nx + ball2.VelocityY * ny;

                // Składowe styczne (nie zmieniają się przy zderzeniu bez tarcia)
                double v1tX = ball1.VelocityX - v1n * nx;
                double v1tY = ball1.VelocityY - v1n * ny;
                double v2tX = ball2.VelocityX - v2n * nx;
                double v2tY = ball2.VelocityY - v2n * ny;

                // Nowe prędkości wzdłuż normalnej po zderzeniu (1D sprężyste)
                double m1 = ball1.Mass;
                double m2 = ball2.Mass;

                double newV1n = (v1n * (m1 - m2) + 2 * m2 * v2n) / (m1 + m2);
                double newV2n = (v2n * (m2 - m1) + 2 * m1 * v1n) / (m1 + m2);

                // Łączenie składowych z powrotem
                ball1.VelocityX = v1tX + newV1n * nx;
                ball1.VelocityY = v1tY + newV1n * ny;

                ball2.VelocityX = v2tX + newV2n * nx;
                ball2.VelocityY = v2tY + newV2n * ny;
            }
        }

    }


}
