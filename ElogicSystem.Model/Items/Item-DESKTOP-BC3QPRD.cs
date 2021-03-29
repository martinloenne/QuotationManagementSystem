using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Provides universal information about an item.
  /// </summary>
  public abstract class Item {
    // PROPERTIES
    public int ID { get; set; }

    public string Description { get; set; }

    public virtual double Price { get; set; }

    /// <summary>
    /// Time it takes to install the product given in time.
    /// </summary>
    public virtual double Time { get; set; }

    // METHODS
    /// <summary>
    /// Defines equality by ID.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj) {
      if(!(obj is Item other))
        return base.Equals(obj);

      return Equals(ID, other.ID);
    }
  }
}