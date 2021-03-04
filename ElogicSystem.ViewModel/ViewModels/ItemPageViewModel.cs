using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {
  public class ItemPageViewModel : BaseViewModel {
    private ObservableCollection<ItemViewModel> _items;
    private IWindowFactory _windowFactory;

    public ReadOnlyObservableCollection<ItemViewModel> Items { get; set; }

    public object SelectedItem { get; set; }

    public ICommand CreateNewItemCommand { get; set; }
    public ICommand ConfigureItemCommand { get; set; }
    public ICommand DeleteItemCommand { get; set; }

    public ItemPageViewModel(Action onClose, IWindowFactory windowFactory, ObservableCollection<ItemViewModel> items) : base(onClose) {
      _items = items;
      _windowFactory = windowFactory;
      Items = new ReadOnlyObservableCollection<ItemViewModel>(_items);

      CreateNewItemCommand = new RelayCommand(CreateNewCustomer, o => true);
      //ConfigureItemCommand = new RelayCommand(ConfigureCustomer, o => SelectedItem is CustomerViewModel ? true : false);
      //DeleteItemCommand = new RelayCommand(DeleteCustomer, o => SelectedItem is CustomerViewModel ? true : false);
    }

    /// <summary>
    /// Adds a customer to the collection.
    /// </summary>
    /// <param name="customer">The customer to add.</param>
    public void AddCustomerToCollection(ItemViewModel customer) {
      _items.Add(customer);
    }

    public void RemoveCustomerFromCollection(ItemViewModel customer) {
      _items.Remove(customer);
    }

    private void CreateNewCustomer(object obj) {
      IWindowService createItemView = _windowFactory.GetWindowService(WindowType.CreateItemView);
      CreateItemViewModel viewModel = new CreateItemViewModel(createItemView.Close, _items);
      createItemView.OpenAsDialog(viewModel);
      //ItemViewModel customer = new ItemViewModel(new Item(00000, "", 0.0, 0, 0));
      //ConfigureCustomerViewModel configureCustomer = new ConfigureCustomerViewModel(configureCustomerView.Close, customer);
      //configureCustomerView.OpenAsDialog(configureCustomer);
      //if (configureCustomer.NeedToBeAdded) {
      //  AddCustomerToCollection(customer);
      //}
    }

    //private void ConfigureCustomer(object obj) {
    //  if (SelectedCustomer != null && SelectedCustomer is ItemViewModel) {
    //    IWindowService configureCustomerView = _windowFactory.GetWindowService(WindowType.ConfigureCustomerView);
    //    ConfigureCustomerViewModel configureCustomer = new ConfigureCustomerViewModel(configureCustomerView.Close, SelectedCustomer as ItemViewModel);
    //    configureCustomerView.OpenAsDialog(configureCustomer);
    //  }
    //}

    //private void DeleteCustomer(object obj) {
    //  if (SelectedCustomer != null && SelectedCustomer is ItemViewModel) {
    //    RemoveCustomerFromCollection(SelectedCustomer as ItemViewModel);
    //  }
    //}
  }
}
