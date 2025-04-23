using System.ComponentModel;

public class BallModel : INotifyPropertyChanged
{
    private double _x;
    private double _y;
    private double _radius;
    private double _velocityX;
    private double _velocityY;

    public BallModel(double x, double y, double radius)
    {
        _x = x;
        _y = y;
        _radius = radius;
    }

    public double X
    {
        get => _x;
        set
        {
            if (_x != value)
            {
                _x = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(XDisplay));
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
                OnPropertyChanged();
                OnPropertyChanged(nameof(YDisplay));
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
                OnPropertyChanged();
                OnPropertyChanged(nameof(XDisplay));
                OnPropertyChanged(nameof(YDisplay));
            }
        }
    }

    public double VelocityX
    {
        get => _velocityX;
        set => _velocityX = value;
    }

    public double VelocityY
    {
        get => _velocityY;
        set => _velocityY = value;
    }

    public double XDisplay => X - Radius / 2;
    public double YDisplay => Y - Radius / 2;

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
