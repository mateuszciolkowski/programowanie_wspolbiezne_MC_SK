using Data;

namespace Logic.BallLogicNamespace
{
    public interface IBallLogic
    {
        IBall CreateBall(double x, double y, double radius, double velocityX, double velocityY);
        void Move(IBall ball, double timeToMove);
        void Bounce(IBall ball, double width, double height);
    }
}