using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel.Tests {
  [TestClass]
  public class ClosePanelCreatorFromDialogWindowCommandTests {
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
      bool actualResult = _viewModel.ClosePanelCreatorFromDialogWindowCommand.CanExecute(null);

      // Assert
      Assert.IsTrue(actualResult);
    }

    [TestMethod]
    public void Execute_UsingWindowService_ClosesPanelCreatorWindow() {
      // Act
      _viewModel.ClosePanelCreatorFromDialogWindowCommand.Execute(null);
      bool actualResult = _windowsServiceMock.ClosePanelCreatorFromDialogWindowCalled;

      // Assert
      Assert.IsTrue(actualResult);
    }
  }
}