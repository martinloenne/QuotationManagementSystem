using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.ViewModel;

namespace ElogicSystem.View {
  public class PageFactory : IPageFactory {
    public object GetNewPageInstanceAsObject(PageType pageType) {
      switch (pageType) {
        case PageType.QuotationPageView:
          return new QuotationPageView();

        case PageType.CustomerPageView:
          return new CustomerPageView();

        case PageType.ItemPageView:
          return new ItemPageView();

        case PageType.TemplatePageView:
          return new TemplatePageView();

        default:
          throw new ArgumentException("The page type could not be found.");
      }
    }

    public IPageService GetPageService(object page) {
      if (page is QuotationPageView) {
        return new PageService(page as QuotationPageView);
      }
      else if (page is CustomerPageView) {
        return new PageService(page as CustomerPageView);
      }
      else if (page is ItemPageView) {
        return new PageService(page as ItemPageView);
      }
      else if (page is TemplatePageView) {
        return new PageService(page as TemplatePageView);
      }
      else {
        throw new ArgumentException("object is not a page");
      }
    }
  }
}
