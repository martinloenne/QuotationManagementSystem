using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class CustomerViewModel : BaseNotify {
    public Customer CustomerModel { get; private set; }

    public int ID {
      get { return CustomerModel.ID; }
    }

    public bool IsActive {
      get { return CustomerModel.IsActive; }
    }

    public CustomerInfoViewModel CustomerInfo { get; private set; }

    public ObservableCollection<QuotationViewModel> Quotations { get; private set; }

    public CustomerViewModel(Customer customerModel) {
      CustomerModel = customerModel;
      CustomerInfo = new CustomerInfoViewModel(CustomerModel.CustomerInfo);
      Quotations = new ObservableCollection<QuotationViewModel>();
    }

    /// <summary>
    /// Sets the customer as active
    /// </summary>
    public void Activate() => CustomerModel.Activate();

    /// <summary>
    /// Set the customer as inactive
    /// </summary>
    public void Deactivate() => CustomerModel.Deactivate();

    public void AddQuotation(Quotation quotation) {
      int index = CustomerModel.GetQuotationIndexByID(quotation);

      // If the quotation does not exist, add it to the quotation collection. Otherwise throw exception.
      if (index == -1) {
        quotation.Customer = CustomerModel;

        QuotationViewModel quotationViewModel = new QuotationViewModel(quotation);
        quotationViewModel.Customer = this;

        Quotations.Add(quotationViewModel);
        CustomerModel.Quotations.Add(quotation);
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
      int index = CustomerModel.GetQuotationIndexByID(quotation);

      // If the quotation exists, remove it from the collection. Otherwise throw exception.
      if (index != -1) {
        Quotations.RemoveAt(index);
        CustomerModel.Quotations.Remove(quotation);
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
      CustomerModel.CustomerInfo = customerInfo;
      CustomerInfo = new CustomerInfoViewModel(customerInfo);
    }
  }
}