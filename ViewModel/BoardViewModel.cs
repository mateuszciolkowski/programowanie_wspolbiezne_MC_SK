using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class BoardViewModel : INotifyPropertyChanged
{
    private BoardViewModel _boardModel;

    public BoardViewModel(BoardViewModel boardModel)
    {
        _boardModel = boardModel;
    }

    public IReadOnlyList<IBall> Balls => _boardModel.GeTBalls();
}
