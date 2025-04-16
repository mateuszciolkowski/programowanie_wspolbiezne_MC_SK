using System.ComponentModel;
using Data;

namespace ViewModel
{
    public class BallViewModel : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _radius;

        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(nameof(X)); }
        }

        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(nameof(Y)); }
        }

        public double Radius
        {
            get => _radius;
            set { _radius = value; OnPropertyChanged(nameof(Radius)); }
        }

        public BallViewModel(double x, double y, double radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public void Update(Ball modelBall)
        {
            X = modelBall.X;
            Y = modelBall.Y;
            Radius = modelBall.Radius;
        }
        public void UpdateFromBallModel(Model.BallModel modelBall)
        {
            X = modelBall.X;
            Y = modelBall.Y;
            Radius = modelBall.Radius;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
