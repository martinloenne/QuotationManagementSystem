using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// Represents a class implementing <see cref="INotifyPropertyChanged" /> enabling inheriting classes
  /// to automatically notify the view, when its properties changes.
  /// </summary>
  public abstract class BaseNotify : INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;
  }
}