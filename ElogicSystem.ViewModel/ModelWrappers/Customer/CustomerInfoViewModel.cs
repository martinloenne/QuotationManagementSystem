using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class CustomerInfoViewModel : BaseNotify {
    private CustomerInfo _customerInfoModel;

    public string Name {
      get { return _customerInfoModel.Name; }
      set { _customerInfoModel.Name = value; }
    }

    public string PhoneNumber {
      get { return _customerInfoModel.PhoneNumber; }
      set { _customerInfoModel.PhoneNumber = value; }
    }

    public string Email {
      get { return _customerInfoModel.Email; }
      set { _customerInfoModel.Email = value; }
    }

    public string ShippingAddress {
      get { return _customerInfoModel.ShippingAddress; }
      set { _customerInfoModel.ShippingAddress = value; }
    }

    public string ShippingZipCode {
      get { return _customerInfoModel.ShippingZipCode; }
      set { _customerInfoModel.ShippingZipCode = value; }
    }

    public string BillingAddress {
      get { return _customerInfoModel.BillingAddress; }
      set { _customerInfoModel.BillingAddress = value; }
    }

    public string BillingZipCode {
      get { return _customerInfoModel.BillingZipCode; }
      set { _customerInfoModel.BillingZipCode = value; }
    }

    public CustomerInfoViewModel() : this(new CustomerInfo("", "", "", "", "", "", "")) { }

    public CustomerInfoViewModel(CustomerInfo customerInfoModel) {
      _customerInfoModel = customerInfoModel;
    }
  }
}