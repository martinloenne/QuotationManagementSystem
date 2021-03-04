using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// Defines methods to open and close a specific window in the binding UI.
  /// </summary>
  public interface IWindowService {

    /// <summary>
    /// Opens the window using the specified view model as its data context.
    /// </summary>
    /// <param name="viewModel">The view model that should be set as data context for the window.</param>
    void Open(BaseViewModel viewModel);

    /// <summary>
    /// Opens the window as a dialog using the specified view model as its data context.
    /// </summary>
    /// <param name="baseViewModel">The view model that should be set as data context for the window.</param>
    void OpenAsDialog(BaseViewModel viewModel);

    /// <summary>
    /// Closes the window.
    /// </summary>
    void Close();
  }
}