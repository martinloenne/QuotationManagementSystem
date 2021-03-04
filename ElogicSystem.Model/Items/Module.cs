using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Represents a <see cref="Container"/> capable of containning classes of type <see cref="Product"/>.
  /// </summary>
  public class Module : Container {
    // CONSTRUCTOR

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="quantity"></param>
    /// <param name="price"></param>
    /// <param name="time"></param>
    public Module(int id, string description, double quantity, double price, double time, Category category) :
      base(id, description, quantity, price, time, category) {
    }
  }
}