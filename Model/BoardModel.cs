using Logic;
using Model;
using System.Collections.ObjectModel;

public class BoardModel : IBoardModel
{
    public double Width { get; private set; }
    public double Height { get; private set; }

    private readonly BoardLogic _boardLogic;
    public ObservableCollection<BallModel> Balls { get; set; }
    public event Action BallsMovedEvent;

    private int _tickCounter;
    private const int TicksPerUpdate = 2; // ~30 FPS

    public BoardModel(double width, double height)
    {
        Width = width;
        Height = height;
        _boardLogic = new BoardLogic(width, height);
        Balls = new ObservableCollection<BallModel>();
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
        if (Balls.Count > 0)
            Balls.RemoveAt(Balls.Count - 1);
        _boardLogic.RemoveBall();
    }
    
    public void ClearBalls()
    {
        Balls.Clear();
        _boardLogic.ClearBalls();
        _tickCounter = 0;
    }

    private void OnBallsMoved()
    {
        if (++_tickCounter % TicksPerUpdate != 0)
            return;

        var snapshot = _boardLogic.GetBalls();
        int count = Math.Min(snapshot.Count, Balls.Count);
        for (int i = 0; i < count; i++)
        {
            Balls[i].UpdatePosition(snapshot[i].X, snapshot[i].Y);
        }
        BallsMovedEvent?.Invoke();
    }
}

