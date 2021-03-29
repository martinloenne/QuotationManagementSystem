using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Represents a type of <see cref="Item"/> capable of containing other items.
  /// </summary>
  public abstract class Container : Item, IRelationItemsOwner {
    public virtual List<ContainerItem> ContainerItems { get; set; }

    public Container() {
      ContainerItems = new List<ContainerItem>();
    }
  }
}