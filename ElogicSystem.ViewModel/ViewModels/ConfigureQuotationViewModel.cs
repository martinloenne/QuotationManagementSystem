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
    private QuotationViewModel _inputQuotation;
    private ObservableCollection<CustomerViewModel> _customers;

    private QuotationStatusType _currentStatusType;

    /// <summary>
    /// A temperary quotaion
    /// </summary>
    public QuotationViewModel TemporaryQuotation { get; set; }
    public List<string> CustomersNames { get; set; }

    public string SelectedCustomer { get; set; }

    public string CurrentDeadline { get; private set; }

    public DateTime SelectedDate { 
      get { return TemporaryQuotation.Deadline; }
      set {
        TemporaryQuotation.Deadline = value;
        CurrentDeadline = value.ToShortDateString();
      }
    }


    public QuotationStatusType CurrentStatusType {
      get { return _currentStatusType; }
      set {
        if (_inputQuotation.CurrentStatus.Type != value) {
          HasCurrentStatusChanged = true;
        } else {
          HasCurrentStatusChanged = false;
        }
        _currentStatusType = value;
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

    /// <summary>
    /// Initilizes a new configution of a quotation
    /// </summary>
    /// <param name="window">A class that implements IWindowFactory</param>
    /// <param name="quotation">The quoation that need to be configured</param>
    public ConfigureQuotationViewModel(Action onClose, QuotationViewModel quotation, ObservableCollection<CustomerViewModel> customers) : base(onClose) {
      TemporaryQuotation = new QuotationViewModel();
      _inputQuotation = quotation;

      CurrentStatusType = _inputQuotation.CurrentStatus.Type;
      CopyInformation(TemporaryQuotation, _inputQuotation);

      NeedToBeAdded = false;

      _customers = customers;
      SelectedCustomer = string.Copy(TemporaryQuotation.Customer.CustomerInfo.Name);
      CustomersNames = _customers.Select(x => x.CustomerInfo.Name).ToList();
      CurrentDeadline = TemporaryQuotation.Deadline.ToShortDateString();

      // Commands
      SaveCommand = new RelayCommand(o => SaveChanges(), o => true);
      CancelCommand = new RelayCommand(o => OnClose(), o => true);
    }

    /// <summary>
    /// A method called by the save command.
    /// </summary>
    /// <param name="obj"></param>
    private void SaveChanges() {
      CopyInformation(_inputQuotation, TemporaryQuotation);
      if (HasCurrentStatusChanged) {
        _inputQuotation.ChangeStatus(new QuotationStatus(DateTime.Now, _currentStatusType, LastToChangeStatus));
      }
      if (SelectedCustomer != "") {

        _inputQuotation.Customer = _customers.Where(x => x.CustomerInfo.Name.Equals(SelectedCustomer)).First();
      }
      NeedToBeAdded = true;
      OnClose();
    }

    /// <summary>
    /// Method to copy the information from one item to another item.
    /// </summary>
    /// <param name="quotationCopyTo">The Quotation to copy the inforamtion to</param>
    /// <param name="quotationCopyFrom">The Quotation to copy the information from</param>
    private void CopyInformation(QuotationViewModel quotationCopyTo, QuotationViewModel quotationCopyFrom) {
      quotationCopyTo.ID = quotationCopyFrom.ID;
      quotationCopyTo.Description = string.Copy(quotationCopyFrom.Description);
      quotationCopyTo.Employee = string.Copy(quotationCopyFrom.Employee);
      quotationCopyTo.Deadline = quotationCopyFrom.Deadline;
      quotationCopyTo.ContributionRatio = quotationCopyFrom.ContributionRatio;
      quotationCopyTo.ShippingCost = quotationCopyFrom.ShippingCost;
      quotationCopyTo.Customer = quotationCopyFrom.Customer;
    }

  }
}