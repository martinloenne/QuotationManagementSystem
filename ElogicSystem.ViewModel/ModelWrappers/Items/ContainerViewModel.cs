using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class ContainerViewModel : ItemViewModel {
    private Container _containerModel;
    protected ObservableCollection<ItemViewModel> _itemViewModels;

    public override double Price {
      get { return _containerModel.Price; }
    }

    public override double Time {
      get { return _containerModel.Time; }
    }

    public ReadOnlyObservableCollection<ItemViewModel> ItemViewModels { get; private set; }

    public ContainerViewModel(Container containerModel) : base(containerModel) {
      _containerModel = containerModel;
      _itemViewModels = new ObservableCollection<ItemViewModel>(_containerModel.Items.Select(i => new ItemViewModel(i as Item)));
      ItemViewModels = new ReadOnlyObservableCollection<ItemViewModel>(_itemViewModels);
    }
  }
}