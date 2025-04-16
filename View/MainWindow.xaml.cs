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
                var dispatcher = new WpfDispatcher(); 
                this.DataContext = new BoardViewModel(dispatcher);
            }
        }
       
        }
    }

