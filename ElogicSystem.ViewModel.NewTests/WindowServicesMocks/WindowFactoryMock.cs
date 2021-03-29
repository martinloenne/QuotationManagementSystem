using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel.NewTests {
  public class WindowFactoryMock : IWindowFactory {
    public IWindowService GetWindowService(WindowType windowType) {
      return new WindowServiceMock();
    }
  }
}
