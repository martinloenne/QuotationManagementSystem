using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Contains information about and provides methods to manipulate a quotation.
  /// </summary>
  public class Quotation {

    // PROPERTIES
    public int ID { get; private set; }

    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Employee { get; set; }

    public double Time { get; set; }

    // Prices
    public double BasePrice { get; set; }

    public double ContributionRatio { get; set; } // Max 1
    public double ShippingCost { get; set; }

    public double HourPrice { get; set; }

    public double TotalHourPrice { get; set; }

    public double TotalPrice { get; set; }

    public double PriceDifference { get; set; }

    // Price after adjustment
    public double TotalPriceAdjusted { get; set; }

    public double ActualPrice { get; set; }

    public double ActualPriceDeviation { get; set; }

    public virtual Customer Customer { get; set; }

    // Lists
    public virtual List<Panel> Panels { get; private set; }

    public virtual List<QuotationStatus> StatusHistory { get; private set; }

    public QuotationStatus CurrentStatus {
      get { return StatusHistory.Last(); }
    }

    // CONSTRUCTOR

    /// <summary>
    /// Initializes a new instance of the Quotation class.
    /// </summary>
    /// <param name="quotationStatus">Initial quotation status</param>
    public Quotation(QuotationStatus quotationStatus) {
      Panels = new List<Panel>();
      StatusHistory = new List<QuotationStatus>();
      StatusHistory.Add(quotationStatus);
    }

    // Needed for EF to instantiate class.
    /// <summary>
    /// Do not use - required by EF to load entity.
    /// </summary>
    protected Quotation() { // Cannot be private.
    }

    private double CalculateTotalHourPrice() => Time * HourPrice;

    private double CalculateTotalPrice() => BasePrice * (1 + ContributionRatio) + ShippingCost + TotalHourPrice;

    private double CalculateTotalPriceAdjusted() => TotalPrice + PriceDifference;

    private double CalculateActualPriceDeviation() => ActualPrice - TotalPrice;
  }
}