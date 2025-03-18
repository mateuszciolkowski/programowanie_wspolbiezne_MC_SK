using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace proj.Data
{
    public class BallRepository
    {
        private List<Ball> balls = [];

        public void AddBall(Ball ball) => balls.Add(ball);
        public void RemoveBall(Ball ball) => balls.Remove(ball);
        public List<Ball> GetAllBalls() => new(balls);

        public void Clear()
        {
            foreach (var ball in balls)
            {
                RemoveBall(ball);
            }
        }
    }

}

