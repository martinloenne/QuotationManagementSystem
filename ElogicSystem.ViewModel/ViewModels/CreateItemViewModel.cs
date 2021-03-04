using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {
  public class CreateItemViewModel : BaseViewModel {
    private ObservableCollection<ItemViewModel> _items;

    private int _selectedTypeIndex;

    public bool CreateProductVisible { get; set; }
    public bool CreateModuleVisible { get; set; }
    public bool CreateBlockVisible { get; set; }

    public ProductViewModel NewProduct { get; set; }
    public ModuleViewModel NewModule { get; set; }
    public BlockViewModel NewBlock { get; set; }

    public int SelectedTypeIndex {
      get { return _selectedTypeIndex; }
      set {
        if (value == 0) {
          CreateProductVisible = true;
          CreateModuleVisible = false;
          CreateBlockVisible = false;
          //NewProduct = new ProductViewModel(new Product(00000, "", 0.0, 0.0, 0.0));
        } else if (value == 1) {
          CreateProductVisible = false;
          CreateModuleVisible = true;
          CreateBlockVisible = false;
          //NewModule = new ModuleViewModel(new Module(00000, "", 0.0, 0.0, 0.0));
        } else if (value == 2) {
          CreateProductVisible = false;
          CreateModuleVisible = false;
          CreateBlockVisible = true;
          //NewBlock = new BlockViewModel(new Block(00000, "", 0.0, 0.0, 0.0));
        }
      }
    }

    public ICommand SaveItemCommand { get; set; }

    public CreateItemViewModel(Action onClose, ObservableCollection<ItemViewModel> items) : base(onClose) {
      _items = items;
      SelectedTypeIndex = 0;
      SaveItemCommand = new RelayCommand(SaveItem, o => true);
    }

    private void SaveItem(object obj) {
      _items.Add(NewProduct);
      OnClose();
    }
  }
}
