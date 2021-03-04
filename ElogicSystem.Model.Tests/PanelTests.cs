using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElogicSystem.Model.Tests {

  [TestClass]
  public class PanelTests {

    #region Fields

    private Panel _panel;
    private Product _product1;
    private Product _product2;
    private Product _product3;

    #endregion Fields

    #region TestInitalize

    [TestInitialize]
    public void TestInitialize() {
      // Arrange
      _panel = new Panel();

      _product1 = new Product {
        ID = 1,
        Price = 50,
        Time = 20
      };

      _product2 = new Product {
        ID = 2,
        Price = 100,
        Time = 500
      };

      _product3 = new Product {
        ID = 3,
        Price = 250,
        Time = 150
      };

      _panel.OverridePrice(200);

      _panel.AddItem(_product1);
      _panel.AddItem(_product2);
    }

    #endregion TestInitalize

    #region ContainsItem

    [TestMethod]
    public void ContainsItem_CheckIfContainsProduct1_ReturnsTrue() {
      // Act
      bool actualResult = _panel.Contains(_product1);
      // Assert
      Assert.IsTrue(actualResult);
    }

    [TestMethod]
    public void ContainsItem_CheckIfContainsProduct3_ReturnsFalse() {
      // Act
      bool actualResult = _panel.Contains(_product3);
      // Assert
      Assert.IsFalse(actualResult);
    }

    #endregion ContainsItem

    #region AddItem

    // Test for the ability to add products to panel
    [TestMethod]
    public void AddItem_AddNewItem_ItemAddedToList() {
      // Assert
      bool expectedResult = true;
      // Act
      bool actualResult = _panel.Items.Contains(_product1);
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Item not contained in list.");
    }

    // Panel already contains product2 at index 1, quantity increased
    [TestMethod]
    public void AddItem_AddNewAlreadyExistingItem_QuantityIncreased() {
      // Arrange
      Quantity expectedResult = new Quantity(2);
      _panel.AddItem(_product2);
      // Act
      Quantity actualResult = _panel.Items[1].Quantity;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Quantity not incremented, panel quantity is: {_panel.Items[1].Quantity}");
    }

    // Panel already contains product2 at index 1, quantity increased
    [TestMethod]
    public void AddItem_AddNewQuantityOfAlreadyExistingItem_QuantityIncreased() {
      // Arrange
      double expectedResult = 6;
      _panel.AddItem(_product2, 5);
      // Act
      double actualResult = _panel.Items[1].Quantity;

      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Quantity not incremented, panel quantity is: {(double)_panel.Items[1].Quantity}");
    }

    // Panel does not contain product3. Product3 is added with a quantity
    [TestMethod]
    public void AddItem_AddNewQuantityWithNewItem_QuantityIncreased() {
      // Arrange
      double expectedResult = 5;
      _panel.AddItem(_product3, 5);
      // Act
      double actualResult = _panel.Items[2].Quantity;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Quantity not incremented, panel quantity is: {(double)_panel.Items[2].Quantity}");
    }

    [TestMethod]
    public void AddItem_AddNewItemWithNegativeQuantity_QuantityExceptionThrown() {
      // Act
      Action action = () => _panel.AddItem(_product3, -5); // Encapsulates a method that has no parameters and does not return a value.
      // Assert
      Assert.ThrowsException<ArgumentException>(action);
    }

    #endregion AddItem

    #region RemoveItem

    // Panel already contains product2 at index 1
    [TestMethod]
    public void RemoveItem_RemovesOneQuantityOfItemWithQuantityOfMoreThanOne_QuantityDecreased() {
      // Arrange
      double expectedResult = 1;
      _panel.AddItem(_product2);
      _panel.RemoveItem(_product2);
      // Act
      double actualResult = _panel.Items[1].Quantity;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Quantity not decreased, panel quantity is: {(double)_panel.Items[1].Quantity}");
    }

    [TestMethod]
    public void RemoveItem_RemoveItemFromList_ItemRemovedFromList() {
      // Arrange
      _panel.RemoveItem(_product1);
      // Act
      bool expectedResult = false;
      bool actualResult = _panel.Items.Contains(_product1);
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Item still contained in list.");
    }

    [TestMethod]
    public void RemoveItem_RemoveLargerQuantityOfItemThanItemContains_ItemRemovedFromList() {
      // Arrange
      _panel.AddItem(_product2);
      _panel.RemoveItem(_product2, 3);
      // Act
      bool expectedResult = false;
      bool actualResult = _panel.Items.Contains(_product2);
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Item still contained in list.");
    }

    #endregion RemoveItem

    #region CalculateBasePrice

    [TestMethod]
    public void CalculateBasePrice_ContainsListOfPricedProducts_CorrectBasePriceCalculated() {
      // Assert
      double expectedResult = _product1.Price + _product2.Price;
      double actualResult = _panel.BasePrice;
      Assert.AreEqual(expectedResult, actualResult, "Prices are not equal.");
    }

    #endregion CalculateBasePrice

    #region CalculateTime

    [TestMethod]
    public void CalculateTime_ContainsListOfTimes_CorrectTotalTimeCalculated() {
      // Assert
      double expectedResult = _product1.Time + _product2.Time;
      double actualResult = _panel.Time;
      Assert.AreEqual(expectedResult, actualResult, "Time are not equal.");
    }

    #endregion CalculateTime

    #region PriceAdjustment

    [TestMethod]
    public void PriceAdjustment_ContainsBasePriceAndTotalPrice_CorrectAdjustedPrice() {
      double expectedResult = _panel.AdjustedPrice - _panel.BasePrice;
      double actualResult = _panel.PriceDifference;
      Assert.AreEqual(expectedResult, actualResult, "Prices are not equal.");
    }

    #endregion PriceAdjustment

    #region AdjustedPrice

    [TestMethod]
    public void AdjustedPrice_ChangedPriceAfterItemAdded_CorrectAdjustedPrice() {
      // Assert
      double expectedResult = _panel.AdjustedPrice + _product3.Price;
      _panel.AddItem(_product3);
      // Act
      double actualResult = _panel.AdjustedPrice;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "The price has not been adjusted correctly. ");
    }

    [TestMethod]
    public void AdjustedPrice_AdjustedPriceAfterOverrideThenItemAdded_CorrectAdjustedPrice() {
      // Assert
      double expectedResult = 1000 + _product2.Price;
      // Act
      _panel.OverridePrice(1000);
      _panel.AddItem(_product2);

      double actualResult = _panel.AdjustedPrice;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"The price has not been adjusted correctly. P2 quantity: {_product1.Quantity} Base price: {_panel.BasePrice} Price difference: {_panel.PriceDifference}");
    }

    #endregion AdjustedPrice

    #region OverridePrice

    [TestMethod]
    public void OverridePrice_PriceOverriden_CorrectAdjustedPrice() {
      // Arrange
      double expectedResult = 1000;
      _panel.OverridePrice(1000);
      // Act
      double actualResult = _panel.AdjustedPrice;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Price not overriden.");
    }

    #endregion OverridePrice

    #region OverridePriceDifference

    [TestMethod]
    public void OverridePriceDifference_SetNewPriceDifference_PriceDifferenceSuccessfullyOverriden() {
      // Arrange
      double expectedResult = 100;
      _panel.OverridePriceDifference(100);
      // Act
      double actualResult = _panel.PriceDifference;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Price difference not overriden.");
    }

    #endregion OverridePriceDifference
  }
}