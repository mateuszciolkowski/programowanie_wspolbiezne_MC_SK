using System.Collections.ObjectModel;
using System.Diagnostics;
using Logic;

namespace Model
{
    public class BoardModel : IBoardModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        BoardLogic boardLogic;
        public ObservableCollection<BallModel> Balls { get; set; }

        public BoardModel(double width, double height)
        {
            Width = width;
            Height = height;
            boardLogic = new BoardLogic(width, height);
            Balls = new ObservableCollection<BallModel>(); 
        }

        public void ResizeBoard(double width, double height)
        {
            Width = width;
            Height = height;
            boardLogic.ResizeBoard(width, height);
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            var ball = new BallModel(x, y, radius);

            Balls.Add(ball);

            boardLogic.AddBall(x, y, radius, velocityX, velocityY);
        }


        public void RemoveBall()
        {
            if (Balls.Count > 0)
            {
                Balls.RemoveAt(Balls.Count - 1);
            }
        }

        public void MoveTheBalls(double timeToMove)
        {
            boardLogic.MoveTheBalls(timeToMove);
            var balls = boardLogic.GetBalls();
            for (int i = 0; i < balls.Count(); i++)
            {
                Balls[i].X = balls[i].X;
                Balls[i].Y = balls[i].Y;
            }


        }


        public void ClearBalls()
        {
            boardLogic.ClearBalls();
            Balls.Clear();
        }
    }
}
