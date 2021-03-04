using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class PanelViewModel : BaseNotify {
    // PROPERTIES

    private readonly Panel _panelModel;
    private ObservableCollection<ItemViewModel> _itemViewModels;


    public int ID {
      get { return _panelModel.ID; }
      set { _panelModel.ID = value; }
    }

    public string Description {
      get { return _panelModel.Description; }
      set { _panelModel.Description = value; }
    }

    public double Quantity {
      get { return _panelModel.Quantity; }
      set { _panelModel.Quantity = value; }
    }

    public double Price {
      get { return _panelModel.Price; }
      set { _panelModel.Price = value; }
    }

    public double Time {
      get { return _panelModel.Time; }
    }

    public double BasePrice {
      get { return _panelModel.BasePrice; }
    }

    public double PriceDifference {
      get { return _panelModel.PriceDifference; }
    }

    public double AdjustedPrice {
      get { return _panelModel.AdjustedPrice; }
    }

    public double TotalPrice {
      get { return _panelModel.TotalPrice; }
    }

    public PanelType Type { 
      get { return _panelModel.Type; }
      set { _panelModel.Type = value; }
    }

    public Panel GetPanel { get { return _panelModel; } }

    public ReadOnlyObservableCollection<ItemViewModel> ItemViewModels { get; private set; }

    // CONSTRUCTORS

    public PanelViewModel(Panel panelModel) {
      _panelModel = panelModel;
      Quantity = 0;

      _itemViewModels = new ObservableCollection<ItemViewModel>(panelModel.Items.Select(i => new ItemViewModel(i as Item)));

      ItemViewModels = new ReadOnlyObservableCollection<ItemViewModel>(_itemViewModels);
    }

    // METHODS

    public void AddItem(Item item) => AddItem(item, 1);

    public void AddItem(Item item, double quantity) {
      QuantifiableObjectHandler<Item, ItemViewModel>.Add(_panelModel.Items,
                                                         _itemViewModels,
                                                         item,
                                                         new ItemViewModel(item),
                                                         quantity);
    }

    public void RemoveItem(Item item) => RemoveItem(item, 1);

    public void RemoveItem(Item item, double quantity) {
      QuantifiableObjectHandler<Item, ItemViewModel>.Remove(_panelModel.Items,
                                                            _itemViewModels,
                                                            item,
                                                            quantity);
    }

    public bool Contains(Item item) => QuantifiableObjectHandler<Item, ItemViewModel>.Contains(_panelModel.Items, item);

    public Item Get(Item item) => QuantifiableObjectHandler<Item, ItemViewModel>.Get(_panelModel.Items, item);
  }
}