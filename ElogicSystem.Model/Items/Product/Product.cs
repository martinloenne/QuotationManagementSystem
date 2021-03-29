using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Represents the most simple type of <see cref="Item"/>.
  /// </summary>
  public class Product : Item {
    // PROPERTIES
    public string Link { get; set; }

    public virtual Manufacturer Manufacturer { get; set; }
    public virtual Category Category { get; set; }
  }
}