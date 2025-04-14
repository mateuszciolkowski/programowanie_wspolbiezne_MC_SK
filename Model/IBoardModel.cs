using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model
{
    public interface IBoardModel
    {
        double Width { get; set; }
        double Height { get; set; }
        //List<BallModel> Balls { get; set; }
        public ObservableCollection<BallModel> Balls { get; set; }


        public void ResizeBoard(double width, double height);
        public void AddBall(double x, double y, double radius, double velocityX, double velocityY);
        public void RemoveBall();
        public void MoveTheBalls(double timeToMove);
    }
}
