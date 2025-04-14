using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;
using Model;

namespace ViewModel
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        private readonly IBoardModel _boardModel;
        private readonly System.Timers.Timer _timer; // nie wiem 
        private readonly DispatcherTimer dispatcherTimer;
        private int _ballCount;
        public int BallCount
        {
            get => _ballCount;
            set
            {
                _ballCount = value;
                OnPropertyChanged(nameof(BallCount));
            }
        }

        public ObservableCollection<BallModel> Balls { get; set; }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        private bool _isRunning;

        public BoardViewModel(IBoardModel boardModel)
        {
            _boardModel = boardModel;
            Balls = new ObservableCollection<BallModel>(_boardModel.Balls);

            StartCommand = new RelayCommand(StartSimulation, () => !_isRunning);
            StopCommand = new RelayCommand(StopSimulation, () => _isRunning);

            // Użycie DispatcherTimer, który działa na wątku UI
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(20)
            };
            _timer.Tick += OnTick;
        }

        private void StartSimulation()
        {
            _boardModel.ResizeBoard(800, 600); // Przykładowe wymiary

            var rnd = new Random();
            for (int i = 0; i < BallCount; i++)
            {
                double x = rnd.NextDouble() * 700 + 50;
                double y = rnd.NextDouble() * 500 + 50;
                double radius = 20;
                double vx = rnd.NextDouble() * 200 - 100;
                double vy = rnd.NextDouble() * 200 - 100;

                _boardModel.AddBall(x, y, radius, vx, vy);
            }

            Balls.Clear();
            foreach (var ball in _boardModel.Balls)
                Balls.Add(ball);

            _isRunning = true;
            _timer.Start();
        }

        private void StopSimulation()
        {
            _timer.Stop();
            _isRunning = false;
        }

        private void OnTick(object? sender, EventArgs e)
        {
            _boardModel.MoveTheBalls(0.02);

            foreach (var ball in _boardModel.Balls)
            {
                ball.Refresh(); // Już jesteśmy na UI threadzie dzięki DispatcherTimer
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
