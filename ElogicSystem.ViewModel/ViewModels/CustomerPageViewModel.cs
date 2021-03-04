using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {
  public class CustomerPageViewModel : BaseViewModel {
    private ObservableCollection<CustomerViewModel> _customers;

    private ObservableCollection<CustomerViewModel> _activatedCustomers;
    private ObservableCollection<CustomerViewModel> _deactivatedCustomers;
    private IWindowFactory _windowFactory;

    public ReadOnlyObservableCollection<CustomerViewModel> CustomersToShow { get; set; }


    public object SelectedCustomer { get; set; }
    public bool ShowDeactivatedCustomers { get; set; }

    public ICommand CreateNewCustomerCommand { get; set; }
    public ICommand ConfigureCustomerCommand { get; set; }
    public ICommand DeleteCustomerCommand { get; set; }
    public ICommand ActivateCustomerCommand { get; set; }
    public ICommand DeactivateCustomerCommand { get; set; }
    public ICommand ShowActivedCustomersCommand { get; set; }
    public ICommand ShowDeactivedCustomersCommand { get; set; }

    public CustomerPageViewModel(Action onClose, IWindowFactory windowFactory, ObservableCollection<CustomerViewModel> customers) : base(onClose) {
      _customers = customers;
      List<CustomerViewModel> activeCustomers = _customers.Where(x => x.IsActive == true).ToList();
      List<CustomerViewModel> deactiveCustomers = _customers.Where(x => x.IsActive == false).ToList();
      _activatedCustomers = new ObservableCollection<CustomerViewModel>(activeCustomers);
      _deactivatedCustomers = new ObservableCollection<CustomerViewModel>(deactiveCustomers);

      _windowFactory = windowFactory;
      CustomersToShow = new ReadOnlyObservableCollection<CustomerViewModel>(_activatedCustomers);

      CreateNewCustomerCommand = new RelayCommand(CreateNewCustomer, o => true);
      ConfigureCustomerCommand = new RelayCommand(ConfigureCustomer, o => SelectedCustomer is CustomerViewModel ? true : false);
      DeleteCustomerCommand = new RelayCommand(DeleteCustomer, o => SelectedCustomer is CustomerViewModel ? true : false);
      ActivateCustomerCommand = new RelayCommand(ActivateCustomer, o => SelectedCustomer is CustomerViewModel ? true : false);
      DeactivateCustomerCommand = new RelayCommand(DeactivateCustomer, o => SelectedCustomer is CustomerViewModel ? true : false);
      ShowActivedCustomersCommand = new RelayCommand(ShowActiveCustomers, o => true); ;
      ShowDeactivedCustomersCommand = new RelayCommand(ShowDeactiveCustomers, o => true);

    }

    /// <summary>
    /// Adds a customer to the collection.
    /// </summary>
    /// <param name="customer">The customer to add.</param>
    public void AddCustomerToCollection(CustomerViewModel customer) {
      if (customer.IsActive) {
        _activatedCustomers.Add(customer);
      } else {
        _deactivatedCustomers.Add(customer);
      }
      _customers.Add(customer);
    }

    public void RemoveCustomerFromCollection(CustomerViewModel customer) {
      if (customer.IsActive) {
        _activatedCustomers.Remove(customer);
      }
      else {
        _deactivatedCustomers.Remove(customer);
      }
      _customers.Remove(customer);
    }

    private void CreateNewCustomer(object obj) {
      IWindowService configureCustomerView = _windowFactory.GetWindowService(WindowType.ConfigureCustomerView);
      CustomerViewModel customer = new CustomerViewModel(new Customer(new CustomerInfo("", "", "", "", "", "", "")));
      ConfigureCustomerViewModel configureCustomer = new ConfigureCustomerViewModel(configureCustomerView.Close, customer);
      configureCustomerView.OpenAsDialog(configureCustomer);
      if (configureCustomer.NeedToBeAdded) {
        customer.Activate();
        AddCustomerToCollection(customer);
      }
    }

    private void ConfigureCustomer(object obj) {
      if (SelectedCustomer != null && SelectedCustomer is CustomerViewModel) {
        IWindowService configureCustomerView = _windowFactory.GetWindowService(WindowType.ConfigureCustomerView);
        ConfigureCustomerViewModel configureCustomer = new ConfigureCustomerViewModel(configureCustomerView.Close, SelectedCustomer as CustomerViewModel);
        configureCustomerView.OpenAsDialog(configureCustomer);
      }
    }

    private void DeleteCustomer(object obj) {
      if (SelectedCustomer != null && SelectedCustomer is CustomerViewModel) {
        RemoveCustomerFromCollection(SelectedCustomer as CustomerViewModel);
      }
    }

    private void ActivateCustomer(object obj) {
      if (SelectedCustomer != null && SelectedCustomer is CustomerViewModel) {
        CustomerViewModel temp = SelectedCustomer as CustomerViewModel;
        temp.Activate();
        _activatedCustomers.Add(temp);
        _deactivatedCustomers.Remove(temp);
      }
    }

    private void DeactivateCustomer(object obj) {
      if (SelectedCustomer != null && SelectedCustomer is CustomerViewModel) {
        CustomerViewModel temp = SelectedCustomer as CustomerViewModel;
        temp.Deactivate();
        _deactivatedCustomers.Add(temp);
        _activatedCustomers.Remove(temp);
      }
    }

    private void ShowActiveCustomers(object obj) {
      if (ShowDeactivatedCustomers) {
        CustomersToShow = new ReadOnlyObservableCollection<CustomerViewModel>(_activatedCustomers);
        ShowDeactivatedCustomers = false;
      }
    }
    private void ShowDeactiveCustomers(object obj) {
      if (!ShowDeactivatedCustomers) {
        CustomersToShow = new ReadOnlyObservableCollection<CustomerViewModel>(_deactivatedCustomers);
        ShowDeactivatedCustomers = true;
      }
    }
  }
}
