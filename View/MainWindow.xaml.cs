using System.Windows;
using ViewModel;

namespace View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new BoardViewModel(450, 800, () => Application.Current.Dispatcher.Invoke(() => { }));
            InitializeComponent();
        }
    }
}
