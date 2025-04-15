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
    Random random = new Random();
    private readonly IBoardModel _boardModel;
    public double Width { get; set; }
    public double Height { get; set; }

    private readonly IDispatcher _dispatcher;
    private Timer _timer;
    public ObservableCollection<BallModel> Balls { get; set; }
    
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
        Width = width;
        Height = height;

        // Komendy
        ApplyBallsCommand = new RelayCommand(ApplyBalls);
       

        // Timer
        _timer = new Timer(14); // 60 FPS (16 ms)
        _timer.Elapsed += OnTimerElapsed;
    }

    private void ApplyBalls()
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
    //private void StartMovingBalls()
    //{


    //    Console.WriteLine("Rozpoczęcie ruchu kulek.");
    //    _timer.Start();
    //}


    private void AddBall()
    {
        
        _boardModel.AddBall(random.NextDouble()*400+40, random.NextDouble() * 70+40, 70, 80, 80);
        OnPropertyChanged(nameof(Balls));
    }

    //private void RemoveBall()
    //{
    //    _boardModel.RemoveBall();
    //    OnPropertyChanged(nameof(Balls)); // Powiadomienie o zmianach w kolekcji Balls
    //    LogBallCollection();  // Logowanie stanu kolekcji po każdej zmianie

    //}

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
    // Metoda do logowania stanu kolekcji kulek
 
}
