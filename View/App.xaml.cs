﻿using System.Windows;
using ViewModel;

namespace View
{
    public partial class App : Application
    {
        public App()
        {
            var window = new MainWindow(); // Twoje okno
            var viewModel = new BoardViewModel(800, 600); // Rozmiar planszy
            window.DataContext = viewModel; // Ustawiamy ViewModel jako DataContext
            window.Show();
        }
    }
}
