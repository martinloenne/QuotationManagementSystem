using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Contains information about a panel and provides methods to manipulate the the panel.
  /// </summary>
  public class Panel : IQuantifiable<Panel> {
    // PROPERTIES
    private double _quantity;

    public int ID { get; set; }
    public string Description { get; set; }

    public double Quantity {
      get { return _quantity; }
      set { _quantity = Math.Floor(value); }
    }

    // Has to implement this because of interface, need to find better alternative. Da ikke mere???
    public double Price { get; set; }

    public double Time {
      get { return CalculateTime(); }
    }

    public double BasePrice {
      get { return CalculateBasePrice(); }
    }

    public double PriceDifference { get; private set; }

    public double AdjustedPrice {
      get { return CalculateAdjustedPrice(); }
    }

    // Panel price * Qty = Net Price
    public double TotalPrice {
      get { return CalculateTotalPrice(); }
    }

    public PanelType Type { get; set; }

    public List<IQuantifiable<Item>> Items { get; private set; }

    // CONSTRUCTOR

    public Panel(int id, string description, PanelType type) {
      ID = id;
      Description = description;
      Type = type;

      Items = new List<IQuantifiable<Item>>();
      PriceDifference = 0;
    }

    // METHODS

    /// <summary>
    /// Overrides the price of the panel
    /// </summary>
    /// <param name="newPrice">New price of the panel</param>
    public void OverridePrice(double newPrice) => PriceDifference = newPrice - BasePrice;

    /// <summary>
    /// Overrides the price difference between new price and base price
    /// </summary>
    /// <param name="newPriceDifference">New price difference</param>
    public void OverridePriceDifference(double newPriceDifference) => PriceDifference = newPriceDifference;

    private double CalculateTime() => Items.Select(x => ((Item)x).Time * x.Quantity).Sum();

    private double CalculateBasePrice() => Items.Select(x => ((Item)x).Price * x.Quantity).Sum();

    private double CalculateAdjustedPrice() => BasePrice + PriceDifference;

    private double CalculateTotalPrice() => AdjustedPrice * Quantity;
  }
}