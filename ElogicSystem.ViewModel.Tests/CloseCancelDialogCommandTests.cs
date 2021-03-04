using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.Tests {

  [TestClass]
  public class CloseCancelDialogCommand {
    private PanelBuilderViewModel _viewModel;
    private WindowServiceMock _windowsServiceMock;
    /*
    [TestInitialize]
    public void TestInitialize() {
      _windowsServiceMock = new WindowServiceMock();
      _viewModel = new PanelBuilderViewModel(_windowsServiceMock);
    }

    [TestMethod]
    public void CanExecute_ReturnsTrueAlways() {
      // Act
      bool actualResult = _viewModel.CloseCancelDialogCommand.CanExecute(null);

      // Assert
      Assert.IsTrue(actualResult);
    }

    [TestMethod]
    public void Execute_UsingWindowService_ClosesCancelDialog() {
      // Act
      _viewModel.CloseCancelDialogCommand.Execute(null);
      bool actualResult = _windowsServiceMock.CloseCancelDialogCalled;

      // Assert
      Assert.IsTrue(actualResult);
    }*/
  }
}