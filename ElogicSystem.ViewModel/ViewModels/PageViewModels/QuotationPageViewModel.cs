using ElogicSystem.DataAccess;
using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {

  public class QuotationPageViewModel : BaseViewModel, IDisposableViewModel {
    private IWindowFactory _windowFactory;

    public ObservableCollection<QuotationViewModel> Quotations { get; set; }
    public ObservableCollection<TemplateViewModel> Templates { get; set; }
    public ObservableCollection<CustomerViewModel> ActiveCustomers { get; set; }

    private CustomerViewModel _selectedCustomer;

    public CustomerViewModel SelectedCustomer {
      get { return _selectedCustomer; }
      set {
        _selectedCustomer = value;
        SelectedQuotation.Customer = _selectedCustomer;
      }
    }

    private QuotationViewModel _selectedQuotation;

    public QuotationViewModel SelectedQuotation {
      get { return _selectedQuotation; }
      set {
        _selectedQuotation = value;
        ShowPanels = true;
      }
    }

    public PanelViewModel SelectedPanel { get; set; }

    private int _selectedPanelTypeIndex;

    public int SelectedPanelTypeIndex {
      get { return _selectedPanelTypeIndex; }
      set {
        _selectedPanelTypeIndex = value;
        SetPanelType();
      }
    }

    public ObservableCollection<string> PanelTypes { get; set; }

    // Quotation commands

    public ICommand CreateNewQuotationCommand { get; set; }
    public ICommand ConfigureQuotationCommand { get; set; }
    public ICommand DeleteQuotationCommand { get; set; }
    public ICommand PrintQuotationCommand { get; set; }

    // Panel commands
    public ICommand CreateNewPanelCommand { get; set; }

    public ICommand EditPanelCommand { get; set; }
    public ICommand DeletePanelCommand { get; set; }

    public TemplateViewModel TemplateViewModel { get; set; }

    public bool ShowPanels { get; set; }

    private UnitOfWork _unitOfWork;

    public QuotationPageViewModel(Action onClose, IWindowFactory windowFactory) : base(onClose) {
      _windowFactory = windowFactory;

      LoadDB();

      PanelTypes = new ObservableCollection<string>() {
       "Iron",
       "Insulation Plastic"
      };

      // Commands
      CreateNewQuotationCommand = new RelayCommand(o => CreateNewQuotation(), o => true);
      ConfigureQuotationCommand = new RelayCommand(o => ConfigureQuotation(), o => SelectedQuotation != null);
      DeleteQuotationCommand = new RelayCommand(o => DeleteQuotation(), o => SelectedQuotation != null);
      PrintQuotationCommand = new RelayCommand(o => PrintQuotation(), o => SelectedQuotation != null);
      CreateNewPanelCommand = new RelayCommand(o => CreateNewPanel(), o => true);
      EditPanelCommand = new RelayCommand(o => EditPanel(), o => SelectedPanel != null);
      DeletePanelCommand = new RelayCommand(o => DeletePanel(), o => SelectedPanel != null);
    }

    // Methods
    private void CreateNewQuotation() {
      QuotationViewModel quotation = new QuotationViewModel(new Quotation(new QuotationStatus(DateTime.Now, QuotationStatusType.Created)));
      Quotations.Add(quotation);

      _unitOfWork.QuotationRepository.Add(quotation.QuotationModel);
      _unitOfWork.Save();

      IWindowService configureQuotationView = _windowFactory.GetWindowService(WindowType.ConfigureQuotationView);
      ConfigureQuotationViewModel configureQuotationViewModel = new ConfigureQuotationViewModel(configureQuotationView.Close, quotation, ActiveCustomers, _windowFactory, _unitOfWork);

      configureQuotationView.OpenAsDialog(configureQuotationViewModel);

      if (!configureQuotationViewModel.NeedToBeAdded) {
        _unitOfWork.QuotationRepository.Delete(quotation.QuotationModel);
        _unitOfWork.Save();
        Quotations.Remove(quotation);
      }
    }

    private void ConfigureQuotation() {
      if (SelectedQuotation != null) {
        IWindowService configureQuotationView = _windowFactory.GetWindowService(WindowType.ConfigureQuotationView);

        ConfigureQuotationViewModel configureQuotationViewModel = new ConfigureQuotationViewModel(configureQuotationView.Close, SelectedQuotation, ActiveCustomers, _windowFactory, _unitOfWork);
        configureQuotationView.OpenAsDialog(configureQuotationViewModel);

        _unitOfWork.Dispose();
        LoadDB();
      }
    }

    private void DeleteQuotation() {
      if (SelectedQuotation != null) {
        _unitOfWork.QuotationRepository.Delete(SelectedQuotation.QuotationModel);
        _unitOfWork.Save();

        Quotations.Remove(SelectedQuotation);
      }
    }

    private void PrintQuotation() {
      if (SelectedQuotation != null) {
        IPrintableWindowService printQuotationView = _windowFactory.GetWindowService(WindowType.PrintQuotationView) as IPrintableWindowService;
        printQuotationView.Open(new PrintQuotationViewModel(printQuotationView.Close, SelectedQuotation, printQuotationView));
      }
    }

    private void CreateNewPanel() {
      if (SelectedQuotation != null) {
        PanelViewModel panelViewModel = new PanelViewModel(new Panel());
        panelViewModel.Quantity = 1;

        _unitOfWork.PanelRepository.Add(panelViewModel.PanelModel);
        SelectedQuotation.AddPanel(panelViewModel);

        _unitOfWork.Save();
      }
    }

    private void EditPanel() {
      if (SelectedPanel != null) {
        IWindowService panelBuilderView = _windowFactory.GetWindowService(WindowType.PanelBuilderView);
        BuilderViewModel panelBuilderViewModel = new PanelBuilderViewModel(panelBuilderView.Close, _windowFactory, Templates.First(), SelectedPanel, _unitOfWork, Templates);

        panelBuilderView.OpenAsDialog(panelBuilderViewModel);
        panelBuilderViewModel.ResetSourceQuantities();

        SelectedQuotation.CalculateQuotationInfo();
      }
    }

    private void DeletePanel() {
      if (SelectedPanel != null) {
        _unitOfWork.PanelRepository.Delete(SelectedPanel.PanelModel);
        SelectedQuotation.RemovePanel(SelectedPanel);
        _unitOfWork.Save();
      }
    }

    private void SetPanelType() {
      switch (SelectedPanelTypeIndex) {
        case 0:
          SelectedPanel.PanelModel.Type = PanelType.Iron;
          break;

        case 1:
          SelectedPanel.PanelModel.Type = PanelType.InsulationPlastic;
          break;

        default:
          break;
      }
    }

    private void LoadDB() {
      _unitOfWork = new UnitOfWork(new ElogicSystemContext());

      List<Template> templateModels = _unitOfWork.TemplateRepository.GetAll().ToList();
      Templates = new ObservableCollection<TemplateViewModel>(templateModels.Select(t => new TemplateViewModel(t)));

      List<Quotation> quotationModels = _unitOfWork.QuotationRepository.GetAll().ToList();
      Quotations = new ObservableCollection<QuotationViewModel>(quotationModels.Select(q => new QuotationViewModel(q)));

      List<Customer> customerModels = _unitOfWork.CustomerRepository.GetByActivity(true).ToList();
      ActiveCustomers = new ObservableCollection<CustomerViewModel>(customerModels.Select(c => new CustomerViewModel(c)));
    }

    public void SaveAndDispose() {
      _unitOfWork?.Save();
      _unitOfWork?.Dispose();
    }
  }
}