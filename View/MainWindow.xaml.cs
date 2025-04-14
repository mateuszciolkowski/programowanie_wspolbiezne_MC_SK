using System.Windows;
using ViewModel;

namespace View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new BoardViewModel(450, 800, () => Application.Current.Dispatcher.Invoke(() => { }));
        }
    }
}
