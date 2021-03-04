using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {

  public class ConfigureCustomerViewModel : BaseViewModel {

    private CustomerViewModel _inputCustomer;

    // Commands
    /// <summary>
    /// A command to save the configuresion of the item
    /// </summary>
    public ICommand SaveCommand { get; private set; }

    /// <summary>
    /// A command to cancel the configuresion
    /// </summary>
    public ICommand CancelCommand { get; private set; }

    // Properties

    /// <summary>
    /// A temperary customer.
    /// </summary>
    public CustomerViewModel TemporaryCustomer { get; }

    /// <summary>
    /// Property that describs if the item need to be saved in the collection
    /// </summary>
    public bool NeedToBeAdded { get; private set; }

    // Constructor

    /// <summary>
    /// Constructor to initilize a configure viewmodel of a customer
    /// </summary>
    /// <param name="window">A class that implements IWindowFactory</param>
    /// <param name="customer">The customer to configure</param>
    public ConfigureCustomerViewModel(Action onClose, CustomerViewModel customer) : base(onClose) {
      TemporaryCustomer = new CustomerViewModel(new Customer(new CustomerInfo("","","","","","","")));
      _inputCustomer = customer;
      NeedToBeAdded = false;
      CopyInformation(TemporaryCustomer, _inputCustomer);

      // Commands
      SaveCommand = new RelayCommand(o => SaveChanges(), o => true);
      CancelCommand = new RelayCommand(o => OnClose(), o => true);
    }

    /// <summary>
    /// Method called on Command <see cref="SaveCommand"/>.
    /// </summary>
    /// <param name="obj"></param>
    private void SaveChanges() {
      CopyInformation(_inputCustomer, TemporaryCustomer);
      NeedToBeAdded = true;
      OnClose();
    }

    /// <summary>
    /// Method to copy all the information from one item to another.
    /// </summary>
    /// <param name="customerCopyTo">The item to copy information to</param>
    /// <param name="customerCopyFrom">The item to copy information from</param>
    private void CopyInformation(CustomerViewModel customerCopyTo, CustomerViewModel customerCopyFrom) {
      customerCopyTo.CustomerInfo.Name = string.Copy(customerCopyFrom.CustomerInfo.Name);
      customerCopyTo.CustomerInfo.PhoneNumber = string.Copy(customerCopyFrom.CustomerInfo.PhoneNumber);
      customerCopyTo.CustomerInfo.Email = string.Copy(customerCopyFrom.CustomerInfo.Email);
      customerCopyTo.CustomerInfo.BillingAddress = string.Copy(customerCopyFrom.CustomerInfo.BillingAddress);
      customerCopyTo.CustomerInfo.BillingZipCode = string.Copy(customerCopyFrom.CustomerInfo.BillingZipCode);
      customerCopyTo.CustomerInfo.ShippingAddress = string.Copy(customerCopyFrom.CustomerInfo.ShippingAddress);
      customerCopyTo.CustomerInfo.ShippingZipCode = string.Copy(customerCopyFrom.CustomerInfo.ShippingZipCode);
    }
  }
}