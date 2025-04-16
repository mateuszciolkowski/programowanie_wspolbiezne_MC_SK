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
               

                // Ustawiamy DataContext
                this.DataContext = new BoardViewModel(dispatcher);
            }
        }
       
        }
    }

