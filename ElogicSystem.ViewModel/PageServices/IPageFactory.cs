using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {
  public interface IPageFactory {
    object GetNewPageInstanceAsObject(PageType pageType);

    IPageService GetPageService(object page);
  }
}
