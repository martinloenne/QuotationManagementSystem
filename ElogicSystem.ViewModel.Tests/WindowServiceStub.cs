using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.ViewModel;

namespace ElogicSystem.ViewModel.Tests {

  internal class WindowServiceStub : IWindowService {

    public void CloseCancelDialog() {
      throw new NotImplementedException();
    }

    public void ClosePanelCreatorFromDialogWindow() {
      throw new NotImplementedException();
    }

    public void ClosePanelCreatorWindow() {
      throw new NotImplementedException();
    }

    public void ShowCancelDialog(PanelCreatorViewModel viewModel) {
      throw new NotImplementedException();
    }
  }
}