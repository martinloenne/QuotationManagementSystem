using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// Provides methods for manipulating a <see cref="IQuantifiable{T}"/> object collection.
  /// </summary>
  /// <typeparam name="T">The type of the collection</typeparam>
  public static class QuantifiableObjectHandler<T, E> {

    /// <summary>
    /// Adds the given quantity of an object implementing <see cref="IQuantifiable{T}"/>
    /// to the specified collection of <see cref="IQuantifiable{T}"/>.
    /// </summary>
    /// <param name="collection">The collection to add the object to.</param>
    /// <param name="objectToAdd">The object to add to the collection.</param>
    /// <param name="quantity">The quantity of the object to be added</param>
    public static void Add(List<IQuantifiable<T>> modelCollection,
                           ObservableCollection<E> viewModelCollection,
                           IQuantifiable<T> modelObjectToAdd,
                           E viewModelObjectToAdd,
                           double quantity) {
      int index = GetIndexByID(modelCollection, modelObjectToAdd);

      if (index == -1) {
        modelObjectToAdd.Quantity += quantity;
        modelCollection.Add(modelObjectToAdd);
        viewModelCollection.Add(viewModelObjectToAdd);
      }
      else {
        modelCollection[index].Quantity = modelCollection[index].Quantity + quantity;
      }
    }

    /// <summary>
    /// Removes the given quantity of an object implementing <see cref="IQuantifiable{T}"/>
    /// to the specified collection of <see cref="IQuantifiable{T}"/>.
    /// </summary>
    /// <param name="collection">The collection to remove the object from.</param>
    /// <param name="objectToAdd">The object to remove from the collection.</param>
    /// <param name="quantity">The quantity of the object to be removed</param>
    public static void Remove(List<IQuantifiable<T>> modelCollection,
                              ObservableCollection<E> viewModelCollection,
                              IQuantifiable<T> modelObjectToRemove,
                              double quantity) {
      int index = GetIndexByID(modelCollection, modelObjectToRemove);

      if (index != -1) {
        if (modelCollection[index].Quantity - quantity <= 0) {   // validates if item should be removed accordingly
          modelCollection.RemoveAt(index);
          viewModelCollection.RemoveAt(index);
        }
        else {
          modelCollection[index].Quantity = modelCollection[index].Quantity - quantity;
        }
      }
    }

    /// <summary>
    /// Gets the object from the specified <see cref="IQuantifiable{T}"/> collection
    /// that is equal to the given <see cref="IQuantifiable{T}"/> object.
    /// Throws an exeption if the object is not found.
    /// </summary>
    /// <param name="collection">The collection to get the object from.</param>
    /// <param name="objectToGet">The object that equals the object in the collection.</param>
    /// <returns></returns>
    public static T Get(List<IQuantifiable<T>> collection,
                        IQuantifiable<T> objectToGet) {
      int index = GetIndexByID(collection, objectToGet);

      if (Contains(collection, objectToGet)) {
        return (T)collection[index];
      }
      else {
        return default;
      }
    }

    /// <summary>
    /// Determines whether the given <see cref="IQuantifiable{T}"/> object exists in the specified
    /// <see cref="IQuantifiable{T}"/> collection
    /// </summary>
    /// <param name="collection">The collection to search for the object.</param>
    /// <param name="objectToCheck">The object that equals the object in the collection.</param>
    /// <returns></returns>
    public static bool Contains(List<IQuantifiable<T>> collection,
                                IQuantifiable<T> objectToCheck) {
      int index = GetIndexByID(collection, objectToCheck);

      if (index == -1) {
        return false;
      }
      else {
        return true;
      }
    }

    private static int GetIndexByID(List<IQuantifiable<T>> collection, IQuantifiable<T> targetObject) {
      return collection.IndexOf(collection.Where(x => x.ID == targetObject.ID).FirstOrDefault());
    }
  }
}