using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class ManufacturerViewModel : BaseNotify {
    public Manufacturer ManufacturerModel { get; }

    public string Name {
      get { return ManufacturerModel.Name; }
      set { ManufacturerModel.Name = value; }
    }

    public ManufacturerViewModel(Manufacturer manufacturerModel) {
      ManufacturerModel = manufacturerModel;
    }
  }
}