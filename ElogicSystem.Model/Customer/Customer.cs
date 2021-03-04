using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Contains information about a customer and provides methods to manipulate the quotation. .
  /// </summary>
  public class Customer {
    public bool IsActive { get; set; }
    public CustomerInfo CustomerInfo { get; set; }
    public List<Quotation> Quotations { get; private set; }

    // CONSTRUCTOR
    public Customer(CustomerInfo customerInfo) {
      Quotations = new List<Quotation>();

      CustomerInfo = customerInfo;
    }

    // METHODS
    /// <summary>
    /// Sets the customer as active
    /// </summary>
    public void Activate() => IsActive = true;

    /// <summary>
    /// Set the customer as inactive
    /// </summary>
    public void Deactivate() => IsActive = false;

    /// <summary>
    /// Adds a quotation to the customer
    /// </summary>
    /// <param name="quotation">Quotation to be added</param>
    public void AddQuotation(Quotation quotation) {
      int index = GetQuotationIndexByID(quotation);

      // If the quotation does not exist, add it to the quotation collection. Otherwise throw exception.
      if (index == -1) {
        Quotations.Add(quotation);
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
      int index = GetQuotationIndexByID(quotation);

      // If the quotation exists, remove it from the collection. Otherwise throw exception.
      if (index != -1) {
        Quotations.Remove(quotation);
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
      CustomerInfo = customerInfo;
    }

    public int GetQuotationIndexByID(Quotation quotationToGet) {
      return Quotations.IndexOf(Quotations.Where(x => x.ID == quotationToGet.ID).FirstOrDefault());
    }
  }
}