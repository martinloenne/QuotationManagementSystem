using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class BlockViewModel : ContainerViewModel {
    private Block _blockModel;

    public BlockViewModel(Block block) : base(block) {
      _blockModel = block;
    }

    // Item add and remove
    public void AddItem(Product product) => AddItem(product, 1);
    public void AddItem(Product product, double quantity) {
      QuantifiableObjectHandler<Item, ItemViewModel>.Add(_blockModel.Items,
                                                         _itemViewModels,
                                                         product,
                                                         new ProductViewModel(product),
                                                         quantity);
    }

    public void RemoveItem(Product product) => RemoveItem(product, 1);
    public void RemoveItem(Product product, double quantity)
    {
      QuantifiableObjectHandler<Item, ItemViewModel>.Remove(_blockModel.Items,
                                                            _itemViewModels,
                                                            product,
                                                            quantity);
    }

    // Module add and remove
    public void AddItem(Module module) => AddItem(module, 1);
    public void AddItem(Module module, double quantity) {
      QuantifiableObjectHandler<Item, ItemViewModel>.Add(_blockModel.Items,
                                                         _itemViewModels,
                                                         module,
                                                         new ModuleViewModel(module),
                                                         quantity);
    }

    public void RemoveItem(Module module) => RemoveItem(module, 1);
    public void RemoveItem(Module module, double quantity)
    {
      QuantifiableObjectHandler<Item, ItemViewModel>.Remove(_blockModel.Items,
                                                            _itemViewModels,
                                                            module,
                                                            quantity);
    }



  }
}