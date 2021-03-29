using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Represents a relation between an item and a template in which the item is included.
  /// </summary>
  public class TemplateItem : RelationItem {
    public int TemplateID { get; set; }
    public virtual Template Template { get; set; }

    public TemplateItem(Template template, Item item) : base(item) {
      Template = template;
      TemplateID = Template.ID;
    }

    /// <summary>
    /// Do not use - required by EF to load entity.
    /// </summary>
    protected TemplateItem() { // Cannot be private.
    }
  }
}