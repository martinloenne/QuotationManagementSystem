using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public static class ItemViewModelFactory {

    public static ItemViewModel GetItemViewModel(RelationItem relationItem) {
      switch (relationItem.Item) {
        case Product p:
          return new ProductViewModel(relationItem);

        case Module m:
          return new ModuleViewModel(relationItem);

        case Block b:
          return new BlockViewModel(relationItem);

        default:
          throw new ArgumentException("Could determine RelationItem");
      }
    }

    public static ItemViewModel GetItemViewModel(Item item) {
      switch (item) {
        case Product p:
          return new ProductViewModel(item as Product);

        case Module m:
          return new ModuleViewModel(item as Module);

        case Block b:
          return new BlockViewModel(item as Block);

        default:
          return null;
      }
    }
  }
}