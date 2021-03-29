using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.DataAccess;
using System.ComponentModel;

namespace ElogicSystem.ViewModel {

  public class PanelViewModel : BaseNotify, IBillOfMaterial, IDataErrorInfo {
    // PROPERTIES

    public Panel PanelModel { get; }

    public int ID {
      get { return PanelModel.ID; }
    }

    public string Description {
      get { return PanelModel.Description; }
      set { PanelModel.Description = value; }
    }

    public int Quantity {
      get { return PanelModel.Quantity; }
      set {
        PanelModel.Quantity = value;
        CalculateInfo();
      }
    }

    public double Price {
      get { return PanelModel.Price; }
      set { PanelModel.Price = value; }
    }

    public double Time {
      get { return PanelModel.Time; }
      set { PanelModel.Time = value; }
    }

    public double BasePrice {
      get { return PanelModel.BasePrice; }
    }

    public double PriceDifference {
      get { return PanelModel.PriceDifference; }
    }

    public double AdjustedPrice {
      get { return PanelModel.AdjustedPrice; }
    }

    public double TotalPrice { get; set; }

    public PanelType Type {
      get { return PanelModel.Type; }
      set { PanelModel.Type = value; }
    }

    public ObservableCollection<ItemViewModel> ItemViewModels { get; private set; }

    public string Error { get; set; }

    public string this[string propertyName] {
      get {
        switch (propertyName) {
          case nameof(Quantity):
            if (!Validate.Quantity(Quantity)) {
              return "Quantity must be a nonnegative number.";
            }
            else {
              return null;
            }
          default:
            throw new Exception("Property not found.");
        }
      }
    }

    // CONSTRUCTORS

    public PanelViewModel(Panel panelModel) {
      PanelModel = panelModel;
      ItemViewModels = new ObservableCollection<ItemViewModel>(PanelModel.PanelItems.Select(i => ItemViewModelFactory.GetItemViewModel(i)));
    }

    public void Add(ItemViewModel itemViewModel) {
      PanelItem panelItem = new PanelItem(PanelModel, itemViewModel.ItemModel);
      panelItem.Quantity = itemViewModel.Quantity;
      PanelModel.PanelItems.Add(panelItem);
      ItemViewModels.Add(ItemViewModelFactory.GetItemViewModel(panelItem));
    }

    public void Remove(int index) {
      PanelModel.PanelItems.RemoveAt(index);
      ItemViewModels.RemoveAt(index);
    }

    public void CalculateInfo() {
      Price = ItemViewModels.Select(x => (x.Price) * (x.Quantity)).Sum();
      Time = ItemViewModels.Select(x => (x.Time) * (x.Quantity)).Sum();
      TotalPrice = Price * Quantity;
    }
  }
}