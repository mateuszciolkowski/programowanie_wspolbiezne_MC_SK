using System.Collections.Generic;
using Data;

namespace Logic.BoardLogicNamespace
{
    public interface IBoardLogic
    {
        double Width { get; }
        double Height { get; }
        IReadOnlyList<IBall> Balls { get; }

        void ResizeBoard(double width, double height);
        void AddBall(double x, double y, double radius, double velocityX, double velocityY);
        void RemoveBall();
        void MoveTheBalls(double timeToMove);
    }
}