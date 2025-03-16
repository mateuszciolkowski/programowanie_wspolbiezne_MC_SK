using System;
using System.Collections.Generic;

namespace Logic
{
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

        public void MoveTheBalls(double timeToMove)
        {
            foreach (var ball in Balls)
            {
                ball.Move(timeToMove);
                ball.Bounce(Width, Height);
            }
        }
    }
}
