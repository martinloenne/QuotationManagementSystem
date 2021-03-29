using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.DataAccess;
using ElogicSystem.Model;
using ElogicSystem.Helper;

namespace ElogicSystem.ViewModel {

  public class QuotationViewModel : BaseNotify {
    public Quotation QuotationModel { get; set; }

    public int ID {
      get { return QuotationModel.ID; }
    }

    public string Description {
      get { return QuotationModel.Description; }
      set { QuotationModel.Description = value; }
    }

    public DateTime Deadline {
      get { return QuotationModel.Deadline; }
      set { QuotationModel.Deadline = value; }
    }

    public string Employee {
      get { return QuotationModel.Employee; }
      set { QuotationModel.Employee = value; }
    }

    public QuotationStatusViewModel CurrentStatus {
      get { return StatusHistoryViewModels.Last(); }
    }

    // Prices
    public double BasePrice {
      get { return QuotationModel.BasePrice; }
      set { QuotationModel.BasePrice = value; }
    }

    public double Time {
      get { return QuotationModel.Time; }
      set { QuotationModel.Time = value; }
    }

    public double HourPrice {
      get { return QuotationModel.HourPrice; }
      set { QuotationModel.HourPrice = value; }
    }

    public double TotalHourPrice {
      get { return QuotationModel.TotalHourPrice; }
      set { QuotationModel.TotalHourPrice = value; }
    }

    public double ContributionRatio {
      get { return QuotationModel.ContributionRatio; }
      set {
        QuotationModel.ContributionRatio = value;
        CalculateQuotationInfo();
      }
    } // Max 1

    public double ShippingCost {
      get { return QuotationModel.ShippingCost; }
      set {
        QuotationModel.ShippingCost = value;
        //CalculateQuotationInfo();
      }
    }

    // Price before price adjustment
    public double TotalPrice {
      get { return QuotationModel.TotalPrice; }
      set {
        QuotationModel.TotalPrice = value;
        CalculateQuotationInfo();
      }
    }

    public double PriceDifference {
      get { return QuotationModel.PriceDifference; }
      set {
        QuotationModel.PriceDifference = value;
        CalculateQuotationInfo();
      }
    }

    // Price after adjustment
    public double TotalPriceAdjusted {
      get { return QuotationModel.TotalPriceAdjusted; }
      set { QuotationModel.TotalPriceAdjusted = value; }
    }

    public double ActualPrice {
      get { return QuotationModel.ActualPrice; }
      set { QuotationModel.ActualPrice = value; }
    }

    public double ActualPriceDeviation {
      get { return QuotationModel.ActualPriceDeviation; }
      set { QuotationModel.ActualPriceDeviation = value; }
    }

    public Quotation GetQuotation { get { return QuotationModel; } }

    public CustomerViewModel Customer { get; set; }

    public ObservableCollection<PanelViewModel> PanelViewModels { get; private set; }

    public ObservableCollection<QuotationStatusViewModel> StatusHistoryViewModels { get; private set; }

    public QuotationViewModel(Quotation quotationModel) {
      QuotationModel = quotationModel;

      if (QuotationModel.Customer != null) {
        Customer = new CustomerViewModel(quotationModel.Customer);
      }

      PanelViewModels = new ObservableCollection<PanelViewModel>(quotationModel.Panels.Select(p => new PanelViewModel(p)));
      StatusHistoryViewModels = new ObservableCollection<QuotationStatusViewModel>(quotationModel.StatusHistory.Select(q => new QuotationStatusViewModel(q)));
    }

    public void ChangeStatus(QuotationStatus quotationStatus) {
      QuotationModel.StatusHistory.Add(quotationStatus);
      StatusHistoryViewModels.Add(new QuotationStatusViewModel(quotationStatus));
    }

    public void AddPanel(PanelViewModel panelViewModel) {
      PanelViewModels.Add(panelViewModel);
      QuotationModel.Panels.Add(panelViewModel.PanelModel);
    }

    public void RemovePanel(PanelViewModel panelViewModel) {
      PanelViewModels.Remove(panelViewModel);
      QuotationModel.Panels.Remove(panelViewModel.PanelModel);
    }

    public void CalculateQuotationInfo() {
      BasePrice = PanelViewModels.Select(x => x.TotalPrice).Sum();
      Time = PanelViewModels.Select(x => x.Time).Sum();

      TotalHourPrice = Time * HourPrice;

      TotalPrice = BasePrice * (1 + ContributionRatio) + ShippingCost + TotalHourPrice;

      TotalPriceAdjusted = TotalPrice + PriceDifference;

      ActualPriceDeviation = ActualPrice - TotalPriceAdjusted;

      ShippingCost = CalculateShipping();
    }

    private double CalculateShipping() {
      double numberOfPanels = QuotationModel.Panels.Select(x => x.Quantity).Sum();

      double shippingCost = (((numberOfPanels / Constants.NUMBER_OF_PANELS_PER_PALLET) * Constants.BASE_SHIPPING_COST) * (1 + ContributionRatio));

      return shippingCost;
    }
  }
}