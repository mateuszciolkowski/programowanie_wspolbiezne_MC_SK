using System;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute; // Akcja do wykonania
        private readonly Func<bool>? _canExecute; // Określa, czy komenda może zostać wykonana

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute; // Zapisujemy akcję
            _canExecute = canExecute; // Zapisujemy opcjonalną funkcję, która sprawdza, czy komenda może zostać wykonana
        }

        // Ta metoda sprawdza, czy komenda może być wykonana (np. czy przycisk jest aktywowany)
        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke() ?? true; // Jeśli nie podano funkcji, domyślnie pozwalamy na wykonanie komendy
        }

        // Ta metoda wykonuje akcję
        public void Execute(object? parameter)
        {
            _execute(); // Wywołuje zapisaną akcję
        }

        // Wydarzenie, które informuje UI, że stan komendy (np. czy może być wykonana) uległ zmianie
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; } // Zarejestrowanie eventu
            remove { CommandManager.RequerySuggested -= value; } // Wyrejestrowanie eventu
        }
    }
}
