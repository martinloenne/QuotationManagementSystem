using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Provides universal information about an item.
  /// </summary>
  public abstract class Item : IQuantifiable<Item> {

    // PROPERTIES
    public int ID { get; set; }

    public string Description { get; set; }
    public double Quantity { get; set; }
    public virtual double Price { get; set; }
    public virtual double Time { get; set; }
    public Category Category { get; set; }

    // CONSTRUCTOR
    /// <summary>
    /// Initializes a new instance of the <see cref="Item"/> class setting all it properties
    /// based on the specified constructor parameters.
    /// </summary>
    /// <param name="id">The unique ID for this item.</param>
    /// <param name="description">A description of the item.</param>
    /// <param name="quantity">The quanitity of the item. </param>
    /// <param name="price">The price of the item. </param>
    /// <param name="time">How much time that should be allocated for this item.</param>
    public Item(int id,
                string description,
                double quantity,
                double price,
                double time,
                Category category) {
      ID = id;
      Description = description;
      Quantity = quantity;
      Price = price;
      Time = time;
      Category = category;
    }

    // METHODS
    /// <summary>
    /// Override equals to define equality by ID.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj) {
      if (!(obj is Item other))
        return base.Equals(obj);

      return Equals(ID, other.ID);
    }
  }
}