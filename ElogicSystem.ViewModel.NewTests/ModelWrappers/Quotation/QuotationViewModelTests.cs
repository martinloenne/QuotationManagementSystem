using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElogicSystem.Model;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.NewTests {

  [TestClass]
  public class QuotationViewModelTests {
    private PanelViewModel _panel1;
    private PanelViewModel _panel2;

    private Quotation _quotation;
    private QuotationStatus _quotationStatus;

    private QuotationViewModel _quotationViewModel;

    [TestInitialize]
    public void TestInitialize() {
      _panel1 = new PanelViewModel(new Panel()) {
        Price = 5,
        Time = 5,
        TotalPrice = 5,
      };

      _panel2 = new PanelViewModel(new Panel()) {
      };

      _quotationStatus = new QuotationStatus(new DateTime(), QuotationStatusType.Created);

      _quotation = new Quotation(_quotationStatus) {
        PriceDifference = 50,
        HourPrice = 200,
        BasePrice = 50,
        ContributionRatio = 0.5,
      };

      _quotationViewModel = new QuotationViewModel(_quotation);
    }

    [TestMethod]
    public void AddPanelToViewModel_AddsAPanelToQuotation_IsTrue() {
      _quotationViewModel.AddPanel(_panel2);

      Assert.IsTrue(_quotationViewModel.PanelViewModels[0].ID == _panel2.ID);
    }

    [TestMethod]
    public void AddPanelToViewModel_TestsIfListsIsSynchronized_AreEqual() {
      _quotationViewModel.AddPanel(_panel2);

      int index1 = _quotationViewModel.QuotationModel.Panels.Count - 1;
      int index2 = _quotationViewModel.PanelViewModels.Count - 1;
      Assert.AreEqual(_quotationViewModel.PanelViewModels[index2].PanelModel, _quotationViewModel.QuotationModel.Panels[index1]);
    }

    [TestMethod]
    public void RemovePanelFromViewModel_ChecksIfAPanelStillInTheQuotation_AreNotEqual() {
      _quotationViewModel.AddPanel(_panel2);
      _quotationViewModel.AddPanel(_panel1);
      _quotationViewModel.RemovePanel(_panel2);

      Assert.AreNotEqual(_quotationViewModel.PanelViewModels[0], _panel2);
    }

    [TestMethod]
    public void ChangeStatus_TestIfTheStatusChanges_IsTrue() {
      _quotationViewModel.ChangeStatus(new QuotationStatus(DateTime.Now, QuotationStatusType.Confirmed));

      Assert.IsTrue(_quotationViewModel.CurrentStatus.Type == QuotationStatusType.Confirmed);
    }

    [TestMethod]
    public void CalculateQuotationInfo_OverrideThePriceDifference_IsTrue() {
      _quotationViewModel.HourPrice = 50;
      _quotationViewModel.ContributionRatio = 0.2;
      _quotationViewModel.PriceDifference = 10;
      _quotationViewModel.ActualPrice = 10;

      _quotationViewModel.AddPanel(_panel1);
      _quotationViewModel.CalculateQuotationInfo();

      bool expected = _quotation.BasePrice == 5 && _quotationViewModel.Time == 5 && _quotationViewModel.TotalHourPrice == 250 &&
                      _quotationViewModel.TotalPrice == 256 && _quotationViewModel.TotalPriceAdjusted == 266 && _quotationViewModel.ActualPriceDeviation == -256;
      Assert.IsTrue(expected);
    }

    [TestMethod]
    public void ShippingCost_TestIfItCalculatesTheShippingCorrectly_AreEqual() {
      double expected = 270;
      _quotationViewModel.ContributionRatio = 0.2;
      _panel1.Quantity = 1;
      _quotationViewModel.AddPanel(_panel1);
      _quotationViewModel.AddPanel(_panel1);
      _quotationViewModel.AddPanel(_panel1);
      _quotationViewModel.CalculateQuotationInfo();

      Assert.AreEqual(expected, _quotationViewModel.ShippingCost);
    }
  }
}