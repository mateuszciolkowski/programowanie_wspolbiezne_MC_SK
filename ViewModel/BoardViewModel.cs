using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;
using System;
using System.Diagnostics;
using ViewModel;

public class BoardViewModel : INotifyPropertyChanged
{
    private readonly IBoardModel _boardModel;
    private readonly IDispatcher _dispatcher;
    private Timer _timer;

    public double Width { get; set; }
    public double Height { get; set; }

    public ObservableCollection<BallModel> Balls { get; set; }
    public ICommand StartMovingBallsCommand { get; }
    public ICommand AddBallCommand { get; }
    public ICommand RemoveBallCommand { get; }
    public ICommand ClearBallsCommand { get; }
    public ICommand StopMovingBallsCommand { get; }

    public BoardViewModel(IDispatcher dispatcher, double width, double height)
    {
        _boardModel = new BoardModel(width, height);
        _dispatcher = dispatcher;
        Balls = _boardModel.Balls;  // Pobranie kolekcji piłek
        Width = width;
        Height = height;

        // Komendy
        StartMovingBallsCommand = new RelayCommand(StartMovingBalls);
        AddBallCommand = new RelayCommand(AddBall);
        RemoveBallCommand = new RelayCommand(RemoveBall);
        ClearBallsCommand = new RelayCommand(ClearBalls);
        StopMovingBallsCommand = new RelayCommand(StopMovingBalls);

        // Timer
        _timer = new Timer(16); // 60 FPS (16 ms)
        _timer.Elapsed += OnTimerElapsed;
    }

    private void StartMovingBalls()
    {
 

        Console.WriteLine("Rozpoczęcie ruchu kulek.");
        _timer.Start();
    }

    private void StopMovingBalls()
    {
        Console.WriteLine("Zakończenie ruchu kulek.");
        _timer.Stop();
    }

    private void AddBall()
    {
        _boardModel.AddBall(50, 50, 20, 100, 80);
        OnPropertyChanged(nameof(Balls));
        LogBallCollection();  // Logowanie stanu kolekcji po każdej zmianie
    }

    private void RemoveBall()
    {
        _boardModel.RemoveBall();
        OnPropertyChanged(nameof(Balls)); // Powiadomienie o zmianach w kolekcji Balls
        LogBallCollection();  // Logowanie stanu kolekcji po każdej zmianie

    }

    private void ClearBalls()
    {
        _boardModel.ClearBalls();
        OnPropertyChanged(nameof(Balls)); // Powiadomienie o zmianach w kolekcji Balls
        LogBallCollection();  // Logowanie stanu kolekcji po każdej zmianie

    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _boardModel.MoveTheBalls(0.016);
        _dispatcher.Invoke(new Action(() =>
        {
            OnPropertyChanged(nameof(Balls));  // Zaktualizowanie widoku po każdej zmianie piłek
            LogBallCollection();  // Logowanie stanu kolekcji po każdej zmianie

        }));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string property = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
    // Metoda do logowania stanu kolekcji kulek
    private void LogBallCollection()
    {
        Console.WriteLine("Aktualny stan kolekcji kulek:");
        foreach (var ball in Balls)
        {
            Debug.WriteLine($"Kulka: X={ball.X}, Y={ball.Y}, Radius={ball.Radius}, VelocityX={ball.VelocityX}, VelocityY={ball.VelocityY}");
        }
    }
}
