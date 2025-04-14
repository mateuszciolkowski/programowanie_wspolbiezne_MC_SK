using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using Model;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;

namespace ViewModel
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BallViewModel> Balls { get; set; }
        private System.Timers.Timer _timer;
        private readonly IBoardModel _boardModel;
        private readonly IDispatcher _dispatcher;
        private readonly Action _runOnUiThread;

        private int _amountOfBalls;
        public int AmountOfBalls
        {
            get => _amountOfBalls;
            set
            {
                _amountOfBalls = value;
                OnPropertyChanged(nameof(AmountOfBalls));
            }
        }

        public ICommand ApplyCommand { get; }

        public BoardViewModel(double height, double width, Action runOnUiThread)
        {
            _boardModel = new BoardModel(height, width);
            Balls = new ObservableCollection<BallViewModel>();
            _runOnUiThread = runOnUiThread;

            ApplyCommand = new RelayCommand(ApplyBallAmount);

            _timer = new System.Timers.Timer(50); // 20 FPS
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        private void ApplyBallAmount()
        {
            _boardModel.ClearBalls(); // Musisz dodać tę metodę w BoardModel
            var rand = new Random();

            for (int i = 0; i < AmountOfBalls; i++)
            {
                double radius = 20;
                double x = rand.NextDouble() * (_boardModel.Width - radius * 2);
                double y = rand.NextDouble() * (_boardModel.Height - radius * 2);
                double vx = rand.NextDouble() * 100 - 50;
                double vy = rand.NextDouble() * 100 - 50;

                _boardModel.AddBall(x, y, radius, vx, vy);
            }
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            _boardModel.MoveTheBalls(0.05);

            var modelBalls = _boardModel.Balls;

           
                while (Balls.Count < modelBalls.Count)
                    Balls.Add(new BallViewModel(modelBalls[Balls.Count].X, modelBalls[Balls.Count].Y, modelBalls[Balls.Count].Radius));

                while (Balls.Count > modelBalls.Count)
                    Balls.RemoveAt(Balls.Count - 1);

                for (int i = 0; i < modelBalls.Count; i++)
                {
                    Balls[i].UpdateFromBallModel(modelBalls[i]);
                }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
