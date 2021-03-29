using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class CustomerInfoViewModel : BaseNotify, IDataErrorInfo {
    public CustomerInfo CustomerInfoModel { get; }

    public string Name {
      get { return CustomerInfoModel.Name; }
      set { CustomerInfoModel.Name = value; }
    }

    public string PhoneNumber {
      get { return CustomerInfoModel.PhoneNumber; }
      set { CustomerInfoModel.PhoneNumber = value; }
    }

    public string Email {
      get { return CustomerInfoModel.Email; }
      set { CustomerInfoModel.Email = value; }
    }

    public string ShippingAddress {
      get { return CustomerInfoModel.ShippingAddress; }
      set { CustomerInfoModel.ShippingAddress = value; }
    }

    public string ShippingZipCode {
      get { return CustomerInfoModel.ShippingZipCode; }
      set { CustomerInfoModel.ShippingZipCode = value; }
    }

    public string BillingAddress {
      get { return CustomerInfoModel.BillingAddress; }
      set { CustomerInfoModel.BillingAddress = value; }
    }

    public string BillingZipCode {
      get { return CustomerInfoModel.BillingZipCode; }
      set { CustomerInfoModel.BillingZipCode = value; }
    }

    public CustomerInfoViewModel(CustomerInfo customerInfoModel) {
      CustomerInfoModel = customerInfoModel;
    }

    public string Error { get; set; }

    public string this[string propertyName] {
      get {
        switch (propertyName) {
          case nameof(Email):
            if (!string.IsNullOrEmpty(Email) && !Validate.Email(Email)) {
              return "Email should contain an @-sign.";
            }
            else {
              return null;
            }
          default:
            throw new Exception("Property not found.");
        }
      }
    }
  }
}