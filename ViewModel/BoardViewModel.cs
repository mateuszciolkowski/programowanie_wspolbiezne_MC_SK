using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;
using ViewModel;
using System.Windows;

public class BoardViewModel : INotifyPropertyChanged
{
    Random random = new Random();
    private readonly IBoardModel _boardModel;
    private readonly IDispatcher _dispatcher;

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

    public ObservableCollection<BallModel> Balls { get; set; }

    public ICommand ResizeCommand { get; }
    public ICommand ApplyBallsCommand { get; }

    private string _ballCountInput;
    public string BallCountInput
    {
        get => _ballCountInput;
        set
        {
            if (_ballCountInput != value)
            {
                _ballCountInput = value;
                OnPropertyChanged();
            }
        }
    }

    public BoardViewModel(IDispatcher dispatcher)
    {
        _boardModel = new BoardModel(800, 600);
        _dispatcher = dispatcher;
        Balls = _boardModel.Balls;

        // Subskrypcja na event informujący o zmianach w piłkach
        _boardModel.BallsMovedEvent += OnBallsMoved;

        // Inicjalizacja komend
        ResizeCommand = new RelayCommand(OnResize);
        ApplyBallsCommand = new RelayCommand(ApplyBalls);
    }

    private void OnResize(object parameter)
    {
        if (parameter is SizeChangedEventArgs args)
        {
            if (args.Source is FrameworkElement element)
            {
                _boardWidth = element.ActualWidth;
                _boardHeight = element.ActualHeight;
                _boardModel.ResizeBoard(_boardWidth, _boardHeight);
                OnPropertyChanged(nameof(BoardWidth));
                OnPropertyChanged(nameof(BoardHeight));
            }
        }
    }

    private void ApplyBalls(object _)
    {
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
        double x = 100 + random.NextDouble() * 500;
        double y = 100 + random.NextDouble() * 500;
        double radius = 50 + random.NextDouble() * 50; 
        double velocityX = random.NextDouble() * 250 - 50;
        double velocityY = random.NextDouble() * 250 - 50;
        double mass = radius * 3;

        _boardModel.AddBall(x, y, radius, velocityX, velocityY, mass);
    }

    private void ClearBalls()
    {
        _boardModel.ClearBalls();
    }

    // Funkcja, która zostanie wywołana, gdy piłki się zmienią
    private void OnBallsMoved()
    {
        _dispatcher.Invoke(() =>
        {
            // Po zakończeniu ruchu piłek, odświeżamy kolekcję w ViewModel
            OnPropertyChanged(nameof(Balls));
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string property = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
