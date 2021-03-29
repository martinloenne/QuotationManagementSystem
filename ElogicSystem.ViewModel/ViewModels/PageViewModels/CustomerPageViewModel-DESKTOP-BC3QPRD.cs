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

  public class CustomerPageViewModel : BaseViewModel, IDisposableViewModel {
    private ObservableCollection<CustomerViewModel> _allCustomers;
    private ObservableCollection<CustomerViewModel> _activatedCustomers;
    private ObservableCollection<CustomerViewModel> _deactivatedCustomers;
    private IWindowFactory _windowFactory;

    public ObservableCollection<CustomerViewModel> CustomersToShow { get; set; }

    public CustomerViewModel SelectedCustomer { get; set; }
    public bool ShowDeactivatedCustomers { get; set; }

    public ICommand CreateNewCustomerCommand { get; set; }
    public ICommand ConfigureCustomerCommand { get; set; }
    public ICommand DeleteCustomerCommand { get; set; }
    public ICommand ActivateCustomerCommand { get; set; }
    public ICommand DeactivateCustomerCommand { get; set; }
    public ICommand ShowActivedCustomersCommand { get; set; }
    public ICommand ShowDeactivedCustomersCommand { get; set; }

    private UnitOfWork _unitOfWork;

    public CustomerPageViewModel(Action onClose, IWindowFactory windowFactory) : base(onClose) {
      LoadDB();

      _windowFactory = windowFactory;

      CreateNewCustomerCommand = new RelayCommand(o => CreateNewCustomer(), o => true);
      ConfigureCustomerCommand = new RelayCommand(o => ConfigureCustomer(), o => SelectedCustomer is CustomerViewModel ? true : false);
      DeleteCustomerCommand = new RelayCommand(o => DeleteCustomer(), o => SelectedCustomer is CustomerViewModel ? true : false);
      ActivateCustomerCommand = new RelayCommand(o => ActivateCustomer(), o => SelectedCustomer is CustomerViewModel ? true : false);
      DeactivateCustomerCommand = new RelayCommand(o => DeactivateCustomer(), o => SelectedCustomer is CustomerViewModel ? true : false);
      ShowActivedCustomersCommand = new RelayCommand(o => ShowActiveCustomers(), o => true);
      ShowDeactivedCustomersCommand = new RelayCommand(o => ShowDeactiveCustomers(), o => true);
    }

    /// <summary>
    /// Adds a customer to the collection.
    /// </summary>
    /// <param name="customer">The customer to add.</param>
    public void AddCustomerToCollection(CustomerViewModel customer) {
      if (customer.IsActive) {
        _activatedCustomers.Add(customer);
      }
      else {
        _deactivatedCustomers.Add(customer);
      }
      _allCustomers.Add(customer);
      _unitOfWork.Save();
    }

    public void RemoveCustomerFromCollection(CustomerViewModel customer) {
      if (customer.IsActive) {
        _activatedCustomers.Remove(customer);
      }
      else {
        _deactivatedCustomers.Remove(customer);
      }
      _allCustomers.Remove(customer);
      _unitOfWork.Save();
    }

    private void CreateNewCustomer() {
      IWindowService configureCustomerView = _windowFactory.GetWindowService(WindowType.ConfigureCustomerView);

      CustomerViewModel customer = new CustomerViewModel(new Customer());
      _unitOfWork.CustomerRepository.Add(customer.CustomerModel);
      _unitOfWork.Save();
      AddCustomerToCollection(customer);

      ConfigureCustomerViewModel configureCustomer = new ConfigureCustomerViewModel(configureCustomerView.Close, customer, _windowFactory);
      configureCustomerView.OpenAsDialog(configureCustomer);

      if (!configureCustomer.NeedToBeAdded) {
        RemoveCustomerFromCollection(customer);
        _unitOfWork.CustomerRepository.Delete(customer.CustomerModel);
        _unitOfWork.Save();
      }
    }

    private void ConfigureCustomer() {
      if (SelectedCustomer != null) {
        IWindowService configureCustomerView = _windowFactory.GetWindowService(WindowType.ConfigureCustomerView);

        ConfigureCustomerViewModel configureCustomer = new ConfigureCustomerViewModel(configureCustomerView.Close, SelectedCustomer, _windowFactory);
        configureCustomerView.OpenAsDialog(configureCustomer);

        if (configureCustomer.NeedToBeAdded) {
          _unitOfWork.Save();
        }
        else {
          _unitOfWork.Dispose();
          LoadDB();
        }
      }
    }

    private void DeleteCustomer() {
      if (SelectedCustomer != null) {
        _unitOfWork.CustomerRepository.Delete(SelectedCustomer.CustomerModel);
        RemoveCustomerFromCollection(SelectedCustomer);
      }
    }

    private void ActivateCustomer() {
      if (SelectedCustomer != null) {
        SelectedCustomer.Activate();
        _activatedCustomers.Add(SelectedCustomer);
        _deactivatedCustomers.Remove(SelectedCustomer);
      }
    }

    private void DeactivateCustomer() {
      if (SelectedCustomer != null) {
        SelectedCustomer.Deactivate();
        _deactivatedCustomers.Add(SelectedCustomer);
        _activatedCustomers.Remove(SelectedCustomer);
      }
    }

    private void ShowActiveCustomers() {
      if (ShowDeactivatedCustomers) {
        CustomersToShow = new ObservableCollection<CustomerViewModel>(_activatedCustomers);
        ShowDeactivatedCustomers = false;
      }
    }

    private void ShowDeactiveCustomers() {
      if (!ShowDeactivatedCustomers) {
        CustomersToShow = new ObservableCollection<CustomerViewModel>(_deactivatedCustomers);
        ShowDeactivatedCustomers = true;
      }
    }

    private void LoadDB() {
      _unitOfWork = new UnitOfWork(new ElogicSystemContext());

      List<Customer> customerModels = _unitOfWork.CustomerRepository.GetAll().ToList();
      _allCustomers = new ObservableCollection<CustomerViewModel>(customerModels.Select(q => new CustomerViewModel(q)));

      _activatedCustomers = new ObservableCollection<CustomerViewModel>(_allCustomers.Where(x => x.IsActive == true).ToList());
      _deactivatedCustomers = new ObservableCollection<CustomerViewModel>(_allCustomers.Where(x => x.IsActive == false).ToList());

      CustomersToShow = new ObservableCollection<CustomerViewModel>(_activatedCustomers);
    }

    public void Dispose() {
      _unitOfWork?.Dispose();
    }
  }
}