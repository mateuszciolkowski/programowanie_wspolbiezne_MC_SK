using System.Collections.ObjectModel;
using Logic;
namespace Model
{
    public class BoardModel: IBoardModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        //public List<BallModel> Balls { get; set; }
        public readonly IBoardLogic boardLogic;
        public ObservableCollection<BallModel> Balls { get; set; }

         public BoardModel(double width, double height)
        {
            Width = width;
            Height = height;
            //Balls = new List<BallModel>();
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

        public void ClearBalls()
        {
            Balls.Clear();
            boardLogic.ClearBalls();
        }

        public void RemoveBall()
        {
            boardLogic.RemoveBall();
            if (Balls.Count > 0)
            {
                Balls.RemoveAt(Balls.Count - 1);
            }
        }

        public void MoveTheBalls(double timeToMove)
        {
            boardLogic.MoveTheBalls(timeToMove);
        }
    }
}
