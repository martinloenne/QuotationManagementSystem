using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public static class CollectionToObservableConverter<T, E> {

    public static ObservableCollection<T> MakeListObservable(ReadOnlyCollection<E> readOnlyCollection) {
      ObservableCollection<T> _newObservableCollection = new ObservableCollection<T>();

      foreach (E obj in readOnlyCollection) {
        _newObservableCollection.Add((T)Activator.CreateInstance(typeof(T), obj));
      }

      return _newObservableCollection;
    }
  }
}