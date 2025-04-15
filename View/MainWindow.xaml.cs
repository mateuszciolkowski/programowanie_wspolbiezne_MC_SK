using System;
using System.Windows;
using ViewModel;

namespace View
{
    public partial class MainWindow : Window
    {
        public MainWindow(int ballCount)
        {
            InitializeComponent();

            // Tworzymy akcję do odświeżania wizualizacji
            Action invalidateVisual = () => { this.InvalidateVisual(); }; // Można to dostosować do potrzeby

            // Tworzymy ViewModel i przekazujemy parametry
            var viewModel = new BoardViewModel(600, 800, ballCount, invalidateVisual); // 10 to przykładowa liczba kulek
            DataContext = viewModel;
        }
    }
}
