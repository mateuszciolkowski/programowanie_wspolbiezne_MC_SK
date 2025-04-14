using System.Windows;
using ViewModel;

namespace View
{
    public partial class App : Application
    {
        public App()
        {
            var window = new MainWindow(); 
            var viewModel = new BoardViewModel(450, 800, () => Application.Current.Dispatcher.Invoke(() => { }));
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
