using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.Tests {

  internal class WindowServiceMock : IWindowService {
    public bool CloseCancelDialogCalled { get; set; }
    public bool ClosePanelCreatorFromDialogWindowCalled { get; set; }
    public bool ClosePanelCreatorWindowCalled { get; set; }
    public bool ShowCancelDialogCalled { get; set; }

    public WindowServiceMock() {
      CloseCancelDialogCalled = false;
      ClosePanelCreatorFromDialogWindowCalled = false;
      ClosePanelCreatorWindowCalled = false;
      ShowCancelDialogCalled = false;
    }

    public void CloseCancelDialog() {
      CloseCancelDialogCalled = true;
    }

    public void ClosePanelCreatorFromDialogWindow() {
      ClosePanelCreatorFromDialogWindowCalled = true;
    }

    public void ClosePanelCreatorWindow() {
      ClosePanelCreatorWindowCalled = true;
    }

    public void ShowCancelDialog(PanelCreatorViewModel viewModel) {
      ShowCancelDialogCalled = true;
    }

    public void OpenPanelCreatorWindow(Panel panel) {
      throw new NotImplementedException();
    }
  }
}