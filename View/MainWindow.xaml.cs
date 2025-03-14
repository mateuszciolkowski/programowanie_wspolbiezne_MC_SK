using System.Windows;
using Logic;
using System.Windows.Shapes;
using System.Windows.Media;

namespace BallAndBoardWpfApp
{
    public partial class MainWindow : Window
    {
        private Board board;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            board = new Board(100, 100);

            Ball ball1 = new Ball(10, 20, 2, 1, 1);
            Ball ball2 = new Ball(30, 40, 3, 1, 1);
            Ball ball3 = new Ball(50, 60, 1, 1, 1);

            board.AddBall(ball1);
            board.AddBall(ball2);
            board.AddBall(ball3);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16); 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            board.moveTheBalls(0.1); 

            gameCanvas.Children.Clear();

            foreach (var ball in board.Balls)
            {
                DrawBall(ball);
            }
        }

        private void DrawBall(Ball ball)
        {
            Ellipse ballShape = new Ellipse
            {
                Width = ball.Radius * 2,
                Height = ball.Radius * 2,
                Fill = Brushes.Blue
            };

            Canvas.SetLeft(ballShape, ball.X - ball.Radius);  
            Canvas.SetTop(ballShape, ball.Y - ball.Radius);   

            gameCanvas.Children.Add(ballShape);
        }
    }
}
