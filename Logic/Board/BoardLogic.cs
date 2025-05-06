using System.Timers;
using Data;

namespace Logic
{
    public class BoardLogic : IBoardLogic, IDisposable
    {
        public double Width { get; private set; }
        public double Height { get; private set; }

        private readonly List<IBall> _balls = new List<IBall>();
        private readonly object _ballLock = new object();
        private readonly IBallLogic _ballLogic;
        private readonly System.Timers.Timer _timer;
        private readonly double _intervalMs = 1;
        private readonly Dictionary<IBall, CancellationTokenSource> _ballTasks = new();

        public event Action BallsMoved;

        public IReadOnlyList<IBall> Balls
        {
            get
            {
                lock (_ballLock)
                {
                    return _balls.ToList();
                }
            }
        }

        public BoardLogic(double width, double height, BallLogic ballLogic)
        {
            Width = width;
            Height = height;
            //_ballLogic = new BallLogic();
            _ballLogic = ballLogic;
            _timer = new System.Timers.Timer(_intervalMs);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Synchronous update to avoid task overhead
            UpdateBalls(_intervalMs / 1000.0);
            BallsMoved?.Invoke();
        }

        public void ResizeBoard(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY, double mass)
        {
            var ball = _ballLogic.CreateBall(x, y, radius, velocityX, velocityY, mass);
            var cts = new CancellationTokenSource();

            int ballIndex;
            lock (_ballLock)
            {
                _balls.Add(ball);
                _ballTasks[ball] = cts;
                ballIndex = _balls.Count - 1;
            }

            Task.Run(async () =>
            {
               
                Thread.CurrentThread.Name = $"BallThread_{ballIndex}_{Guid.NewGuid()}";
                


                while (!cts.Token.IsCancellationRequested)
                {
                    lock (_ballLock)
                    {
                        _ballLogic.Move(ball, _intervalMs / 1000.0);
                        _ballLogic.Bounce(ball, Width, Height);
                    }

                    BallsMoved?.Invoke();
                    await Task.Delay((int)_intervalMs, cts.Token);
                }

            }, cts.Token);
        }


        public void RemoveBall()
        {
            lock (_ballLock)
            {
                if (_balls.Count > 0)
                {
                    var ball = _balls[^1];
                    _balls.RemoveAt(_balls.Count - 1);
                    if (_ballTasks.TryGetValue(ball, out var cts))
                    {
                        cts.Cancel();
                        _ballTasks.Remove(ball);
                    }
                }
            }
        }

        public void ClearBalls()
        {
            lock (_ballLock)
            {
                foreach (var cts in _ballTasks.Values)
                {
                    cts.Cancel();
                }

                _ballTasks.Clear();
                _balls.Clear();
            }
        }

        private void UpdateBalls(double deltaTime)
        {

            List<IBall> snapshot;
            lock (_ballLock)
            {
                snapshot = _balls.ToList();
            }
            //foreach (var ball in snapshot)
            //{
            //    _ballLogic.Move(ball, deltaTime);
            //    _ballLogic.Bounce(ball, Width, Height);
            //}

            int count = snapshot.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    _ballLogic.BounceBetweenBalls(snapshot[i], snapshot[j]);
                }
            }
        }

        public List<IBall> GetBalls()
        {
            lock (_ballLock)
            {
                return _balls.ToList();
            }
        }

        public async Task<List<IBall>> GetBallsAsync()
        {
            // Simulacja operacji asynchronicznej, np. pobieranie z bazy danych, API itp.
            return await Task.FromResult(new List<IBall>(_balls));
        }


        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}