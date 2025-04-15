using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BallModel> Balls { get; set; }
        private readonly IBoardModel _boardModel;
        private readonly Action _invalidateVisual;

        public ICommand AddBallCommand { get; }
        public ICommand RemoveBallCommand { get; }
        public ICommand ClearBallsCommand { get; }
        public ICommand MoveBallsCommand { get; }

        // Konstruktor publiczny bez parametrów
        public BoardViewModel()
        {
            _boardModel = new BoardModel(800, 600); // Zainicjalizuj domyślną szerokość i wysokość
            Balls = _boardModel.Balls;
            _invalidateVisual = () => { }; // Pusta akcja
            AddBallCommand = new RelayCommand(AddBall);
            RemoveBallCommand = new RelayCommand(RemoveBall);
            ClearBallsCommand = new RelayCommand(ClearBalls);
            MoveBallsCommand = new RelayCommand(MoveBalls);
        }

        public BoardViewModel(double height, double width, int ballCount, Action invalidateVisual)
        {
            _boardModel = new BoardModel(width, height);
            Balls = _boardModel.Balls;
            _invalidateVisual = invalidateVisual;

            // Dodaj kulki do gry na podstawie przekazanej liczby
            for (int i = 0; i < ballCount; i++)
            {
                AddBall();
            }

            AddBallCommand = new RelayCommand(AddBall);
            RemoveBallCommand = new RelayCommand(RemoveBall);
            ClearBallsCommand = new RelayCommand(ClearBalls);
            MoveBallsCommand = new RelayCommand(MoveBalls);
        }

        private void AddBall()
        {
            _boardModel.AddBall(50, 50, 20, 100, 80); // przykładowe dane
            OnPropertyChanged(nameof(Balls));
            _invalidateVisual();
        }

        private void RemoveBall()
        {
            _boardModel.RemoveBall();
            OnPropertyChanged(nameof(Balls));
            _invalidateVisual();
        }

        private void ClearBalls()
        {
            _boardModel.ClearBalls();
            OnPropertyChanged(nameof(Balls));
            _invalidateVisual();
        }

        private void MoveBalls()
        {
            _boardModel.MoveTheBalls(0.1); // np. 0.1 sekundy
            OnPropertyChanged(nameof(Balls));
            _invalidateVisual();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
