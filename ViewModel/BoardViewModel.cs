using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Logic;
using Model;

namespace ViewModel
{
    public class BoardViewModel
    {
        private readonly IBoardLogic _boardLogic;

        public ObservableCollection<BallModel> Balls { get; }
        public double BoardWidth { get; set; }
        public double BoardHeight { get; set; }
        public int AmountOfBalls { get; set; }

        public ICommand ApplyCommand { get; }

        public BoardViewModel()
        {
            // Inicjalizujemy logikę planszy
            _boardLogic = new BoardLogic(BoardWidth, BoardHeight);
            Balls = new ObservableCollection<BallModel>();
            ApplyCommand = new RelayCommand(ApplyChanges);

            // Możesz tutaj dodać inicjalne piłki
            AddBalls();
        }

        private void AddBalls()
        {
            // Przykład dodania 5 piłek losowo
            for (int i = 0; i < 5; i++)
            {
                double x = new Random().NextDouble() * BoardWidth;
                double y = new Random().NextDouble() * BoardHeight;
                double radius = new Random().Next(20, 30);
                double vx = new Random().Next(-3, 3);
                double vy = new Random().Next(-3, 3);
                _boardLogic.AddBall(x, y, radius, vx, vy);
            }
            UpdateBalls();
        }

        private void ApplyChanges()
        {
            // Logika aplikowania zmian
            _boardLogic.ResizeBoard(BoardWidth, BoardHeight);
            UpdateBalls();
        }

        private void UpdateBalls()
        {
            Balls.Clear();
            foreach (var ball in _boardLogic.Balls)
            {
                Balls.Add(new BallModel(ball.X, ball.Y, ball.Radius));
            }
        }
    }
}
