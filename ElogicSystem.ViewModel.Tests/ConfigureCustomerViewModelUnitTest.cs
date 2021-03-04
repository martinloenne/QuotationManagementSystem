using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.Tests {

  [TestClass]
  public class ConfigureCustomerViewModelUnitTest {

    [TestMethod]
    public void SaveCommand_SaveChangedToAQuotation_IsTrue() {
      CustomerDataViewModel customer = new CustomerDataViewModel();
      CustomerDataViewModel expected = new CustomerDataViewModel("Has Been Changed", "Has Been Changed", "Has Been Changed", "Has Been Changed", "Has Been Changed");
      ConfigureCustomerViewModel configure = new ConfigureCustomerViewModel(new MockCustomerIWindowFactory(), customer);
      configure.TemperaryCustomer.Address = "Has Been Changed";
      configure.TemperaryCustomer.PhoneNumber = "Has Been Changed";
      configure.TemperaryCustomer.Email = "Has Been Changed";
      configure.TemperaryCustomer.Name = "Has Been Changed";
      configure.TemperaryCustomer.ZipCode = "Has Been Changed";

      configure.SaveCommand.Execute(new object());

      Assert.IsTrue(IsCustomersEquivalent(expected, customer));
    }

    [TestMethod]
    public void Cancel_CancelTheChangedToAQuotation_IsTrue() {
      CustomerDataViewModel customer = new CustomerDataViewModel();
      CustomerDataViewModel expected = new CustomerDataViewModel("Has Been Changed", "Has Been Changed", "Has Been Changed", "Has Been Changed", "Has Been Changed");

      ConfigureCustomerViewModel configure = new ConfigureCustomerViewModel(new MockCustomerIWindowFactory(), customer);
      configure.TemperaryCustomer.Address = "Has Been Changed";
      configure.TemperaryCustomer.PhoneNumber = "Has Been Changed";
      configure.TemperaryCustomer.Email = "Has Been Changed";
      configure.TemperaryCustomer.Name = "Has Been Changed";
      configure.TemperaryCustomer.ZipCode = "Has Been Changed";

      configure.CancelCommand.Execute(new object());

      Assert.IsTrue(!IsCustomersEquivalent(expected, customer));
    }

    private bool IsCustomersEquivalent(CustomerDataViewModel customer1, CustomerDataViewModel customer2) {
      return string.Equals(customer1.Address, customer2.Address) && string.Equals(customer1.Name, customer2.Name) && string.Equals(customer1.Email, customer2.Email) && string.Equals(customer1.PhoneNumber, customer2.PhoneNumber) && string.Equals(customer1.ZipCode, customer2.ZipCode);
    }
  }
}