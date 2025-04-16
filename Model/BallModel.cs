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
        _x = x - radius;
        _y = y - radius;
        _radius = radius;
        //_velocityX = velocityX;
        //_velocityY = velocityY;
        
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


    

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
