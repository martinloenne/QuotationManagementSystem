using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class CustomerViewModel : BaseNotify {
    private Customer _customerModel;
    private ObservableCollection<QuotationViewModel> _quotations;

    public bool IsActive {
      get { return _customerModel.IsActive; }
    }

    public CustomerInfoViewModel CustomerInfo { get; private set; }

    public ReadOnlyObservableCollection<QuotationViewModel> Quotations { get; private set; }

    public CustomerViewModel() {
      _customerModel = new Customer(new CustomerInfo("", "", "","", "", "", ""));
      CustomerInfo = new CustomerInfoViewModel(_customerModel.CustomerInfo);
      _quotations = new ObservableCollection<QuotationViewModel>();
      Quotations = new ReadOnlyObservableCollection<QuotationViewModel>(_quotations);
      Activate();
    }

    public CustomerViewModel(Customer customerModel) {
      _customerModel = customerModel;
      CustomerInfo = new CustomerInfoViewModel(_customerModel.CustomerInfo);
      _quotations = new ObservableCollection<QuotationViewModel>(_customerModel.Quotations.Select(q => new QuotationViewModel(q)));
      Quotations = new ReadOnlyObservableCollection<QuotationViewModel>(_quotations);
    }

    /// <summary>
    /// Sets the customer as active
    /// </summary>
    public void Activate() => _customerModel.IsActive = true;

    /// <summary>
    /// Set the customer as inactive
    /// </summary>
    public void Deactivate() => _customerModel.IsActive = false;

    public void AddQuotation(Quotation quotation) {
      int index = _customerModel.GetQuotationIndexByID(quotation);

      // If the quotation does not exist, add it to the quotation collection. Otherwise throw exception.
      if (index == -1) {
        quotation.Customer = _customerModel;

        QuotationViewModel quotationViewModel = new QuotationViewModel(quotation);
        quotationViewModel.Customer = this;

        _quotations.Add(quotationViewModel);
        _customerModel.Quotations.Add(quotation);
      }
      else {
        throw new InvalidOperationException("Cannot add already existing quotation");
      }
    }

    /// <summary>
    /// Removes a quotation from a customer
    /// </summary>
    /// <param name="quotation">Quotation to be removed</param>
    public void RemoveQuotation(Quotation quotation) {
      int index = _customerModel.GetQuotationIndexByID(quotation);

      // If the quotation exists, remove it from the collection. Otherwise throw exception.
      if (index != -1) {
        _quotations.RemoveAt(index);
        _customerModel.Quotations.Remove(quotation);
      }
      else {
        throw new InvalidOperationException("Quotation not found");
      }
    }

    /// <summary>
    /// Changes the customer's information
    /// </summary>
    /// <param name="customerInfo">The new customer information</param>
    public void ChangeCustomerInfo(CustomerInfo customerInfo) {
      _customerModel.CustomerInfo = customerInfo;
      CustomerInfo = new CustomerInfoViewModel(customerInfo);
    }
  }
}