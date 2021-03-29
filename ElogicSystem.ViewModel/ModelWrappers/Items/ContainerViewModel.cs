using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.DataAccess;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public abstract class ContainerViewModel : ItemViewModel, IBillOfMaterial {
    public Container ContainerModel { get; private set; }

    public override double Price {
      get { return CalculatePrice(); }
      set { ContainerModel.Price = value; }
    }

    public override double Time {
      get { return CalculateTime(); }
      set { ContainerModel.Time = value; }
    }

    public override bool IsContainer {
      get { return true; }
    }

    public ObservableCollection<ItemViewModel> ItemViewModels { get; private set; }

    public ContainerViewModel(Container containerModel) : base(containerModel) {
      ContainerModel = containerModel;
      ItemViewModels = new ObservableCollection<ItemViewModel>(ContainerModel.ContainerItems.Select(i => ItemViewModelFactory.GetItemViewModel(i)));
    }

    public ContainerViewModel(RelationItem relationItem) : base(relationItem) {
      ContainerModel = relationItem.Item as Container;

      ItemViewModels = new ObservableCollection<ItemViewModel>(ContainerModel.ContainerItems.Select(i => ItemViewModelFactory.GetItemViewModel(i)));
    }

    protected double CalculatePrice() => ItemViewModels.Select(x => (x.Price) * (x.Quantity)).Sum();

    protected double CalculateTime() => ItemViewModels.Select(x => (x).Time * (x.Quantity)).Sum();

    public void Add(ItemViewModel itemViewModel) {
      ContainerItem containerItem = new ContainerItem(ContainerModel, itemViewModel.ItemModel);
      ContainerModel.ContainerItems.Add(containerItem);
      ItemViewModels.Add(ItemViewModelFactory.GetItemViewModel(containerItem));
    }

    public void Remove(int index) {
      ContainerModel.ContainerItems.RemoveAt(index);
      ItemViewModels.RemoveAt(index);
    }

    public void CalculateInfo() {
      Price = ItemViewModels.Select(x => (x.Price) * (x.Quantity)).Sum();
      Time = ItemViewModels.Select(x => (x.Time) * (x.Quantity)).Sum();
    }
  }
}