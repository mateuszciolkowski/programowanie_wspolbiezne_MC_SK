using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Presentation.Model;
using Data;

namespace Presentation.ViewModel
{
    public class BallViewModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
    }

    public class BoardViewModel : INotifyPropertyChanged
    {
        private readonly BoardModel _model;
        public ObservableCollection<BallViewModel> Balls { get; } = new();

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand AddBallCommand { get; }

        public BoardViewModel()
        {
            _model = new BoardModel(800, 600);
            _model.Updated += RefreshBalls;

            StartCommand = new RelayCommand(_ => _model.Start());
            StopCommand = new RelayCommand(_ => _model.Stop());
            AddBallCommand = new RelayCommand(_ => _model.AddBall(100, 100, 20, 100, 150));
        }

        private void RefreshBalls()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Balls.Clear();
                foreach (var b in _model.GetBalls())
                {
                    Balls.Add(new BallViewModel { X = b.X, Y = b.Y, Radius = b.Radius });
                }
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
