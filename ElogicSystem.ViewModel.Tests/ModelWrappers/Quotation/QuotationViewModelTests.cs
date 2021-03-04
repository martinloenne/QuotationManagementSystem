using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElogicSystem.Model;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.Tests {

  [TestClass]
  public class QuotationViewModelTests {
    private Panel _panel1;
    private Panel _panel2;

    private Quotation _quotation;
    private QuotationStatus _quotationStatus;

    private QuotationViewModel _quotationViewModel;

    [TestInitialize]
    public void TestInitialize() {
      _panel1 = new Panel() {
        ID = 1,
      };

      _panel2 = new Panel() {
        ID = 5,
      };

      _quotationStatus = new QuotationStatus(new DateTime(), QuotationStatusType.Created, "test");

      _quotation = new Quotation(_quotationStatus) {
        ID = 3
      };

      _quotationViewModel = new QuotationViewModel(_quotation);
    }

    [TestMethod]
    public void AddPanelToViewModel() {
      _quotationViewModel.AddPanel(_panel2);

      PanelViewModel panelViewModel = new PanelViewModel(_panel2);

      Assert.IsTrue(_quotationViewModel.PanelViewModels.Contains(panelViewModel));
    }
  }
}