using Data;
using Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyApp
{
    public partial class MainWindow : Window 
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    
    }
    //public partial class MainWindow : Window
    //{
    //    private IBoard _board;
    //    private DispatcherTimer _timer;

    //    public MainWindow()
    //    {
    //        InitializeComponent();
    //        _board = new Board(550, 550);
    //        InitializeBalls();
    //        InitializeTimer();
    //    }

    //    private void InitializeBalls()
    //    {
    //        // Używamy metod Board do dodawania kul
    //        _board.AddBall(50, 20, 20, 50, 2);
    //        _board.AddBall(100, 100, 30, -3, -1);
    //        _board.AddBall(200, 200, 25, 1, -2);
    //    }

    //    private void InitializeTimer()
    //    {
    //        _timer = new DispatcherTimer();
    //        _timer.Interval = TimeSpan.FromMilliseconds(16);
    //        _timer.Tick += Timer_Tick;
    //        _timer.Start();
    //    }

    //    private void Timer_Tick(object sender, EventArgs e)
    //    {
    //        // Przemieszczamy piłki
    //        _board.MoveTheBalls(1);

    //        // Czyszczymy płótno przed rysowaniem nowych kółek
    //        canvas.Children.Clear();

    //        // Iteracja po piłkach na planszy
    //        foreach (var ball in _board.Balls)
    //        {
    //            // Tworzenie elipsy do reprezentacji piłki
    //            Ellipse ellipse = new Ellipse
    //            {
    //                Width = ball.Radius * 2,
    //                Height = ball.Radius * 2,
    //                Fill = Brushes.Red
    //            };

    //            // Ustawienie pozycji elipsy na płótnie
    //            Canvas.SetLeft(ellipse, ball.X - ball.Radius);
    //            Canvas.SetTop(ellipse, ball.Y - ball.Radius);

    //            // Dodanie elipsy do płótna
    //            canvas.Children.Add(ellipse);
    //        }
    //    }
    //}
}
