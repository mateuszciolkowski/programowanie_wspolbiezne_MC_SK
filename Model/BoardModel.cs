using System.Collections.ObjectModel;
using Logic;

namespace Model
{
    public class BoardModel : IBoardModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public ObservableCollection<BallModel> Balls { get; set; }

        private readonly IBoardLogic boardLogic;

        public BoardModel(double width, double height)
        {
            Width = width;
            Height = height;
            Balls = new ObservableCollection<BallModel>();
            boardLogic = new BoardLogic(width, height);
        }

        public void ResizeBoard(double width, double height)
        {
            boardLogic.ResizeBoard(width, height);
            Width = width;
            Height = height;
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            boardLogic.AddBall(x, y, radius, velocityX, velocityY);
            Balls.Add(new BallModel(x, y, radius, velocityX, velocityY));
        }

        public void RemoveBall()
        {
            boardLogic.RemoveBall();
            if (Balls.Count > 0)
            {
                Balls.RemoveAt(Balls.Count - 1);
            }
        }

        public void ClearBalls()
        {
            Balls.Clear();
            boardLogic.ClearBalls();
        }

        public void MoveTheBalls(double timeToMove)
        {
            boardLogic.MoveTheBalls(timeToMove);
            // Optionally update the ball positions after move (if necessary for ViewModel)
        }
    }
}
