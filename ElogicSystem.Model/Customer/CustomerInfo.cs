using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// Represents basic information about a customer.
  /// </summary>
  public class CustomerInfo {
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public string ShippingAddress { get; set; }
    public string ShippingZipCode { get; set; }

    public string BillingAddress { get; set; }
    public string BillingZipCode { get; set; }
  }
}