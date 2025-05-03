using Logic;
using Model;
using System.Collections.ObjectModel;

public class BoardModel : IBoardModel
{
    public double Width { get; set; }
    public double Height { get; set; }
    private BoardLogic _boardLogic;
    public ObservableCollection<BallModel> Balls { get; set; }

    // Event do powiadomienia o zmianie w piłkach
    public event Action BallsMovedEvent;

    public BoardModel(double width, double height)
    {
        Width = width;
        Height = height;
        _boardLogic = new BoardLogic(width, height);
        Balls = new ObservableCollection<BallModel>();

        // Subskrypcja na event powiadamiający o zmianach w piłkach
        _boardLogic.BallsMoved += OnBallsMoved;
    }

    public void ResizeBoard(double width, double height)
    {
        Width = width;
        Height = height;
        _boardLogic.ResizeBoard(width, height);
    }

    public void AddBall(double x, double y, double radius, double velocityX, double velocityY, double mass)
    {
        var ball = new BallModel(x, y, radius);

        Balls.Add(ball);

        _boardLogic.AddBall(x, y, radius, velocityX, velocityY, mass);
    }

    public void RemoveBall()
    {
        _boardLogic.RemoveBall();
    }

    public void ClearBalls()
    {
        _boardLogic.ClearBalls();
        Balls.Clear();
    }

    // Funkcja wywoływana, gdy piłki zostały poruszone
    private void OnBallsMoved()
    {
        var balls = _boardLogic.GetBalls();

        // Zaktualizuj pozycje piłek w ObservableCollection
        int i = 0;
        foreach (var ball in balls)
        {
            Balls[i].X = balls[i].X;  // Zaktualizuj X
            Balls[i].Y = balls[i].Y;  // Zaktualizuj Y
            i++;
        }
    }
}
