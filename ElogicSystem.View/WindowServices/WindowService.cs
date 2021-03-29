using ElogicSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElogicSystem.View {

  /// <summary>
  /// Represents a service class implementing <see cref="IWindowService"/> that is able to open and close windows.
  /// </summary>
  public class WindowService : IWindowService {
    private Window _window;

    /// <summary>
    /// Instantiates a new <see cref="WindowService"/> using the specified window as its internal window
    /// on which open and close operations can be performed.
    /// </summary>
    /// <param name="window">The internal window.</param>
    public WindowService(Window window) {
      _window = window;
    }

    /// <summary>
    ///  Opens the internal window using the specified view model as its data context.
    /// </summary>
    /// <param name="viewModel">The view model that should be set as data context for the internal window.</param>
    public void Open(BaseViewModel viewModel) {
      _window.DataContext = viewModel;
      _window.Show();
    }

    /// <summary>
    /// Opens the internal window as a dialog using the specified view model as its data context.
    /// </summary>
    /// <param name="baseViewModel">The view model that should be set as data context for the internal window.</param>
    public void OpenAsDialog(BaseViewModel viewModel) {
      _window.DataContext = viewModel;
      _window.ShowDialog();
    }

    /// <summary>
    /// Closes the internal window.
    /// </summary>
    public void Close() {
      _window.Close();
    }
  }
}