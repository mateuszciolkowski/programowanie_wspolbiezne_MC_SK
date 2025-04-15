using System.Windows;
using ViewModel;

namespace View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (Application.Current != null)
            {
                // Tworzymy instancję Dispatcher
                var dispatcher = new WpfDispatcher(); // Implementacja IDispatcher dla WPF
                var width = this.Width;  // Możesz ustawić szerokość w kodzie-behind lub przekazać z XAML
                var height = this.Height;  // Możesz ustawić wysokość w kodzie-behind lub przekazać z XAML

                // Ustawiamy DataContext
                this.DataContext = new BoardViewModel(dispatcher, width, height);
            }
        }
    }
}
