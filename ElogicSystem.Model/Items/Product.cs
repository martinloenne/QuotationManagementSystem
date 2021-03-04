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

    public Manufacturer Manufacturer { get; set; }

    public override double Time {
      get { return Category.Time; }
    }

    // CONSTRUCTOR
    public Product(int id,
                   string description,
                   double quantity,
                   double price,
                   double time,
                   Category category,
                   Manufacturer manufacturer) : base(id, description, quantity, price, time, category) {
      Manufacturer = manufacturer;
    }
  }
}