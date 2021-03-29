using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Represents a relation between an item and a container in which the item is included.
  /// </summary>
  public class ContainerItem : RelationItem {
    public int ContainerID { get; set; }
    public virtual Container Container { get; set; }

    public ContainerItem(Container container, Item item) : base(item) {
      Container = container;
      ContainerID = Container.ID;
    }

    /// <summary>
    /// Do not use - required by EF to load entity.
    /// </summary>
    protected ContainerItem() { // Cannot be private.
    }
  }
}