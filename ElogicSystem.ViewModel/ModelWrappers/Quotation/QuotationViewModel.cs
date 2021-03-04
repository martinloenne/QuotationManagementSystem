using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class QuotationViewModel : BaseNotify {
    private Quotation _quotationModel;
    private ObservableCollection<PanelViewModel> _panelViewModels;
    private ObservableCollection<QuotationStatusViewModel> _statusHistoryViewModels;

    public int ID {
      get { return _quotationModel.ID; }
      set { _quotationModel.ID = value; }
    }

    public string Description {
      get { return _quotationModel.Description; }
      set { _quotationModel.Description = value; }
    }

    public DateTime Deadline {
      get { return _quotationModel.Deadline; }
      set { _quotationModel.Deadline = value; }
    }

    public string Employee {
      get { return _quotationModel.Employee; }
      set { _quotationModel.Employee = value; }
    }

    public QuotationStatusViewModel CurrentStatus {
      get { return StatusHistoryViewModels.Last(); }
    }

    // Prices
    public double BasePrice {
      get { return _quotationModel.BasePrice; }
    }

    public double ContributionRatio {
      get { return _quotationModel.ContributionRatio; }
      set { _quotationModel.ContributionRatio = value; }
    } // Max 1

    public double ShippingCost {
      get { return _quotationModel.ShippingCost; }
      set { _quotationModel.ShippingCost = value; }
    }

    // Price before price adjustment
    public double TotalPrice {
      get { return _quotationModel.TotalPrice; }
    }

    public double PriceDifference {
      get { return _quotationModel.PriceDifference; }
    }

    // Price after adjustment
    public double TotalPriceAdjusted {
      get { return _quotationModel.TotalPriceAdjusted; }
    }

    public Quotation GetQuotation { get { return _quotationModel; } }

    public CustomerViewModel Customer { get; set; }

    public ReadOnlyObservableCollection<PanelViewModel> PanelViewModels { get; private set; }

    public ReadOnlyObservableCollection<QuotationStatusViewModel> StatusHistoryViewModels { get; private set; }

    public QuotationViewModel() {
      _quotationModel = new Quotation(new QuotationStatus(DateTime.Now, QuotationStatusType.Created, ""));
      _quotationModel.ID = 0;
      _quotationModel.Description = "";
      _quotationModel.Deadline = DateTime.Now;
      _quotationModel.ContributionRatio = 0;
      _quotationModel.ShippingCost = 0;

      Employee = "";
      Customer = new CustomerViewModel();

      _panelViewModels = new ObservableCollection<PanelViewModel>(_quotationModel.Panels.Select(p => new PanelViewModel((Panel)p)));
      PanelViewModels = new ReadOnlyObservableCollection<PanelViewModel>(_panelViewModels);

      _statusHistoryViewModels = new ObservableCollection<QuotationStatusViewModel>(_quotationModel.StatusHistory.Select(p => new QuotationStatusViewModel(p)));
      StatusHistoryViewModels = new ReadOnlyObservableCollection<QuotationStatusViewModel>(_statusHistoryViewModels);
    }

    public QuotationViewModel(Quotation quotationModel) {
      _quotationModel = quotationModel;

      Customer = new CustomerViewModel(quotationModel.Customer);

      _panelViewModels = new ObservableCollection<PanelViewModel>(quotationModel.Panels.Select(p => new PanelViewModel((Panel)p)));
      PanelViewModels = new ReadOnlyObservableCollection<PanelViewModel>(_panelViewModels);

      _statusHistoryViewModels = new ObservableCollection<QuotationStatusViewModel>(quotationModel.StatusHistory.Select(p => new QuotationStatusViewModel(p)));
      StatusHistoryViewModels = new ReadOnlyObservableCollection<QuotationStatusViewModel>(_statusHistoryViewModels);
    }

    public void AddPanel(PanelViewModel panel) => AddPanel(panel, 1);

    public void AddPanel(PanelViewModel panel, double quantity) {
      Panel temp = panel.GetPanel;
      QuantifiableObjectHandler<Panel, PanelViewModel>.Add(_quotationModel.Panels,
                                                           _panelViewModels,
                                                           temp,
                                                           panel,
                                                           quantity);
    }

    public void AddPanel(Panel panel) => AddPanel(panel, 1);

    public void AddPanel(Panel panel, double quantity) {
      QuantifiableObjectHandler<Panel, PanelViewModel>.Add(_quotationModel.Panels,
                                                           _panelViewModels,
                                                           panel,
                                                           new PanelViewModel(panel),
                                                           quantity);
    }

    public void RemovePanel(PanelViewModel panel) {
      Panel temp = panel.GetPanel;
      RemovePanel(temp, 1);
    }


    public void RemovePanel(Panel panel) => RemovePanel(panel, 1);

    public void RemovePanel(Panel panel, double quantity) {
      QuantifiableObjectHandler<Panel, PanelViewModel>.Remove(_quotationModel.Panels,
                                                        _panelViewModels,
                                                        panel,
                                                        quantity);
    }

    public void OverrideTotalPrice(double newTotalPriceAdjusted) {
      _quotationModel.PriceDifference = newTotalPriceAdjusted - _quotationModel.TotalPrice;
    }

    public void OverridePriceDifference(double newPriceDifference) => _quotationModel.PriceDifference = newPriceDifference;

    public void ChangeStatus(QuotationStatus quotationStatus) {
      _quotationModel.StatusHistory.Add(quotationStatus);
      _statusHistoryViewModels.Add(new QuotationStatusViewModel(quotationStatus));
    }
  }
}