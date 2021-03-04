using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class TemplateViewModel : BaseNotify {

    #region Private fields

    private readonly Template _templateModel;

    private ObservableCollection<ItemViewModel> _itemViewModels;

    #endregion Private fields

    #region Properties

    public int ID {
      get { return _templateModel.ID; }
      set { _templateModel.ID = value; }
    }

    public ReadOnlyObservableCollection<ItemViewModel> ItemViewModels { get; private set; }

    #endregion Properties

    #region Constructor

    public TemplateViewModel(Template templateModel) {
      _templateModel = templateModel;

      _itemViewModels = new ObservableCollection<ItemViewModel>(templateModel.Items.Select(i => new ItemViewModel((Item)i)));

      ItemViewModels = new ReadOnlyObservableCollection<ItemViewModel>(_itemViewModels);
    }

    #endregion Constructor

    #region Methods

    public void AddItem(Item item) => AddItem(item, 1);

    public void AddItem(Item item, double quantity) {
      QuantifiableObjectHandler<Item, ItemViewModel>.Add(_templateModel.Items,
                                                         _itemViewModels,
                                                         item,
                                                         new ItemViewModel(item),
                                                         quantity);
    }

    public void RemoveItem(Item item) => RemoveItem(item, 1);

    public void RemoveItem(Item item, double quantity) {
      QuantifiableObjectHandler<Item, ItemViewModel>.Remove(_templateModel.Items,
                                                            _itemViewModels,
                                                            item,
                                                            quantity);
    }

    #endregion Methods
  }
}