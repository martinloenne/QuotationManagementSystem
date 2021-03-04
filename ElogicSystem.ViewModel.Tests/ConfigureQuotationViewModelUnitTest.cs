using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.Tests {

  [TestClass]
  public class ConfigureQuotationViewModelUnitTest {

    [TestMethod]
    public void SaveCommand_SaveChangedToAQuotation_IsTrue() {
      QuotationDataViewModel quotation = new QuotationDataViewModel();
      QuotationDataViewModel expected = new QuotationDataViewModel(645, "Has Been Changed", "Has Been Changed", "Has Been Changed", 0, DateTime.Now, new ObservableCollection<Panel>());

      ConfigureQuotationViewModel configure = new ConfigureQuotationViewModel(new MockQuotationIWindowFactory(), quotation);
      configure.TemperaryQuotation.QuotationNumber = 645;
      configure.TemperaryQuotation.Description = "Has Been Changed";
      configure.TemperaryQuotation.Customer = "Has Been Changed";
      configure.TemperaryQuotation.Employee = "Has Been Changed";
      configure.TemperaryQuotation.QuotationStatus = 0;

      configure.SaveCommand.Execute(new object());

      Assert.IsTrue(IsQuotationsEquivalent(expected, quotation));
    }

    [TestMethod]
    public void Cancel_CancelTheChangedToAQuotation_IsTrue() {
      QuotationDataViewModel quotation = new QuotationDataViewModel();
      QuotationDataViewModel expected = new QuotationDataViewModel(645, "Has Been Changed", "Has Been Changed", "Has Been Changed", 0, DateTime.Now, new ObservableCollection<Panel>());

      ConfigureQuotationViewModel configure = new ConfigureQuotationViewModel(new MockQuotationIWindowFactory(), quotation);
      configure.TemperaryQuotation.QuotationNumber = 645;
      configure.TemperaryQuotation.Description = "Has Been Changed";
      configure.TemperaryQuotation.Customer = "Has Been Changed";
      configure.TemperaryQuotation.Employee = "Has Been Changed";
      configure.TemperaryQuotation.QuotationStatus = 0;

      configure.CancelCommand.Execute(new object());

      Assert.IsTrue(!IsQuotationsEquivalent(expected, quotation));
    }

    private bool IsQuotationsEquivalent(QuotationDataViewModel quotation1, QuotationDataViewModel quotation2) {
      return quotation1.QuotationNumber == quotation2.QuotationNumber && string.Equals(quotation1.Description, quotation2.Description) && string.Equals(quotation1.Customer, quotation2.Customer) && string.Equals(quotation1.Employee, quotation2.Employee) && quotation1.QuotationStatus == quotation2.QuotationStatus;
    }
  }
}