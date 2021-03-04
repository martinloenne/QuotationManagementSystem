using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.Tests {
  public class MockQuotationIWindowFactory : IWindowFactory<ConfigureQuotationViewModel>
  {
    public void CloseWindow()
    {
      
    }

    public void ShowWindow(ConfigureQuotationViewModel param)
    {
      param.TemperaryQuotation.Customer = "Has been changed";
      param.TemperaryQuotation.Description = "Has been changed";
      param.TemperaryQuotation.Employee = "Has been changed";
      param.SaveCommand.Execute(new object());
    }
  }
}
