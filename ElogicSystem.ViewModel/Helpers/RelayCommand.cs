using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// Represents a command implementing <see cref="ICommand"/>
  /// which behaviour is fully determined by the instantiating caller.
  /// </summary>
  public class RelayCommand : ICommand {
    private Action<object> _execute;
    private Func<object, bool> _canExecute;

    public event EventHandler CanExecuteChanged {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/>
    /// setting the implementation of its public methods to the given parameters.
    /// </summary>
    /// <param name="execute">The method that should be invoked when the command's Execute-method is called.</param>
    /// <param name="canExecute">The method that should be invoked when the command's CanExecute-method is called.</param>
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute) {
      _execute = execute;
      _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) {
      return _canExecute(parameter);
    }

    public void Execute(object parameter) {
      _execute(parameter);
    }
  }
}