using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Represents a <see cref="Container"/> capable of containing both classes of type <see cref="Product"/>
  /// and <see cref="Module"/>.
  /// </summary>
  public class Block : Container {
    // CONSTRUCTOR

    public Block(int id, string description, double quantity, double price, double time, Category category) :
      base(id, description, quantity, price, time, category) {
    }
  }
}