using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel.Tests {
  [TestClass]
  public class HideTemplateViewCommandTests {
    private PanelCreatorViewModel _viewModel;
    private IWindowService _windowServiceStub;

    [TestInitialize]
    public void TestInitialize() {
      _windowServiceStub = new WindowServiceStub();
      _viewModel = new PanelCreatorViewModel(_windowServiceStub);
    }

    [TestMethod]
    public void CanExecute_WhenTemplateGridIsNotHidden_ReturnsTrueWhenTemplateGridIsNotHidden() {
      // ARRANGE
      _viewModel.IsTemplateGridHidden = false;

      // Act
      bool actualResult = _viewModel.HideTemplateViewCommand.CanExecute(null);

      // Assert
      Assert.IsTrue(actualResult);
    }

    [TestMethod]
    public void CanExecute_WhenTemplateGridIsHidden_ReturnsFalse() {
      // ARRANGE
      _viewModel.IsTemplateGridHidden = true;

      // Act
      bool actualResult = _viewModel.HideTemplateViewCommand.CanExecute(null);

      // Assert
      Assert.IsFalse(actualResult);
    }

    public void Execute_HidesTemplateGrid() {
      // ARRANGE

      _viewModel.IsTemplateGridHidden = false;
      // ACT
      _viewModel.HideTemplateViewCommand.Execute(null);
      bool actualResult = _viewModel.IsTemplateGridHidden;
      // ASSERT
      Assert.IsTrue(actualResult);
    }
  }
}