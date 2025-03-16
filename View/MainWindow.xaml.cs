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
        private Board _board;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _board = new Board(500, 500); // Create a board with dimensions 500x500
            InitializeBalls();
            InitializeTimer();
        }

        private void InitializeBalls()
        {
            // Initialize balls with different positions and velocities
            Ball ball1 = new Ball(10, 20, 20, 2, 2);
            Ball ball2 = new Ball(100, 100, 30, -3, -1);
            Ball ball3 = new Ball(200, 200, 25, 1, -2);
            _board.AddBall(ball1);
            _board.AddBall(ball2);
            _board.AddBall(ball3);
        }

        private void InitializeTimer()
        {
            // Create a timer that ticks every 16 milliseconds (approx 60 FPS)
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(16);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Move the balls
            _board.MoveTheBalls(1); // Move balls by 1 unit per tick

            // Clear the previous ball drawings
            canvas.Children.Clear();

            // Redraw balls at their new positions
            foreach (var ball in _board.Balls)
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = ball.Radius * 2,
                    Height = ball.Radius * 2,
                    Fill = Brushes.Red // Color of the ball
                };

                // Position the ball on the canvas
                Canvas.SetLeft(ellipse, ball.X - ball.Radius);
                Canvas.SetTop(ellipse, ball.Y - ball.Radius);
                canvas.Children.Add(ellipse); // Add the ball to the canvas
            }
        }
    }
}
