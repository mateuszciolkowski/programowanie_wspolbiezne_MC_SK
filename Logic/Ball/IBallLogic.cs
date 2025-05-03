using Data;

namespace Logic
{
    public interface IBallLogic
    {
        IBall CreateBall(double x, double y, double radius, double velocityX, double velocityY, double mass);
        void Move(IBall ball, double timeToMove);
        void Bounce(IBall ball, double width, double height);
        Task BounceBeetwenBalls(IBall ball1, IBall ball2);

    }
}