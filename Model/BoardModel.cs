using System.Collections.Generic;
using Logic;
namespace Model
{
    public class BoardModel: IBoardModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public List<BallModel> Balls { get; set; }
        public readonly IBoardLogic boardLogic;

        public BoardModel(double width, double height)
        {
            Width = width;
            Height = height;
            Balls = new List<BallModel>();
            boardLogic = new BoardLogic(width, height);
            
        }

        void IBoardModel.ResizeBoard(double width, double height)
        {
            boardLogic.ResizeBoard(width, height);
        }

        void IBoardModel.AddBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            boardLogic.AddBall(x, y, radius, velocityX, velocityY);
            Balls.Append(new BallModel(x, y, radius, velocityX, velocityY));
        }

        void IBoardModel.RemoveBall()
        {
            boardLogic.RemoveBall();
            if (Balls.Count > 0)
            {
                Balls.RemoveAt(Balls.Count - 1);
            }
        }

        void IBoardModel.MoveTheBalls(double timeToMove)
        {
            boardLogic.MoveTheBalls(timeToMove);
        }
    }
}
