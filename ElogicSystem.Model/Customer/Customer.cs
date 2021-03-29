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
    public int ID { get; set; }
    public bool IsActive { get; private set; }

    public virtual CustomerInfo CustomerInfo { get; set; }
    public virtual List<Quotation> Quotations { get; set; }

    /// <summary>
    /// Empty constructor used by EF Core for database calls.
    /// </summary>
    public Customer() {
      IsActive = true;
      CustomerInfo = new CustomerInfo();
      Quotations = new List<Quotation>();
    }

    /// <summary>
    /// Sets the customer as active.
    /// </summary>
    public void Activate() => IsActive = true;

    /// <summary>
    /// Set the customer as inactive.
    /// </summary>
    public void Deactivate() => IsActive = false;

    public int GetQuotationIndexByID(Quotation quotationToGet) {
      return Quotations.IndexOf(Quotations.Where(x => x.ID == quotationToGet.ID).FirstOrDefault());
    }
  }
}