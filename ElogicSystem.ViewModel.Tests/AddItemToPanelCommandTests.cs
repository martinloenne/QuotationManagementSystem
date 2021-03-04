using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElogicSystem.ViewModel;
using ElogicSystem.Model;
using System.Collections.ObjectModel;

namespace ElogicSystem.ViewModel.Tests {

  [TestClass]
  public class AddItemToPanelCommandTests {
    private PanelCreatorViewModel _viewModel;
    private IWindowService _windowsServiceStub;

    [TestInitialize]
    public void TestInitialize() {
      _windowsServiceStub = new WindowServiceStub();
      Panel panel = new Panel();
      _viewModel = new PanelCreatorViewModel(_windowsServiceStub, panel);
    }

    [TestMethod]
    public void CanExecute_NoItemSelected_ReturnsFalse() {
      // Arrange
      _viewModel.SelectedItemInTemplateGrid = null;

      // Act
      bool actualResult = _viewModel.AddItemToPanelCommand.CanExecute(null);

      // Assert
      Assert.IsFalse(actualResult);
    }

    [TestMethod]
    public void CanExecute_ItemSelected_ReturnsTrue() {
      // Arrange
      _viewModel.SelectedItemInTemplateGrid = new Product { ID = 1, Description = "Test", Quantity = 1, Price = 100, Time = 100 };
      // Act
      bool actualResult = _viewModel.AddItemToPanelCommand.CanExecute(null);

      // Assert
      Assert.IsTrue(actualResult);
    }

    [TestMethod]
    public void Execute_ItemSelectedWithPositiveQuantityNotInPanel_AddsToPanelItems() {
      // Arrange
      Item product = new Product { ID = 1, Description = "Test", Quantity = 1, Price = 100, Time = 100 };
      _viewModel.SelectedItemInTemplateGrid = product;
      Panel panel = new Panel();
      panel.AddItem(product);
      ReadOnlyObservableCollection<Item> expectedItems = panel.Items;

      // Act
      _viewModel.AddItemToPanelCommand.Execute(null);
      ReadOnlyObservableCollection<Item> actualItems = _viewModel.Panel.Items;

      // Assert
      CollectionAssert.AreEqual(expectedItems, actualItems);
    }

    [TestMethod]
    public void Execute_ItemSelectedWithChangedQuantityAlreadyInPanel_AddsQuantityToPanelItems() {
      // ARRANGE
      // Set up test item
      Item product = new Product { ID = 1, Description = "Test", Quantity = 1, Price = 100, Time = 100 };
      // Add item so already exists in panel
      _viewModel.Panel.AddItem(product);
      // Select a product in template
      _viewModel.SelectedItemInTemplateGrid = product;
      // Set new quantity for selected item
      product.Quantity = 2;
      double expectedQuantity = 2;

      // ACT
      // Execute add command
      _viewModel.AddItemToPanelCommand.Execute(null);
      // Set actual to the item within the panel
      double actualQuantity = _viewModel.Panel.Items[0].Quantity;

      // Assert
      Assert.AreEqual(expectedQuantity, actualQuantity);
    }

    [TestMethod]
    public void Execute_ItemSelectedWithQuantityNotInPanel_AddsNewItemToPanelItems() {
      // ARRANGE
      // Set up test item
      Item product = new Product { ID = 1, Description = "Test", Quantity = 0, Price = 100, Time = 100 };
      // Select the product in template
      _viewModel.SelectedItemInTemplateGrid = product;
      // Set new quantity for selected item
      product.Quantity = 1;
      Item expectedItem = product;

      // ACT
      // Execute add command
      _viewModel.AddItemToPanelCommand.Execute(null);
      // Set actual to the item within the panel
      Item actualItem = _viewModel.Panel.Items[0];

      // Assert
      Assert.AreEqual(expectedItem, actualItem);
    }

    [TestMethod]
    public void Execute_ItemSelectedWithChangedQuantityToZeroAlreadyInPanel_RemovesItemFromPanelItems() {
      // ARRANGE
      // Set up test item
      Item product = new Product { ID = 1, Description = "Test", Quantity = 1, Price = 100, Time = 100 };
      // Add item so already exists in panel
      _viewModel.Panel.AddItem(product);
      // Select a product in template
      _viewModel.SelectedItemInTemplateGrid = product;
      // Set quantity for selected item to zero
      product.Quantity = 0;

      // ACT
      // Execute add command
      _viewModel.AddItemToPanelCommand.Execute(null);
      // Set actual to the item within the panel
      bool actual = _viewModel.Panel.Items.Contains(product);

      // ASSERT
      Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Execute_ItemSelectedWithQuantityToZeroNotInPanel_DoesNotAddToPanel() {
      // ARRANGE
      // Set up test item with zero quantity
      Item product = new Product { ID = 1, Description = "Test", Quantity = 0, Price = 100, Time = 100 };
      // Select the item in template
      _viewModel.SelectedItemInTemplateGrid = product;

      // ACT
      // Execute add command
      _viewModel.AddItemToPanelCommand.Execute(null);
      // Set actual to the item within the panel
      bool actual = _viewModel.Panel.Items.Contains(product);

      // ASSERT
      Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Execute_ItemSelectedWithSameQuantityAsInPanel_DoesNotChangeQuantity() {
      // ARRANGE
      // Set up test item with zero quantity
      Item product = new Product { ID = 1, Description = "Test", Quantity = 1, Price = 100, Time = 100 };

      // Add item so already exists in panel
      _viewModel.Panel.AddItem(product);
      // Select the item in template
      _viewModel.SelectedItemInTemplateGrid = product;
      double expectedQuantity = product.Quantity;

      // ACT
      // Execute add command
      _viewModel.AddItemToPanelCommand.Execute(null);
      // Set actual to the item within the panel
      double actualQuantity = _viewModel.Panel.Items[0].Quantity;

      // ASSERT
      Assert.AreEqual(expectedQuantity, actualQuantity);
    }
  }
}