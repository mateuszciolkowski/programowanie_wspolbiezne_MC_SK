using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;
using Data;

namespace Model
{
    public class BallModel : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _radius;

        public BallModel(double x, double y, double radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public double X => _x;
        public double Y => _y;
        public double Radius
        {
            get => _radius;
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(XDisplay));
                    OnPropertyChanged(nameof(YDisplay));
                }
            }
        }

        public double XDisplay => _x - _radius / 2;
        public double YDisplay => _y - _radius / 2;

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdatePosition(double newX, double newY)
        {
            bool changed = false;
            if (!newX.Equals(_x)) { _x = newX; OnPropertyChanged(nameof(X)); changed = true; }
            if (!newY.Equals(_y)) { _y = newY; OnPropertyChanged(nameof(Y)); changed = true; }
            if (changed)
            {
                OnPropertyChanged(nameof(XDisplay));
                OnPropertyChanged(nameof(YDisplay));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}