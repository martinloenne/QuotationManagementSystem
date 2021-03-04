using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// The main view model of the system which manages literally everything.
  /// </summary>
  public class MainViewModel : BaseViewModel {

    // Observable collection of quotations
    private ObservableCollection<QuotationViewModel> _quotationData;

    // Observable collection of customers
    private ObservableCollection<CustomerViewModel> _customerData;

    // Observable collection of items
    private ObservableCollection<ItemViewModel> _itemData;

    private IWindowFactory _windowFactory;

    private IPageFactory _pageFactory;

    // Properties

    /// <summary>
    /// Read Only Collection of quotations
    /// </summary>
    public ReadOnlyObservableCollection<QuotationViewModel> QuotationData { get; set; }

    /// <summary>
    /// Read Only Collection of customers
    /// </summary>
    public ReadOnlyObservableCollection<CustomerViewModel> CustomerData { get; set; }

    /// <summary>
    /// Read Only Collection of the items
    /// </summary>
    public ReadOnlyObservableCollection<ItemViewModel> ItemData { get; set; }


    public object QuotationPage { get; set; }
    public object CustomerPage { get; set; }
    public object ItemPage { get; set; }

    /// <summary>
    /// Which of the 3 collections that is selected at time.
    /// </summary>
    public int SelectedTabIndex { get; set; }

    // Constructor
    public MainViewModel(Action onClose, IWindowFactory windowFactory, IPageFactory pageFactory) : base(onClose) {
      SelectedTabIndex = 0;
      // Window services.
      _windowFactory = windowFactory;
      _pageFactory = pageFactory;

      // Private members
      _quotationData = new ObservableCollection<QuotationViewModel>();
      _customerData = new ObservableCollection<CustomerViewModel>();
      _itemData = new ObservableCollection<ItemViewModel>();

      // Public properties
      QuotationData = new ReadOnlyObservableCollection<QuotationViewModel>(_quotationData);
      CustomerData = new ReadOnlyObservableCollection<CustomerViewModel>(_customerData);
      ItemData = new ReadOnlyObservableCollection<ItemViewModel>(_itemData);

      // Setup quotation page view 
      QuotationPage = _pageFactory.GetNewPageInstanceAsObject(PageType.QuotationPageView);
      IPageService pageService = _pageFactory.GetPageService(QuotationPage);
      pageService.SetDataContext(new QuotationPageViewModel(this.OnClose, _quotationData, _customerData, _itemData, _windowFactory));

      // Setup customer page view 
      CustomerPage = _pageFactory.GetNewPageInstanceAsObject(PageType.CustomerPageView);
      pageService = _pageFactory.GetPageService(CustomerPage);
      pageService.SetDataContext(new CustomerPageViewModel(this.OnClose, _windowFactory, _customerData));

      // Setup item page view 
      ItemPage = _pageFactory.GetNewPageInstanceAsObject(PageType.ItemPageView);
      pageService = _pageFactory.GetPageService(ItemPage);
      pageService.SetDataContext(new ItemPageViewModel(this.OnClose, _windowFactory, _itemData));
    }
  }
}