using Logic;

public class Board
{
	public double Width { get; set; }
	public double Height { get; set; }
	public List<Ball> Balls { get; set; }

	public Board(double width, double height)
	{
		Width = width;
		Height = height;
		Balls = new List<Ball>();
	}

	public void AddBall(Ball ball)
	{
		Balls.Add(ball);
	}

	public sbyte moveTheBalls(double timeToMove)
	{
		foreach (var ball in Balls)
		{
			ball.Move(timeToMove);
			ball.Bounce(Width, Height);
		}
		return 0;
	}

	public void Display()
	{
		foreach (var ball in Balls)
		{
			Console.WriteLine($"X: {ball.X}, Y: {ball.Y}, Radius: {ball.Radius}");
		}
	}
}