using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// Represents a base view model inhereting from <see cref="BaseNotify"/>
  /// that provides a method to close the associated view.
  /// </summary>
  public abstract class BaseViewModel : BaseNotify {
    private Action _onClose;

    public BaseViewModel(Action onClose) {
      _onClose = onClose;
    }

    /// <summary>
    /// Closes the main view associated to this view model.
    /// </summary>
    public void OnClose() {
      _onClose();
    }
  }
}