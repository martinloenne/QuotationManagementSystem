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
  public abstract class Container : Item {

    // PROPERTIES
    // Item list for to be accessed by higher architecture.
    public List<IQuantifiable<Item>> Items { get; private set; }

    // Overrides Price property in parent Item class.
    public override double Price {
      get { return CalculatePrice(); }
    }

    public override double Time {
      get { return CalculateTime(); }
    }

    // CONSTRUCTOR
    public Container(int id,
                     string description,
                     double quantity,
                     double price,
                     double time,
                     Category category) : base(id, description, quantity, price, time, category) {
      Items = new List<IQuantifiable<Item>>();
    }

    // METHODS
    private double CalculatePrice() => Items.Select(x => ((Item)x).Price).Sum();

    private double CalculateTime() => Items.Select(x => ((Item)x).Time).Sum();
  }
}