using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

namespace ElogicSystem.Model.Tests {
  [TestClass]
  public class QuantifiableObjectHandlerTests {
    private Panel _panel;
    private Product _product1;

    [TestInitialize]
    public void TestInitialize() {
      // Arrange
      _panel = new Panel();
      _product1 = new Product {
        Price = 50,
        Time = 20
      };
    }

    /*[TestMethod]
    public void Add_AddNewItem_() {
      ObservableCollection<IQuantifiable<Item>> _items = new ObservableCollection<IQuantifiable<Item>>();
      _panel = new Panel();
      double qty = 5;

      // Assert
      //bool expectedResult = true;
      // Act
      //bool actualResult = _panel.Items.Contains(_product1);
      // Assert
      //Assert.AreEqual(expectedResult, actualResult, "Item not contained in list.");
    }*/
  }
}