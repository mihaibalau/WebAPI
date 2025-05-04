using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _can_execute;

    // Constructor for command with execution logic and optional CanExecute logic
    public RelayCommand(Action execute) : this(execute, null) { }

    public RelayCommand(Action execute, Func<bool> can_execute)
    {
        this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
        this._can_execute = can_execute;
    }

    // Determines if the command can execute
    public bool CanExecute(object parameter) => _can_execute == null || _can_execute();

    // Executes the command
    public void Execute(object parameter) => _execute();

    // Event for handling when CanExecute changes
    public event EventHandler CanExecuteChanged;

    // You can manually raise this event to indicate a change in CanExecute
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
