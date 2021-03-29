using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ElogicSystem.DataAccess;

namespace ElogicSystem.ViewModel {

  public class MainViewModel : BaseViewModel {
    public object QuotationPage { get; set; }
    public object CustomerPage { get; set; }
    public object ItemPage { get; set; }
    public object TemplatePage { get; set; }
    public int SelectedTabIndex { set { SetNewTabContext(value); } }

    private IWindowFactory _windowFactory;

    private IPageFactory _pageFactory;

    private IPageService _quotationPageService;
    private IPageService _customerPageService;
    private IPageService _itemsPageService;
    private IPageService _templatePageService;

    private IDisposableViewModel _currentViewModelTab;

    public MainViewModel(Action onClose, IWindowFactory windowFactory, IPageFactory pageFactory) : base(onClose) {
      // Deletes existing database and seeds it with predefined data
      DataSeeder dataSeeder = new DataSeeder();
      dataSeeder.SeedAll();

      // Window services.
      _windowFactory = windowFactory;
      _pageFactory = pageFactory;

      // Setup quotation page view
      QuotationPage = _pageFactory.GetNewPageInstanceAsObject(PageType.QuotationPageView);
      _quotationPageService = _pageFactory.GetPageService(QuotationPage);

      // Setup customer page view
      CustomerPage = _pageFactory.GetNewPageInstanceAsObject(PageType.CustomerPageView);
      _customerPageService = _pageFactory.GetPageService(CustomerPage);

      // Setup item page view
      ItemPage = _pageFactory.GetNewPageInstanceAsObject(PageType.ItemPageView);
      _itemsPageService = _pageFactory.GetPageService(ItemPage);

      // Setup template page view
      TemplatePage = _pageFactory.GetNewPageInstanceAsObject(PageType.TemplatePageView);
      _templatePageService = _pageFactory.GetPageService(TemplatePage);

      SelectedTabIndex = 0;
    }

    private void SetNewTabContext(int value) {
      _currentViewModelTab?.SaveAndDispose();
      switch(value) {
        case 0:
          _currentViewModelTab = new QuotationPageViewModel(this.OnClose, _windowFactory);
          _quotationPageService.SetDataContext(_currentViewModelTab);
          break;

        case 1:
          _currentViewModelTab = new CustomerPageViewModel(this.OnClose, _windowFactory);
          _customerPageService.SetDataContext(_currentViewModelTab);
          break;

        case 2:
          _currentViewModelTab = new ItemPageViewModel(this.OnClose, _windowFactory);
          _itemsPageService.SetDataContext(_currentViewModelTab);
          break;

        case 3:
          _currentViewModelTab = new TemplatePageViewModel(this.OnClose, _windowFactory);
          _templatePageService.SetDataContext(_currentViewModelTab);
          break;

        default:
          break;
      }
    }
  }
}