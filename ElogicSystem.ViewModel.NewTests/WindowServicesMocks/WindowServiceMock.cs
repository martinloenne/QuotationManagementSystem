using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.NewTests {

  internal class WindowServiceMock : IWindowService {
    public bool OpenWindowCalled { get; set; }
    public bool OpenAsDialogWindowCalled { get; set; }
    public bool CloseWindowCalled { get; set; }

    public WindowServiceMock() {
      OpenWindowCalled = false;
      OpenAsDialogWindowCalled = false;
      CloseWindowCalled = false;
    }

    public void Open(BaseViewModel viewModel) {
      OpenWindowCalled = true;
    }

    public void OpenAsDialog(BaseViewModel viewModel) {
      OpenAsDialogWindowCalled = true;
    }

    public void Close() {
      CloseWindowCalled = true;
    }
  }
}