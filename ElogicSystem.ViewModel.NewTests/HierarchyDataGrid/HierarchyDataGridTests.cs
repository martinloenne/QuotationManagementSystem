using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElogicSystem.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ElogicSystem.ViewModel.NewTests {
  [TestClass]
  public class HierarchyDataGridTests {
    private ModuleViewModel _module;
    private BlockViewModel _block;
    private ObservableCollection<ItemViewModel> _items;
    private HierarchyDataGrid _hierarchyDataGrid;

    [TestInitialize]
    public void TestInitialize() {
      _module = new ModuleViewModel(new Module());
      ProductViewModel itemViewModel1 = new ProductViewModel(new Product() { ID = 1, Description = "TEST", });
      ProductViewModel itemViewModel2 = new ProductViewModel(new Product() { ID = 2, Description = "TEST2" });

      _module.Add(itemViewModel1);
      _module.Add(itemViewModel2);

      _block = new BlockViewModel(new Block());
      _block.Add(_module);
      _items = new ObservableCollection<ItemViewModel>();

      _hierarchyDataGrid = new HierarchyDataGrid(_items);
    }

    [TestMethod]
    public void Add_TestIfModuletAndChildrenAreAllAddedWhenModuleIsAdded_IsTrue() {
      int expected = 3;
      _hierarchyDataGrid.Add(_module);

      Assert.IsTrue(expected == _hierarchyDataGrid.DataGridItems.Count);
    }

    [TestMethod]
    public void Remove_TestIfModuleAndChildrenAreAllRemoveWhenModuleIsRemoved_IsTrue() {
      int expected = 0;
      _hierarchyDataGrid.Add(_module);
      _hierarchyDataGrid.Remove(_module);

      Assert.IsTrue(expected == _hierarchyDataGrid.DataGridItems.Count);
    }

    [TestMethod]
    public void Add_TestIfBlockAndChildrenAreAllAddedWhenBlockIsAdded_IsTrue() {
      int expected = 4;
      _hierarchyDataGrid.Add(_block);

      Assert.IsTrue(expected == _hierarchyDataGrid.DataGridItems.Count);
    }

    [TestMethod]
    public void Remove_TestIfBlockAndChildrenAreAllRemoveWhenBlockIsRemoved_IsTrue() {
      int expected = 0;
      _hierarchyDataGrid.Add(_block);
      _hierarchyDataGrid.Remove(_block);

      Assert.IsTrue(expected == _hierarchyDataGrid.DataGridItems.Count);
    }
  }
}
