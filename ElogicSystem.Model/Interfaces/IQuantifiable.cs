using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Defines an object containg an ID and a quantity.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IQuantifiable<T> {
    // PROPERTIES

    int ID { get; }
    double Quantity { get; set; }
  }
}