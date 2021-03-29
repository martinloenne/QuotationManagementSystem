using ElogicSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public interface IBillOfMaterial {
    int ID { get; }

    string Description { get; }

    ObservableCollection<ItemViewModel> ItemViewModels { get; }

    double Price { get; set; }

    double Time { get; set; }

    void Add(ItemViewModel itemViewModel);

    void Remove(int index);

    void CalculateInfo();
  }
}