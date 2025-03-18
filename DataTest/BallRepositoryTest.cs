using System;
using System.Collections.Generic;
using Xunit;

namespace proj.Data
{
    public class BallRepositoryTest
    {
        [Fact]
        public void AddBall_ShouldIncreaseCount()
        {
            var repository = new BallRepository();
            var ball = new Ball(1, 1, 1, 1, 1);

            repository.AddBall(ball);
            var balls = repository.GetAllBalls();

            Assert.Single(balls);
            Assert.Contains(ball, balls);
        }

        [Fact]
        public void RemoveBall_ShouldDecreaseCount()
        {
            var repository = new BallRepository();
            var ball = new Ball(1, 1, 1, 1, 1);
            repository.AddBall(ball);

            repository.RemoveBall(ball);
            var balls = repository.GetAllBalls();

            Assert.Empty(balls);
        }

        [Fact]
        public void GetAllBalls_ShouldReturnCopyOfList()
        {
            var repository = new BallRepository();
            var ball1 = new Ball(1, 1, 1, 1, 1);
            var ball2 = new Ball(2, 2, 2, 2, 2);
            repository.AddBall(ball1);
            repository.AddBall(ball2);

            var balls = repository.GetAllBalls();
            balls.Clear();

            Assert.Equal(2, repository.GetAllBalls().Count);
        }

        [Fact]
        public void Clear_ShouldRemoveAllBalls()
        {
            var repository = new BallRepository();
            repository.AddBall(new Ball(1, 1, 1, 1, 1));
            repository.AddBall(new Ball(2, 2, 2, 2, 2));

            repository.Clear();
            var balls = repository.GetAllBalls();

            Assert.Empty(balls);
        }
    }

}
