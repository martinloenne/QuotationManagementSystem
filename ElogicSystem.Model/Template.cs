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
  public class Template {
    // PROPERTIES

    public int ID { get; set; }
    public string Description { get; set; }

    public List<IQuantifiable<Item>> Items { get; private set; }

    // CONSTRUCTOR

    public Template(int id, string description) {
      ID = id;
      Description = description;
      Items = new List<IQuantifiable<Item>>();
    }
  }
}