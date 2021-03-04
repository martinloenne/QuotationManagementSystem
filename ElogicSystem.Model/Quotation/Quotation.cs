using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Contains information about a qoutation and provides methods to manipulate the quotation.
  /// </summary>
  public class Quotation {

    // PROPERTIES
    public int ID { get; set; }

    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Employee { get; set; }

    public double Time {
      get { return CalculateTime(); }
    }

    // Prices
    public double BasePrice {
      get { return CalculateBasePrice(); }
    }

    public double ContributionRatio { get; set; } // Max 1
    public double ShippingCost { get; set; }
    // Price before price adjustment

    public double HourPrice { get; set; }

    public double TotalHourPrice {
      get { return CalculateTotalHourPrice(); }
    }

    public double TotalPrice {
      get { return CalculateTotalPrice(); }
    }

    public double PriceDifference { get; set; }

    // Price after adjustment
    public double TotalPriceAdjusted {
      get { return CalculateTotalPriceAdjusted(); }
    }

    public double ActualPrice { get; set; }

    public double ActualPriceDeviation {
      get { return CalculateActualPriceDeviation(); }
    }

    public Customer Customer { get; set; }

    // Lists
    public List<IQuantifiable<Panel>> Panels { get; private set; }

    public List<QuotationStatus> StatusHistory { get; private set; }

    public QuotationStatus CurrentStatus {
      get { return StatusHistory.Last(); }
    }

    // CONSTRUCTOR

    /// <summary>
    /// Initializes a new instance of the Quotation class.
    /// </summary>
    /// <param name="quotationStatus">Initial quotation status</param>
    public Quotation(QuotationStatus quotationStatus) {
      Panels = new List<IQuantifiable<Panel>>();

      StatusHistory = new List<QuotationStatus> {
        quotationStatus
      };

      PriceDifference = 0;
    }

    // METHODS
    private double CalculateBasePrice() => Panels.Select(x => x.Quantity * ((Panel)x).AdjustedPrice).Sum();

    public double CalculateTime() => Panels.Select(x => x.Quantity * ((Panel)x).Time).Sum();

    private double CalculateTotalHourPrice() => Time * HourPrice;

    private double CalculateTotalPrice() => BasePrice * (1 + ContributionRatio) + ShippingCost + TotalHourPrice;

    private double CalculateTotalPriceAdjusted() => TotalPrice + PriceDifference;

    private double CalculateActualPriceDeviation() => ActualPrice - TotalPrice;
  }
}