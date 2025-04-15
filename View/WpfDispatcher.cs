using System.Windows;

public class WpfDispatcher : IDispatcher
{
    public void Invoke(Action action)
    {
        Application.Current.Dispatcher.Invoke(action);
    }
}
