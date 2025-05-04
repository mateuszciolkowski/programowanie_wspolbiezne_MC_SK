using System.Collections.ObjectModel;

namespace Model
{
    public interface IBoardModel
    {
        double Width { get; }
        double Height { get; }
        ObservableCollection<BallModel> Balls { get; set; }

        void ResizeBoard(double width, double height);
        void AddBall(double x, double y, double radius, double velocityX, double velocityY, double mass);
        void RemoveBall();
        void ClearBalls();
        // Dodajemy event do interfejsu
        event Action BallsMovedEvent;
    }
}
