using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class ModuleViewModel : ContainerViewModel {
    private Module _moduleModel;

    public ModuleViewModel(Module moduleModel) : base(moduleModel) {
      _moduleModel = moduleModel;
    }

    public void AddItem(Product product) => AddItem(product, 1);

    public void AddItem(Product product, double quantity) {
      QuantifiableObjectHandler<Item, ItemViewModel>.Add(_moduleModel.Items,
                                                         _itemViewModels,
                                                         product,
                                                         new ProductViewModel(product),
                                                         quantity);
    }

    public void RemoveItem(Product product) => RemoveItem(product, 1);

    public void RemoveItem(Product product, double quantity) {
      QuantifiableObjectHandler<Item, ItemViewModel>.Remove(_moduleModel.Items,
                                                         _itemViewModels,
                                                         product,
                                                         quantity);
    }
  }
}