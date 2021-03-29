using System;
using ElogicSystem.DataAccess;
using ElogicSystem.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElogicSystem.ViewModel.NewTests {
  [TestClass]
  public class BuilderViewModelTests {
    private TemplateViewModel _template;
    private ModuleViewModel _module;
    private IWindowFactory _windowFactory;
    private IWindowService _windowService;
    private BuilderViewModel _builderViewModel;
    [TestInitialize]
    public void TestInitialize() {
      _template = new TemplateViewModel(new Template());
      _module = new ModuleViewModel(new Module());
      UnitOfWork unitOfWork = new UnitOfWork(new ElogicSystemContext());
      ProductViewModel itemViewModel1 = new ProductViewModel(new Product() { ID = 1, Description = "TEST", });
      ProductViewModel itemViewModel2 = new ProductViewModel(new Product() { ID = 2, Description = "TEST2" });
      ProductViewModel itemViewModel3 = new ProductViewModel(new Product() { ID = 3, Description = "TEST2" });
      ModuleViewModel module1 = new ModuleViewModel(new Module());
      module1.Add(itemViewModel3);

      _template.Add(itemViewModel1);
      _template.Add(itemViewModel2);
      _template.Add(module1);
      _windowFactory = new WindowFactoryMock();
      _windowService = _windowFactory.GetWindowService(WindowType.PanelBuilderView);
      _builderViewModel = new BuilderViewModel(_windowService.Close, _windowFactory, _template, _module, unitOfWork);

    }

    [TestMethod]
    public void AddItemToTargetByButtonCommand_TestsIfSelectedItemAddedToTargetList_AreEqual() {
      DataGridItem expected = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[0];
      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[0];

      _builderViewModel.AddItemToTargetByButtonCommand.Execute(null);

      int index = _builderViewModel.TargetHierarchyDataGrid.DataGridItems.Count - 1;
      Assert.AreEqual(expected.ItemViewModel, _builderViewModel.TargetHierarchyDataGrid.DataGridItems[index].ItemViewModel);
    }

    [TestMethod]
    public void AddItemToTargetByButtonCommand_TestsIfSelectedItemAddedToTargetIfLevelIsNotZero_AreNotEqual() {
      DataGridItem expected = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[3];
      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[0];
      _builderViewModel.AddItemToTargetByButtonCommand.Execute(null);

      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[3];
      _builderViewModel.AddItemToTargetByButtonCommand.Execute(null);

      int index = _builderViewModel.TargetHierarchyDataGrid.DataGridItems.Count - 1;
      Assert.AreNotEqual(expected.ItemViewModel, _builderViewModel.TargetHierarchyDataGrid.DataGridItems[index].ItemViewModel);
    }

    [TestMethod]
    public void AddItemToTargetByButtonCommand_TestIfCommandCanExecute_IsTrue() {
      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[0];

      bool result = _builderViewModel.AddItemToTargetByButtonCommand.CanExecute(null);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void AddItemToTargetByButtonCommand_TestIfCommandCanExecute_IsFalse() {
      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[3];

      bool result = _builderViewModel.AddItemToTargetByButtonCommand.CanExecute(null);

      Assert.IsFalse(result);
    }

    [TestMethod]
    public void RemoveItemFromTargetByButtonCommand_TestsIfSelectedItemRemovedfromTargetList_AreNotEqual() {
      DataGridItem expected = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[0];
      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[0];
      _builderViewModel.AddItemToTargetByButtonCommand.Execute(null);

      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[1];
      _builderViewModel.AddItemToTargetByButtonCommand.Execute(null);

      _builderViewModel.SelectedItemInTarget = _builderViewModel.TargetHierarchyDataGrid.DataGridItems[1];
      _builderViewModel.RemoveItemFromTargetByButtonCommand.Execute(null);

      int index = _builderViewModel.TargetHierarchyDataGrid.DataGridItems.Count - 1;
      Assert.AreEqual(expected.ItemViewModel, _builderViewModel.TargetHierarchyDataGrid.DataGridItems[index].ItemViewModel);
    }

    [TestMethod]
    public void RemoveItemFromTargetByButtonCommand_TestsIfSelectedItemIsRemovedToTargetIfLevelIsNotZero_AreNotEqual() {
      DataGridItem expected = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[2];
      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[1];
      _builderViewModel.AddItemToTargetByButtonCommand.Execute(null);

      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[2];
      _builderViewModel.AddItemToTargetByButtonCommand.Execute(null);

      _builderViewModel.SelectedItemInTarget = _builderViewModel.TargetHierarchyDataGrid.DataGridItems[2];
      _builderViewModel.RemoveItemFromTargetByButtonCommand.Execute(null);

      int index = _builderViewModel.TargetHierarchyDataGrid.DataGridItems.Count - 1;
      Assert.AreNotEqual(expected.ItemViewModel, _builderViewModel.TargetHierarchyDataGrid.DataGridItems[index].ItemViewModel);
    }

    [TestMethod]
    public void RemoveItemFromTargetByButtonCommand_TestIfCommandCanExecute_IsTrue() {
      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[0];
      _builderViewModel.AddItemToTargetByButtonCommand.Execute(null);

      bool result = _builderViewModel.RemoveItemFromTargetByButtonCommand.CanExecute(null);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void RemoveItemFromTargetByButtonCommand_TestIfCommandCanExecute_IsFalse() {
      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[2];
      _builderViewModel.AddItemToTargetByButtonCommand.Execute(null);

      _builderViewModel.SelectedItemInSource = _builderViewModel.SourceHierarchyDataGrid.DataGridItems[3];

      bool result = _builderViewModel.RemoveItemFromTargetByButtonCommand.CanExecute(null);

      Assert.IsFalse(result);
    }
  }
}
