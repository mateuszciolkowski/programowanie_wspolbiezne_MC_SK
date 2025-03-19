using Data;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class Board
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public List<Ball> Balls { get; set; }

        private BallLogic _balllogic;

        public Board(double width, double height)
        {
            Width = width;
            Height = height;
            Balls = new List<Ball>();
            _balllogic = new BallLogic();
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            Balls.Add(new Ball(x, y, radius, velocityX, velocityY));
        }

        public void MoveTheBalls(double timeToMove)
        {
            foreach (var ball in Balls)
            {
                _balllogic.Move(ball, timeToMove);
                _balllogic.Bounce(ball, Width, Height);
            }
        }
    }

}
