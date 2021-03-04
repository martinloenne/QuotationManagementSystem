using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class ItemViewModel : BaseNotify, IDataErrorInfo {
    private Item _itemModel;

    public int ID {
      get { return _itemModel.ID; }
      set { _itemModel.ID = value; }
    }

    public string Description {
      get { return _itemModel.Description; }
      set { _itemModel.Description = value; }
    }

    public double Quantity {
      get { return _itemModel.Quantity; }
      set { _itemModel.Quantity = value; }
    }
      
    public virtual double Price {
      get { return _itemModel.Price; }
    }

    public virtual double Time {
      get { return _itemModel.Time; }
    }

    public string Error => throw new NotImplementedException();

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

    public ItemViewModel(Item itemModel) {
      _itemModel = itemModel;
    }

    public override bool Equals(object obj) {
      if (!(obj is Item other))
        return base.Equals(obj);

      return Equals(ID, other.ID);
    }
  }
}