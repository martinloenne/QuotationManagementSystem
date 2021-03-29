using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  public abstract class RelationItem {
    public int ItemID { get; set; }

    public virtual Item Item { get; set; }

    public double Quantity { get; set; }

    public RelationItem(Item item) {
      Item = item;
      ItemID = Item.ID;
    }

    /// <summary>
    /// Do not use - required by EF to load entity.
    /// </summary>
    protected RelationItem() { // Cannot be private.
    }
  }
}