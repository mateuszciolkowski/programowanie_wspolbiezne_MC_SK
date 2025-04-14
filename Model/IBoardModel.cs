using System.Collections.Generic;

namespace Model
{
    public interface IBoardModel
    {
        double Width { get; set; }
        double Height { get; set; }
        List<BallModel> Balls { get; set; }

        void ResizeBoard(double width, double height);
        void AddBall(double x, double y, double radius, double velocityX, double velocityY);
        void RemoveBall();
        void MoveTheBalls(double timeToMove);
    }
}
