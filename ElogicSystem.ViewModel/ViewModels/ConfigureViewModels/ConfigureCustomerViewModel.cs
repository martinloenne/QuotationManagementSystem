using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {

  public class ConfigureCustomerViewModel : BaseViewModel {
    private IWindowFactory _windowFactory;

    // Commands
    /// <summary>
    /// A command to save the configuresion of the item
    /// </summary>
    public ICommand SaveCommand { get; private set; }

    /// <summary>
    /// A command to cancel the configuresion
    /// </summary>
    public ICommand CancelCommand { get; private set; }

    // Properties

    /// <summary>
    /// A temperary customer.
    /// </summary>
    public CustomerViewModel CustomerToConfigure { get; }

    /// <summary>
    /// Property that describs if the item need to be saved in the collection
    /// </summary>
    public bool NeedToBeAdded { get; private set; }

    // Constructor

    /// <summary>
    /// Constructor to initilize a configure viewmodel of a customer
    /// </summary>
    /// <param name="window">A class that implements IWindowFactory</param>
    /// <param name="customer">The customer to configure</param>
    public ConfigureCustomerViewModel(Action onClose, CustomerViewModel customer, IWindowFactory windowFactory) : base(onClose) {
      CustomerToConfigure = customer;
      _windowFactory = windowFactory;
      NeedToBeAdded = false;

      // Commands
      SaveCommand = new RelayCommand(o => SaveChanges(), o => true);
      CancelCommand = new RelayCommand(o => CancelDialog(), o => true);
    }

    /// <summary>
    /// Method called on Command <see cref="SaveCommand"/>.
    /// </summary>
    /// <param name="obj"></param>
    private void SaveChanges() {
      NeedToBeAdded = true;
      OnClose();
    }

    private void CancelDialog() {
      IWindowService cancelDialog = _windowFactory.GetWindowService(WindowType.CancelDialogView);
      cancelDialog.OpenAsDialog(new CancelDialogViewModel(cancelDialog.Close, OnClose));
    }
  }
}