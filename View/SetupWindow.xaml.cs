using System;
using System.Windows;
using ViewModel;

namespace View
{
    public partial class SetupWindow : Window
    {
        public SetupWindow()
        {
            InitializeComponent();
        }

        private void OnStartGameClicked(object sender, RoutedEventArgs e)
        {
            // Pobierz liczbę kulek z TextBox
            if (int.TryParse(BallsCountTextBox.Text, out int ballCount) && ballCount > 0)
            {
                // Utwórz obiekt ViewModel i przekaż liczbę kulek
                var gameWindow = new MainWindow(ballCount);
                gameWindow.Show();

                // Zamknij okno ustawień
                this.Close();
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić poprawną liczbę kulek.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
