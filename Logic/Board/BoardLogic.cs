using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Data;
using System.Diagnostics; // Dodajemy przestrzeń nazw dla Debug


namespace Logic
{
    public class BoardLogic : IBoardLogic
    {
        public double Width { get; private set; }
        public double Height { get; private set; }

        private readonly List<IBall> _balls = new();
        private readonly object _ballLock = new();
        private readonly IBallLogic _ballLogic;
        private System.Timers.Timer _timer;
        private double _interval = 5; 

        public event Action BallsMoved; 

        public IReadOnlyList<IBall> Balls
        {
            get
            {
                lock (_ballLock)
                {
                    return _balls.AsReadOnly();
                }
            }
        }

        public BoardLogic(double width, double height)
        {
            Width = width;
            Height = height;
            _ballLogic = new BallLogic();

            // Ustawienie timera
            _timer = new System.Timers.Timer(_interval);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            await MoveTheBallsAsync(_interval / 1000.0);
            BallsMoved?.Invoke();
        }

        public void ResizeBoard(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY,double mass)
        {
            var ball = _ballLogic.CreateBall(x, y, radius, velocityX, velocityY,mass);
            lock (_ballLock)
            {
                _balls.Add(ball);
            }
        }

        public void RemoveBall()
        {
            lock (_ballLock)
            {
                if (_balls.Count > 0)
                {
                    _balls.RemoveAt(_balls.Count - 1);
                }
            }
        }

        public void ClearBalls()
        {
            lock (_ballLock)
            {
                _balls.Clear();
            }
        }

        public async Task MoveTheBallsAsync(double timeToMove)
        {
            List<IBall> snapshot;
            lock (_ballLock)
            {
                snapshot = new List<IBall>(_balls);
            }

            await Task.Run(() =>
            {
                Parallel.ForEach(snapshot, ball =>
                {
                    _ballLogic.Move(ball, timeToMove);
                    _ballLogic.Bounce(ball, Width, Height);
                });
            });

            int count = snapshot.Count;
            var collisionTasks = new List<Task>();

            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    var ball1 = snapshot[i];
                    var ball2 = snapshot[j];

                    collisionTasks.Add(Task.Run(() =>
                    {
                        lock (_ballLock)
                        {
                            _ballLogic.BounceBeetwenBalls(ball1, ball2);
                        }
                    }));
                }
            }

            await Task.WhenAll(collisionTasks);
        }

        public List<IBall> GetBalls()
        {
            lock (_ballLock)
            {
                return new List<IBall>(_balls);
            }
        }
    }
}
