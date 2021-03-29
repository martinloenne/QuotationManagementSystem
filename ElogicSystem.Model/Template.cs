using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Contains information about a template and provides methods to manipulate the template.
  /// </summary>
  public class Template : IRelationItemsOwner {
    // PROPERTIES

    public int ID { get; private set; }
    public string Description { get; set; }

    public virtual List<TemplateItem> TemplateItems { get; set; }

    public Template() {
      TemplateItems = new List<TemplateItem>();
    }
  }
}