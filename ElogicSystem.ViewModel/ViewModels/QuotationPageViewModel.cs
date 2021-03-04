using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {
  public class QuotationPageViewModel : BaseViewModel {
    private ObservableCollection<QuotationViewModel> _quotations;
    private ObservableCollection<CustomerViewModel> _customers;
    private ObservableCollection<ItemViewModel> _items;
    private IWindowFactory _windowFactory;

    public ReadOnlyObservableCollection<QuotationViewModel> Quotations { get; set; }
    public ReadOnlyObservableCollection<CustomerViewModel> Customers { get; set; }

    public QuotationViewModel SelectedQuotation { get; set; }
    public PanelViewModel SelectedPanel { get; set; }

    // Quotation commands

    public ICommand CreateNewQuotationCommand{ get; set; }
    public ICommand ConfigureQuotationCommand{ get; set; }
    public ICommand DeleteQuotationCommand{ get; set; }
    public ICommand PrintQuotationCommand{ get; set; }

    // Panel commands

    public ICommand CreateNewPanelCommand { get; set; }
    public ICommand EditPanelCommand { get; set; }
    public ICommand DeletePanelCommand { get; set; }

    public QuotationPageViewModel(Action onClose, ObservableCollection<QuotationViewModel> quotations, ObservableCollection<CustomerViewModel> customers, ObservableCollection<ItemViewModel> items, IWindowFactory windowFactory) : base(onClose) {
      _quotations = quotations;
      _customers = customers;
      _items = items;
      _windowFactory = windowFactory;

      Quotations = new ReadOnlyObservableCollection<QuotationViewModel>(_quotations);
      Customers = new ReadOnlyObservableCollection<CustomerViewModel>(_customers);

      // Commands
      CreateNewQuotationCommand = new RelayCommand(CreateNewQuotation, o => true); ;
      ConfigureQuotationCommand = new RelayCommand(ConfigureQuotation, o => SelectedQuotation != null ? true : false);
      DeleteQuotationCommand = new RelayCommand(DeleteQuotation, o => SelectedQuotation != null ? true : false);
      PrintQuotationCommand = new RelayCommand(PrintQuotation, o => SelectedQuotation != null ? true : false);
      CreateNewPanelCommand = new RelayCommand(CreateNewPanel, o => true);
      EditPanelCommand = new RelayCommand(EditPanel, o => SelectedPanel != null ? true : false);
      DeletePanelCommand = new RelayCommand(DeletePanel, o => SelectedPanel != null ? true : false);
    }

    // Methods
    /// <summary>
    /// Adds a quotation to the collection.
    /// </summary>
    /// <param name="item">The item to add.</param>
    private void AddQuotaionToCollection(QuotationViewModel quotation) {
      _quotations.Add(quotation);
    }

    /// <summary>
    /// Removes a specific quotation from the collection.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    private void RemoveQuoationFromCollection(QuotationViewModel quotation) {
      _quotations.Remove(quotation);
    }

    private void AddPanelToCollection(PanelViewModel panel) {
      if (SelectedQuotation != null) {
        SelectedQuotation.AddPanel(panel);
      }
    }

    private void RemovePanelFromCollection(PanelViewModel panel) {
      if (SelectedQuotation != null) {
        SelectedQuotation.RemovePanel(panel);
      }
    }

    private void CreateNewQuotation(object obj) {
      IWindowService configureQuotationView = _windowFactory.GetWindowService(WindowType.ConfigureQuotationView);
      QuotationViewModel quotation = new QuotationViewModel();
      ConfigureQuotationViewModel configureQuotationViewModel = new ConfigureQuotationViewModel(configureQuotationView.Close, quotation, _customers);
      configureQuotationView.OpenAsDialog(configureQuotationViewModel);

      if (configureQuotationViewModel.NeedToBeAdded) {
        AddQuotaionToCollection(quotation);
        quotation.Customer.AddQuotation(quotation.GetQuotation);
      }
    }

    private void ConfigureQuotation(object obj) {
      if (SelectedQuotation != null) {
        IWindowService configureQuotationView = _windowFactory.GetWindowService(WindowType.ConfigureQuotationView);
        ConfigureQuotationViewModel configureQuotationViewModel = new ConfigureQuotationViewModel(configureQuotationView.Close, SelectedQuotation, _customers);
        configureQuotationView.OpenAsDialog(configureQuotationViewModel);
      }
    }

    private void DeleteQuotation(object obj) {
      if (SelectedQuotation != null) {
        RemoveQuoationFromCollection(SelectedQuotation);
      }
    }

    private void PrintQuotation(object obj) {
      if (SelectedQuotation != null) {
        IWindowService printQuotationView = _windowFactory.GetWindowService(WindowType.PrintQuotationView);
        if (printQuotationView is IPrintableWindowService) {
          IPrintableWindowService temp = printQuotationView as IPrintableWindowService;
          temp.Open(new PrintQuotationViewModel(printQuotationView.Close, SelectedQuotation, temp));
        }
      }
    }

    private void CreateNewPanel(object obj) {
      if (SelectedQuotation != null) {
        PanelViewModel panelViewModel = new PanelViewModel(new Panel(0, "", PanelType.Iron));
        AddPanelToCollection(panelViewModel);
      }
    }

    private void EditPanel(object obj) {
      if (SelectedPanel != null) {
        IWindowService panelBuilderView = _windowFactory.GetWindowService(WindowType.PanelBuilderView);
        PanelBuilderViewModel panelBuilderViewModel = new PanelBuilderViewModel(panelBuilderView.Close, _windowFactory, SelectedPanel);
        panelBuilderView.OpenAsDialog(panelBuilderViewModel);
      }
    }

    private void DeletePanel(object obj) {
      if (SelectedPanel != null) {
        RemovePanelFromCollection(SelectedPanel);
      }
    }
  }
}
