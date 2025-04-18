﻿using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;
using System;
using System.Diagnostics;
using ViewModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Xml.Linq;

public class BoardViewModel : INotifyPropertyChanged
{
    Random random = new Random();
    private readonly IBoardModel _boardModel;
    private double _boardWidth;
    public double BoardWidth
    {
        get => _boardWidth;
        set
        {
            if (_boardWidth != value)
            {
                _boardWidth = value;
                OnPropertyChanged();
                _boardModel.ResizeBoard(_boardWidth, BoardHeight);
            }
        }
    }
    private double _boardHeight;
    public double BoardHeight
    {
        get => _boardHeight;
        set
        {
            if (_boardHeight != value)
            {
                _boardHeight = value;
                OnPropertyChanged();
                _boardModel.ResizeBoard(BoardWidth, _boardHeight);
            }
        }
    }


    private readonly IDispatcher _dispatcher;
    private Timer _timer;
    public ObservableCollection<BallModel> Balls { get; set; }

    public ICommand ResizeCommand => new RelayCommand(OnResize);

    private void OnResize(object parameter)
    {
        if (parameter is SizeChangedEventArgs args)
        {
            if (args.Source is FrameworkElement element)
            {
                _boardWidth = element.ActualWidth;
                _boardHeight = element.ActualHeight;

                _boardModel.ResizeBoard(_boardWidth, _boardHeight); 
                OnPropertyChanged(nameof(_boardWidth));
                OnPropertyChanged(nameof(_boardHeight));
            }
        }
    }

    public ICommand ApplyBallsCommand { get; }
    private string _ballCountInput;
    public string BallCountInput
    {
        get => _ballCountInput;
        set
        {
            if(_ballCountInput != value)
            {
                _ballCountInput = value;
                OnPropertyChanged();
            }
        }
    }

    public BoardViewModel(IDispatcher dispatcher)
    {
        _boardModel = new BoardModel(800,600);
        _dispatcher = dispatcher;
        Balls = _boardModel.Balls;  
        BoardWidth = 800;
        BoardHeight = 600;

        // Komendy
        ApplyBallsCommand = new RelayCommand(ApplyBalls);
       

        // Timer
        _timer = new Timer(14); 
        _timer.Elapsed += OnTimerElapsed;

        _boardWidth = 800;
        _boardHeight = 600;
    }



    private void ApplyBalls(object _)
    {

        _timer.Start();
        if (int.TryParse(BallCountInput, out int count))
        {
            ClearBalls();
            for (int i = 0; i < count; i++)
            {
                AddBall(); 
            }
        }
    }

    private void AddBall()
    {
        
        _boardModel.AddBall(random.NextDouble()*500, random.NextDouble() * 500 , (random.NextDouble()*50)+50, (random.NextDouble() * 250) - 50, (random.NextDouble() * 250) - 50);
        OnPropertyChanged(nameof(Balls));
    }

    private void ClearBalls()
    {
        _boardModel.ClearBalls();
        OnPropertyChanged(nameof(Balls)); 
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _boardModel.MoveTheBalls(0.016);

        _dispatcher.Invoke(new Action(() =>
        {
            if (e.SignalTime.Second % 2 == 0) 
            {
                OnPropertyChanged(nameof(Balls));
            }

        }));
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string property = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

 

}
