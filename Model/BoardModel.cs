using Logic;
using Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public class BoardModel : IBoardModel
{
    public double Width { get; private set; }
    public double Height { get; private set; }

    private readonly BoardLogic _boardLogic;
    public ObservableCollection<BallModel> Balls { get; set; }
    public event Action BallsMovedEvent;

    private int _tickCounter;
    private const int TicksPerUpdate = 5;

    public BoardModel(double width, double height)
    {
        Width = width;
        Height = height;
        var ballLogic = new BallLogic();
        _boardLogic = new BoardLogic(width, height,ballLogic);
        Balls = new ObservableCollection<BallModel>();
        _boardLogic.BallsMoved += async () => await OnBallsMovedAsync();
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

    private async Task OnBallsMovedAsync()
    {
        // Zwiększamy licznik ticków
        if (++_tickCounter % TicksPerUpdate != 0)
            return;

        // Zrób kopię stanu kul w logice
        var snapshot = await _boardLogic.GetBallsAsync(); // Załóżmy, że metoda GetBallsAsync() jest asynchroniczna
        int count = Math.Min(snapshot.Count, Balls.Count);

        // Uaktualnij pozycje kulek
        for (int i = 0; i < count; i++)
        {
            Balls[i].UpdatePosition(snapshot[i].X, snapshot[i].Y);
        }

        // Powiadom interfejs o zmianach
        BallsMovedEvent?.Invoke();
    }
}
