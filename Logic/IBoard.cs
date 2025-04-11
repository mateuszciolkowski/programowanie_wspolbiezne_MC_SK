using System.Collections.Generic;
using Data;

namespace Logic
{
    public interface IBoard
    {
        double Width { get; }
        double Height { get; }
        IReadOnlyList<IBall> Balls { get; }

        void AddBall(double x, double y, double radius, double velocityX, double velocityY);
        void MoveTheBalls(double timeToMove);
    }
}