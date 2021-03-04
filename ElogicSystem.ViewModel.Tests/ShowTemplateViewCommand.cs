using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel.Tests {

  [TestClass]
  public class ShowTemplateViewCommand {
    private PanelCreatorViewModel _viewModel;
    private IWindowService _windowServiceStub;

    [TestInitialize]
    public void TestInitialize() {
      _windowServiceStub = new WindowServiceStub();
      _viewModel = new PanelCreatorViewModel(_windowServiceStub);
    }

    [TestMethod]
    public void CanExecute_WhenTemplateGridIsHidden_ReturnsTrue() {
      // ARRANGE
      _viewModel.IsTemplateGridHidden = true;

      // Act
      bool actualResult = _viewModel.ShowTemplateViewCommand.CanExecute(null);

      // Assert
      Assert.IsTrue(actualResult);
    }

    [TestMethod]
    public void CanExecute_WhenTemplateGridIsNotHidden_ReturnsFalse() {
      // ARRANGE
      _viewModel.IsTemplateGridHidden = false;

      // Act
      bool actualResult = _viewModel.ShowTemplateViewCommand.CanExecute(null);

      // Assert
      Assert.IsFalse(actualResult);
    }

    public void Execute_ShowsTemplateGrid() {
      // ARRANGE

      _viewModel.IsTemplateGridHidden = true;
      // ACT
      _viewModel.ShowTemplateViewCommand.Execute(null);
      bool actualResult = _viewModel.IsTemplateGridHidden;
      // ASSERT
      Assert.IsFalse(actualResult);
    }
  }
}