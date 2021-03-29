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
  /// Contains information about a panel and provides methods to manipulate the panel.
  /// </summary>
  public class Panel : IRelationItemsOwner {
    // PROPERTIES

    public int ID { get; private set; }

    public string Description { get; set; }

    public int Quantity { get; set; }
    
    public double Price { get; set; }

    public double Time { get; set; }

    public double TotalPrice { get; set; }

    // Baseprice, pricedifference and adjustedprice is not really used, but can't be removed because reference
    public double BasePrice { get; set; }

    public double PriceDifference { get; private set; }

    public double AdjustedPrice { get; set; }

    public PanelType Type { get; set; }

    public virtual List<PanelItem> PanelItems { get; set; }

    public Panel() {
      PanelItems = new List<PanelItem>();
    }
  }
}