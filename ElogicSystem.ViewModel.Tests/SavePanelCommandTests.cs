using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel.Tests {
  [TestClass]
  public class SavePanelCommandTests {
    private PanelCreatorViewModel _viewModel;
    private WindowServiceMock _windowsServiceMock;

    [TestInitialize]
    public void TestInitialize() {
      _windowsServiceMock = new WindowServiceMock();
      _viewModel = new PanelCreatorViewModel(_windowsServiceMock);
    }

    [TestMethod]
    public void CanExecute_ReturnsTrueAlways() {
      // Act
      bool actualResult = _viewModel.SavePanelCommand.CanExecute(null);

      // Assert
      Assert.IsTrue(actualResult);
    }

    [TestMethod]
    public void Execute_OnSave_ClosesWindowAfterSaving() {
      // Act
      _viewModel.SavePanelCommand.Execute(null);
      bool actualResult = _windowsServiceMock.ClosePanelCreatorWindowCalled;

      // Assert
      Assert.IsTrue(actualResult);
    }
  }
}