using System.ComponentModel;

namespace Model
{
    public class BallModel : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _radius;
        private double _velocityX;
        private double _velocityY;

        public double X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged(nameof(X));
                }
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged(nameof(Y));
                }
            }
        }

        public double Radius
        {
            get => _radius;
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    OnPropertyChanged(nameof(Radius));
                }
            }
        }

        public double VelocityX
        {
            get => _velocityX;
            set
            {
                if (_velocityX != value)
                {
                    _velocityX = value;
                    OnPropertyChanged(nameof(VelocityX));
                }
            }
        }

        public double VelocityY
        {
            get => _velocityY;
            set
            {
                if (_velocityY != value)
                {
                    _velocityY = value;
                    OnPropertyChanged(nameof(VelocityY));
                }
            }
        }

        public BallModel(double x, double y, double radius, double velocityX, double velocityY)
        {
            _x = x;
            _y = y;
            _radius = radius;
            _velocityX = velocityX;
            _velocityY = velocityY;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
