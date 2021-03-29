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

  /// <summary>
  ///
  /// </summary>
  public class ConfigureQuotationViewModel : BaseViewModel {
    private IWindowFactory _windowFactory;

    private QuotationStatusType _currentStatusType;

    /// <summary>
    /// A temperary quotaion
    /// </summary>
    public QuotationViewModel Quotation { get; set; }

    public ObservableCollection<CustomerViewModel> ActiveCustomers { get; set; }

    public int SelectedCustomerIndex { get; set; }

    public CustomerViewModel SelectedCustomer { get; set; }

    public string CurrentDeadline { get; private set; }

    public ObservableCollection<string> QuotationStatuses { get; set; }

    public DateTime SelectedDate {
      get { return Quotation.Deadline; }
      set {
        Quotation.Deadline = value;
        CurrentDeadline = value.ToShortDateString();
      }
    }

    public QuotationStatusType CurrentStatusType {
      get { return _currentStatusType; }
      set {
        if (Quotation.CurrentStatus.Type != value) {
          HasCurrentStatusChanged = true;
        }
        else {
          HasCurrentStatusChanged = false;
        }
        _currentStatusType = value;
      }
    }

    private int _selectedQuotationStatusIndex;

    public int SelectedQuotationStatusIndex {
      get { return _selectedQuotationStatusIndex; }
      set {
        _selectedQuotationStatusIndex = value;
        SetQuotationStatus();
      }
    }

    /// <summary>
    /// Describes if the quotation need to be added to the collection if it is a new quotation.
    /// </summary>
    public bool NeedToBeAdded { get; private set; }

    public bool HasCurrentStatusChanged { get; private set; }
    public string LastToChangeStatus { get; set; }

    /// <summary>
    /// Command to save the changes of the configuresion
    /// </summary>
    public ICommand SaveCommand { get; private set; }

    /// <summary>
    /// Command to cancel the changes of the configuresion
    /// </summary>
    public ICommand CancelCommand { get; private set; }

    private UnitOfWork _unitOfWork;

    /// <summary>
    /// Initilizes a new configution of a quotation
    /// </summary>
    /// <param name="window">A class that implements IWindowFactory</param>
    /// <param name="quotation">The quoation that need to be configured</param>
    public ConfigureQuotationViewModel(Action onClose, QuotationViewModel quotation, ObservableCollection<CustomerViewModel> activeCustomers,
                                       IWindowFactory windowFactory, UnitOfWork unitOfWork) : base(onClose) {
      Quotation = quotation;
      _windowFactory = windowFactory;
      _unitOfWork = unitOfWork;

      QuotationStatuses = new ObservableCollection<string>() {
        "Created",
        "Sent",
        "Lost",
        "Confirmed",
        "Canceled"
      };

      CurrentStatusType = Quotation.CurrentStatus.Type;

      NeedToBeAdded = false;

      ActiveCustomers = activeCustomers;
      CurrentDeadline = Quotation.Deadline.ToShortDateString();

      if (Quotation.Customer != null) {
        SelectedCustomerIndex = ActiveCustomers.IndexOf(ActiveCustomers.Where(x => x.ID == Quotation.Customer.ID).FirstOrDefault());
      }

      // Commands
      SaveCommand = new RelayCommand(o => SaveChanges(), o => true);
      CancelCommand = new RelayCommand(CancelDialog, o => true);
    }

    /// <summary>
    /// A method called by the save command.
    /// </summary>
    /// <param name="obj"></param>
    private void SaveChanges() {
      Quotation.Customer = SelectedCustomer;
      NeedToBeAdded = true;
      _unitOfWork.Save();
      OnClose();
    }

    private void SetQuotationStatus() {
      switch (SelectedQuotationStatusIndex) {
        case 0:
          Quotation.ChangeStatus(new QuotationStatus(DateTime.Now, QuotationStatusType.Created));
          break;

        case 1:
          Quotation.ChangeStatus(new QuotationStatus(DateTime.Now, QuotationStatusType.Sent));
          break;

        case 2:
          Quotation.ChangeStatus(new QuotationStatus(DateTime.Now, QuotationStatusType.Lost));
          break;

        case 3:
          Quotation.ChangeStatus(new QuotationStatus(DateTime.Now, QuotationStatusType.Confirmed));
          break;

        case 4:
          Quotation.ChangeStatus(new QuotationStatus(DateTime.Now, QuotationStatusType.Canceled));
          break;

        default:
          break;
      }
    }

    private void CancelDialog(object obj) {
      IWindowService cancelDialog = _windowFactory.GetWindowService(WindowType.CancelDialogView);
      cancelDialog.OpenAsDialog(new CancelDialogViewModel(cancelDialog.Close, OnClose));
    }
  }
}