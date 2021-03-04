using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.Tests {
  public class MockCustomerIWindowFactory : IWindowFactory<ConfigureCustomerViewModel>
  {
    public void CloseWindow()
    {

    }

    public void ShowWindow(ConfigureCustomerViewModel param)
    {
      param.TemperaryCustomer.Address = "Has been changed";
      param.TemperaryCustomer.PhoneNumber = "Has been changed";
      param.TemperaryCustomer.Email = "Has been changed";
      param.TemperaryCustomer.Name = "Has been changed";
      param.TemperaryCustomer.ZipCode = "Has been changed";
      param.SaveCommand.Execute(new object());
    }
  }
}
