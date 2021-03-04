using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ElogicSystem.ViewModel;

namespace ElogicSystem.View {
  public class PageService : IPageService {
    private Page _page;

    public PageService(Page page) {
      _page = page;
    }

    public void SetDataContext(BaseViewModel viewModel) {
      _page.DataContext = viewModel;
    }
  }
}
