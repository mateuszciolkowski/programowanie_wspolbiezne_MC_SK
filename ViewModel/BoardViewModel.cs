using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;
using System;
using System.Diagnostics;
using ViewModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Media.Media3D;
using System.Windows;

public class BoardViewModel : INotifyPropertyChanged
{
    Random random = new Random();
    private readonly IBoardModel _boardModel;
    private double _boardWidth;
    public double BoardWidth
    {
        get => _boardWidth;
        set
        {
            if (_boardWidth != value)
            {
                _boardWidth = value;
                OnPropertyChanged();
                _boardModel.ResizeBoard(_boardWidth, BoardHeight);
            }
        }
    }
    private double _boardHeight;
    public double BoardHeight
    {
        get => _boardHeight;
        set
        {
            if (_boardHeight != value)
            {
                _boardHeight = value;
                OnPropertyChanged();
                _boardModel.ResizeBoard(BoardWidth, _boardHeight);
            }
        }
    }


    private readonly IDispatcher _dispatcher;
    private Timer _timer;
    public ObservableCollection<BallModel> Balls { get; set; }

    public ICommand ResizeCommand => new RelayCommand(OnResize);

    private void OnResize(object parameter)
    {
        if (parameter is SizeChangedEventArgs args)
        {
            if (args.Source is FrameworkElement element)
            {
                _boardWidth = element.ActualWidth;
                _boardHeight = element.ActualHeight;

                _boardModel.ResizeBoard(_boardWidth, _boardHeight); // <- jeśli chcesz, by model znał nowy rozmiar
                OnPropertyChanged(nameof(_boardWidth));
                OnPropertyChanged(nameof(_boardHeight));
                Console.WriteLine($"Zmieniono rozmiar: {_boardWidth} x {_boardHeight}");
            }
        }
    }

    public ICommand ApplyBallsCommand { get; }
    private string _ballCountInput;
    public string BallCountInput
    {
        get => _ballCountInput;
        set
        {
            if(_ballCountInput != value)
            {
                _ballCountInput = value;
                OnPropertyChanged();
            }
        }
    }

    public BoardViewModel(IDispatcher dispatcher, double width, double height)
    {
        _boardModel = new BoardModel(width, height);
        _dispatcher = dispatcher;
        Balls = _boardModel.Balls;  // Pobranie kolekcji piłek
        BoardWidth = width;
        BoardHeight = height;

        // Komendy
        ApplyBallsCommand = new RelayCommand(ApplyBalls);
       

        // Timer
        _timer = new Timer(14); // 60 FPS (16 ms)
        _timer.Elapsed += OnTimerElapsed;
    }

    public void UpdateBoardSize()
    {
        _boardModel.ResizeBoard(BoardWidth, BoardHeight);
        Console.WriteLine($"Width: {BoardWidth}, Height: {BoardHeight}");
    }

    private void ApplyBalls(object _)
    {

        _timer.Start();
        if (int.TryParse(BallCountInput, out int count))
        {
            ClearBalls();
            for (int i = 0; i < count; i++)
            {
                AddBall(); 
            }
        }
    }

    private void AddBall()
    {
        
        _boardModel.AddBall(random.NextDouble()*400+40, random.NextDouble() * 70+40, 70, 80, 80);
        OnPropertyChanged(nameof(Balls));
    }

    private void ClearBalls()
    {
        _boardModel.ClearBalls();
        OnPropertyChanged(nameof(Balls)); // Powiadomienie o zmianach w kolekcji Balls
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _boardModel.MoveTheBalls(0.016);

        // Zaktualizuj widok tylko raz na każdą klatkę
        _dispatcher.Invoke(new Action(() =>
        {
            // Wprowadź opóźnienie między aktualizacjami, np. co 2 klatki
            if (e.SignalTime.Second % 2 == 0) // Może być lepiej dostosowane
            {
                OnPropertyChanged(nameof(Balls));
            }

        }));
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string property = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

 

}
