using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElogicSystem.Model.Tests {
  /// <summary>
  /// Summary description for QuotationTests
  /// </summary>
  [TestClass]
  public class QuotationTests {
    private Quotation _quotation;
    private Panel _panel1;
    private Panel _panel2;
    private Panel _panel3;
    private Product _product1;
    private Product _product2;
    private Product _product3;

    #region TestInitialize

    [TestInitialize]
    public void TestInitialize() {
      DateTime dateTime = new DateTime(2019, 01, 01);
      QuotationStatus quotationStatus = new QuotationStatus(dateTime, QuotationStatusType.Created, "Anders");

      _quotation = new Quotation(quotationStatus);

      _product1 = new Product {
        ID = 1,
        Price = 50
      };
      _product2 = new Product {
        ID = 2,
        Price = 100
      };
      _product3 = new Product {
        ID = 3,
        Price = 250
      };

      _panel1 = new Panel {
        ID = 1
      };
      _panel2 = new Panel {
        ID = 2
      };
      _panel3 = new Panel {
        ID = 3
      };

      _panel1.AddItem(_product1);
      _panel2.AddItem(_product2);
      _panel3.AddItem(_product3);

      _quotation.AddPanel(_panel1);
      _quotation.AddPanel(_panel2);
    }

    #endregion TestInitialize

    #region AddPanel

    [TestMethod]
    public void AddPanel_AddPanelToQuotation_PanelAddedToQuotation() {
      // Arrange
      bool expectedResult = true;
      // Act
      bool actualResult = _quotation.Panels.Contains(_panel1);
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Panel not contained in quotation.");
    }

    public void AddPanel_AddNewQuantityOfPanelsToAlreadyExistingQuotations_CorrectQuantityOfPanelsAddedToQuotation() {
      // Arrange
      _quotation.AddPanel(_panel1, 5);
      double expectedResult = 6;
      double actualResult = _quotation.Panels[0].Quantity;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Panel not contained in quotation.");
    }

    [TestMethod]
    public void AddPanel_AddNewPanelToQuotationWithNegativeQuantity_QuantityExceptionThrown() {
      // Act
      Action action = () => _quotation.AddPanel(_panel1, -10); // Encapsulates a method that has no parameters and does not return a value.
      // Assert
      Assert.ThrowsException<ArgumentException>(action);
    }

    #endregion AddPanel

    #region RemovePanel

    [TestMethod]
    public void RemovePanel_RemovePanelFromQuotation_PanelRemovedFromQuotation() {
      // Arrange
      bool expectedResult = false;
      _quotation.RemovePanel(_panel1);
      // Act
      bool actualResult = _quotation.Panels.Contains(_panel1);
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Panel not removed from quotation");
    }

    [TestMethod]
    public void RemovePanel_RemovesOneQuantityOfPanelWithQuantityMoreOfOne_PanelQuantityDecreased() {
      // Arrange
      _quotation.AddPanel(_panel1, 2);
      _quotation.RemovePanel(_panel1, 1);
      double actualResult = _quotation.Panels[0].Quantity;
      double expectedResult = 2;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Quantity not decreased, panel quantity is: {(double)_quotation.Panels[0].Quantity}");
    }

    #endregion RemovePanel

    #region ChangeStatus

    [TestMethod]
    public void ChangeStatus_ChangeStatusOfQuotation_CurrentStatusChanged() {
      // Arrange
      DateTime date = new DateTime(2019, 02, 02);
      string employeeName = "Jan";
      QuotationStatus newQuotationStatus = new QuotationStatus(date, QuotationStatusType.Confirmed, employeeName);

      QuotationStatus expectedResult = newQuotationStatus;

      _quotation.ChangeStatus(newQuotationStatus);
      // Act
      QuotationStatus actualResult = _quotation.CurrentStatus;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Status has not been updated");
    }

    #endregion ChangeStatus

    #region OverridePriceDifference

    [TestMethod]
    public void OverridePriceDifference_OverridesPriceDifferenceBetweenTotalPriceAndAdjustedPrice_PriceDifferenceOverridden() {
      double newPriceDifference = 100;
      _quotation.OverridePriceDifference(newPriceDifference);
      // Act
      double expectedResult = 100;
      double actualResult = _quotation.PriceDifference;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Does not override the difference, expected: " + expectedResult + "Instead got: " + actualResult);
    }

    #endregion OverridePriceDifference

    #region BasePrice

    [TestMethod]
    public void BasePrice_TwoPanelsAddedToQuotation_BasePriceCorrect() {
      // Arrange
      double expectedResult = _panel1.AdjustedPrice + _panel2.AdjustedPrice + _panel3.AdjustedPrice;
      _quotation.AddPanel(_panel3);
      // Act
      double actualResult = _quotation.BasePrice;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Base price not correct");
    }

    #endregion BasePrice

    #region TotalPrice

    [TestMethod]
    public void TotalPrice_CalculatesTotalPrice_TotalPrice() {
      // Arrange

      _quotation.ShippingCost = 200;
      _quotation.ContributionRatio = 0.3;

      double expectedResult = _quotation.BasePrice * 1.3 + 200;

      // Act
      double actualResult = _quotation.TotalPrice;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Total price not correct");
    }

    #endregion TotalPrice

    #region TotalPriceAdjusted

    [TestMethod]
    public void TotalPriceAdjusted_TotalPriceOverridden_TotalPriceCorrect() {
      // Arrange
      _quotation.OverrideTotalPrice(2000);
      double expectedResult = 2000;
      // Act
      double actualResult = _quotation.TotalPriceAdjusted;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Total adjusted price not correct");
    }

    [TestMethod]
    public void TotalPriceAdjusted_PriceDifferenceOverridden_TotalPriceCorrect() {
      // Arrange
      _quotation.OverridePriceDifference(200);
      double expectedResult = _quotation.BasePrice + 200;
      // Act
      double actualResult = _quotation.TotalPriceAdjusted;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Total adjusted price not correct");
    }

    #endregion TotalPriceAdjusted
  }
}