using Data;

namespace Logic
{
    public class BallLogic : IBallLogic
    {
        public IBall CreateBall(double x, double y, double radius, double velocityX, double velocityY) => new Ball(x, y, radius, velocityX, velocityY);

        public void Move(IBall ball, double timeToMove)
        {


            ball.X += ball.VelocityX * timeToMove;
            ball.Y += ball.VelocityY * timeToMove;
        }

        public void Bounce(IBall ball, double width, double height)
        {
            if (ball.X - ball.Radius <= 0 || ball.X + ball.Radius >= width) ball.VelocityX = -ball.VelocityX;
            if (ball.Y - ball.Radius <= 0 || ball.Y + ball.Radius >= height) ball.VelocityY = -ball.VelocityY;
        }
    }


}