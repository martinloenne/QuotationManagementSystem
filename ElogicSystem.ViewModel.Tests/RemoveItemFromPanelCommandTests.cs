using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel.Tests {

  [TestClass]
  public class RemoveItemFromPanelCommandTests {
    private PanelCreatorViewModel _viewModel;
    private IWindowService _windowsServiceStub;

    [TestInitialize]
    public void TestInitialize() {
      _windowsServiceStub = new WindowServiceStub();
      _viewModel = new PanelCreatorViewModel(_windowsServiceStub);
    }

    [TestMethod]
    public void CanExecute_NoItemSelected_ReturnsFalse() {
      // Arrange
      _viewModel.SelectedItemInPanelGrid = null;

      // Act
      bool actualResult = _viewModel.RemoveItemFromPanelCommand.CanExecute(null);

      // Assert
      Assert.IsFalse(actualResult);
    }

    [TestMethod]
    public void CanExecute_ItemSelected_ReturnsTrue() {
      // Arrange
      Product product = new Product { ID = 1, Description = "Test", Quantity = 1, Price = 100, Time = 100 };
      _viewModel.SelectedItemInPanelGrid = product;
      // Act
      bool actualResult = _viewModel.RemoveItemFromPanelCommand.CanExecute(null);

      // Assert
      Assert.IsTrue(actualResult);
    }

    [TestMethod]
    public void Execute_ItemSelected_RemovesItemFromPanel() {
      // ARRANGE

      // Set up product in panel
      Item product = new Product { ID = 1, Description = "Test", Quantity = 1, Price = 100, Time = 100 };
      _viewModel.Panel.AddItem(product);
      _viewModel.SelectedItemInPanelGrid = product;

      // Set up panel con
      Panel panel = new Panel();
      ReadOnlyObservableCollection<Item> expectedItems = panel.Items;

      // Act
      _viewModel.RemoveItemFromPanelCommand.Execute(null);
      ReadOnlyObservableCollection<Item> actualItems = _viewModel.Panel.Items;

      // Assert
      CollectionAssert.AreEqual(expectedItems, actualItems);
    }
  }
}