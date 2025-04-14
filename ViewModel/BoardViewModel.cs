using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using Model;

namespace ViewModel
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BallModel> Balls { get; set; }
        private System.Timers.Timer _timer;
        private readonly IBoardModel _boardModel;

        // Action do synchronizacji z aplikacją główną
        private readonly Action? _updateUIAction;

        public BoardViewModel(double height, double width, Action? updateUIAction = null)
        {
            _boardModel = new BoardModel(height, width);
            Balls = new ObservableCollection<BallModel>(_boardModel.Balls);
            _updateUIAction = updateUIAction;

            _timer = new System.Timers.Timer(50); // 20 FPS
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            _boardModel.MoveTheBalls(0.05);

            // Jeżeli aplikacja WPF dostarczyła akcję do synchronizacji, wywołujemy ją
            _updateUIAction?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
