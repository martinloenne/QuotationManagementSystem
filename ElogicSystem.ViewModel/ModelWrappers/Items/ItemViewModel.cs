using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public abstract class ItemViewModel : BaseNotify, IDataErrorInfo {
    public Item ItemModel { get; }

    public RelationItem RelationItem { get; set; }

    public int ID {
      get { return ItemModel.ID; }
    }

    public string Description {
      get { return ItemModel.Description; }
      set { ItemModel.Description = value; }
    }

    public double Quantity {
      get { return RelationItem.Quantity; }
      set { RelationItem.Quantity = value; }
    }

    public virtual double Price {
      get { return ItemModel.Price; }
      set { ItemModel.Price = value; }
    }

    public virtual double Time {
      get { return ItemModel.Time; }
      set { ItemModel.Time = value; }
    }

    public virtual bool IsContainer { get; set; }

    public bool IsVisible { get; set; }

    public ItemViewModel Parent { get; set; }

    // Not used, but required by interface IDataErrorInfo.
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

    public ItemViewModel(RelationItem relationItem) {
      RelationItem = relationItem;
      ItemModel = RelationItem.Item;
    }

    public ItemViewModel(Item itemModel) {
      ItemModel = itemModel;
    }

    public override bool Equals(object obj) {
      if (!(obj is Item other))
        return base.Equals(obj);

      return Equals(ID, other.ID);
    }
  }
}