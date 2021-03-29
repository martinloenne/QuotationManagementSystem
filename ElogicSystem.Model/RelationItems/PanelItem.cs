using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Represents a relation between an item and a panel in which the item is included.
  /// </summary>
  public class PanelItem : RelationItem {
    public int PanelID { get; set; }
    public virtual Panel Panel { get; set; }

    public PanelItem(Panel panel, Item item) : base(item) {
      Panel = panel;
      PanelID = Panel.ID;
    }

    /// <summary>
    /// Do not use - required by EF to load entity.
    /// </summary>
    protected PanelItem() { // Cannot be private.
    }
  }
}