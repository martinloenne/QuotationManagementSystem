using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElogicSystem.ViewModel.NewTests {

  [TestClass]
  public class DataGridItemTests {
    private DataGridItem _dataGridItemModule;
    private DataGridItem _dataGridItemBlock;

    [TestInitialize]
    public void TestInitialize() {
      ProductViewModel product1 = new ProductViewModel(new Model.Product());
      ProductViewModel product2 = new ProductViewModel(new Model.Product());
      ModuleViewModel module = new ModuleViewModel(new Model.Module());
      module.Add(product1);
      module.Add(product2);

      HierarchyDataGrid hierarchyDataGrid = new HierarchyDataGrid(new ObservableCollection<ItemViewModel>());
      hierarchyDataGrid.Add(module);
      _dataGridItemModule = hierarchyDataGrid.DataGridItems[0];
      hierarchyDataGrid.Remove(module);

      BlockViewModel block = new BlockViewModel(new Model.Block());
      block.Add(module);
      hierarchyDataGrid.Add(block);

      _dataGridItemBlock = hierarchyDataGrid.DataGridItems[0];
    }

    [TestMethod]
    public void ExpandContainerCommand_TestIfAllChildrenInModuleIsShownOnCommand_IsTrue() {
      bool expected = true;
      _dataGridItemModule.ExpandContainerCommand.Execute(null);

      foreach (DataGridItem child in _dataGridItemModule.Children) {
        if (child.Hidden) {
          expected = false;
        }
      }

      Assert.IsTrue(expected);
    }

    [TestMethod]
    public void CollapseContainerCommand_TestIfAllChildrenInModuleIsHiddenOnCommand_IsTrue() {
      bool expected = true;
      _dataGridItemModule.ExpandContainerCommand.Execute(null);

      _dataGridItemModule.CollapseContainerCommand.Execute(null);
      foreach (DataGridItem child in _dataGridItemModule.Children) {
        if (!child.Hidden) {
          expected = false;
        }
      }

      Assert.IsTrue(expected);
    }

    [TestMethod]
    public void ExpandContainerCommand_TestIfAllChildrenInBlockIsShownOnCommand_IsTrue() {
      bool expected = true;
      _dataGridItemBlock.ExpandContainerCommand.Execute(null);

      foreach (DataGridItem child in _dataGridItemBlock.Children) {
        if (child.Hidden) {
          expected = false;
        }
        if (child.ItemViewModel is ContainerViewModel) {
          foreach (DataGridItem grandchild in child.Children) {
            if (child.Hidden) {
              expected = false;
            }
          }
        }
      }

      Assert.IsTrue(expected);
    }

    [TestMethod]
    public void CollapseContainerCommand_TestIfAllChildrenInBlockIsHiddenOnCommand_IsTrue() {
      bool expected = true;
      _dataGridItemBlock.ExpandContainerCommand.Execute(null);

      _dataGridItemBlock.CollapseContainerCommand.Execute(null);
      foreach (DataGridItem child in _dataGridItemBlock.Children) {
        if (!child.Hidden) {
          expected = false;
        }
        if (child.ItemViewModel is ContainerViewModel) {
          foreach (DataGridItem grandchild in child.Children) {
            if (!child.Hidden) {
              expected = false;
            }
          }
        }
      }

      Assert.IsTrue(expected);
    }
  }
}