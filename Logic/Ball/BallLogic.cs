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
            if (ball.X <= 0)
            {
                ball.X = 0;
                ball.VelocityX = -ball.VelocityX;
            }
            else if (ball.X + ball.Radius >= width)
            {
                ball.X = width - ball.Radius;
                ball.VelocityX = -ball.VelocityX;
            }

            if (ball.Y <= 0)
            {
                ball.Y = 0;
                ball.VelocityY = -ball.VelocityY;
            }
            else if (ball.Y + ball.Radius >= height)
            {
                ball.Y = height - ball.Radius;
                ball.VelocityY = -ball.VelocityY;
            }
        }
    }

}
