using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElogicSystem.Model.Tests {
  [TestClass]
  public class TemplateTests {

    #region Fields

    private Template _template;
    private Product _product1;
    private Product _product2;
    private Product _product3;

    #endregion Fields

    #region TestInitialize

    [TestInitialize]
    public void TestInitialize() {
      // Arrange
      _template = new Template();

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

      _template.AddItem(_product1);
      _template.AddItem(_product2);
    }

    #endregion TestInitialize

    #region AddItem

    // Test for the ability to add products to a template
    [TestMethod]
    public void AddItem_AddNewItem_ItemAddedToList() {
      // Assert
      bool expectedResult = true;
      // Act
      bool actualResult = _template.Items.Contains(_product1);
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Item not contained in list.");
    }

    // Panel already contains product2 at index 1, quantity increased
    [TestMethod]
    public void AddItem_AddNewAlreadyExistingItem_QuantityIncreased() {
      // Arrange
      Quantity expectedResult = new Quantity(2);
      _template.AddItem(_product2);
      // Act
      Quantity actualResult = _template.Items[1].Quantity;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Quantity not incremented, template quantity is: {_template.Items[1].Quantity}");
    }

    // Panel already contains product2 at index 1, quantity increased
    [TestMethod]
    public void AddItem_AddNewQuantityOfAlreadyExistingItem_QuantityIncreased() {
      // Arrange
      double expectedResult = 6;
      _template.AddItem(_product2, 5);
      // Act
      double actualResult = _template.Items[1].Quantity;

      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Quantity not incremented, template quantity is: {_template.Items[1].Quantity}");
    }

    // Panel does not contain product3. Product3 is added with a quantity
    [TestMethod]
    public void AddItem_AddNewQuantityWithNewItem_QuantityIncreased() {
      // Arrange
      double expectedResult = 5;
      _template.AddItem(_product3, 5);
      // Act
      double actualResult = _template.Items[2].Quantity;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Quantity not incremented, template quantity is: {_template.Items[2].Quantity}");
    }

    [TestMethod]
    public void AddItem_AddNewItemWithNegativeQuantity_QuantityExceptionThrown() {
      // Act
      Action action = () => _template.AddItem(_product3, -5); // Encapsulates a method that has no parameters and does not return a value.
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
      _template.AddItem(_product2);
      _template.RemoveItem(_product2);
      // Act
      double actualResult = _template.Items[1].Quantity;
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Quantity not decreased, template quantity is: {(double)_template.Items[1].Quantity}");
    }

    [TestMethod]
    public void RemoveItem_RemoveItemFromList_ItemRemovedFromList() {
      // Arrange
      _template.RemoveItem(_product1);
      // Act
      bool expectedResult = false;
      bool actualResult = _template.Items.Contains(_product1);
      // Assert
      Assert.AreEqual(expectedResult, actualResult, "Item still contained in list.");
    }

    [TestMethod]
    public void RemoveItem_RemoveLargerQuantityOfItemThanItemContains_ItemRemovedFromList() {
      // Arrange
      _template.AddItem(_product2);
      _template.RemoveItem(_product2, 3);
      // Act
      bool expectedResult = false;
      bool actualResult = _template.Items.Contains(_product2);
      // Assert
      Assert.AreEqual(expectedResult, actualResult, $"Item still contained in list.");
    }

    #endregion RemoveItem
  }
}