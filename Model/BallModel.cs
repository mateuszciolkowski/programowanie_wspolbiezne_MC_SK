using System.ComponentModel;

public class BallModel : INotifyPropertyChanged
{
    private double _x;
    private double _y;
    private double _radius;
    private double _velocityX; // Prędkość w osi X
    private double _velocityY; // Prędkość w osi Y

    // Konstruktor przyjmujący 5 argumentów
    public BallModel(double x, double y, double radius, double velocityX, double velocityY)
    {
        _x = x;
        _y = y;
        _radius = radius;
        _velocityX = velocityX;
        _velocityY = velocityY;
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
