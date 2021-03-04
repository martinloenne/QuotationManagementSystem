using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// Defines a factory for classes implementing <see cref="IWindowService"/>.
  /// </summary>
  public interface IWindowFactory {

    /// <summary>
    /// Returns a new instance of a class implementing <see cref="IWindowService"/>
    /// based on the specified window type.
    /// </summary>
    /// <param name="windowType">The window type that the service
    /// should be based on. </param>
    /// <returns></returns>
    IWindowService GetWindowService(WindowType windowType);
  }
}