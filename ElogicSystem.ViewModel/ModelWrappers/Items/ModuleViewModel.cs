using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class ModuleViewModel : ContainerViewModel {
    public Module ModuleModel { get; }

    public ModuleViewModel(Module moduleModel) : base(moduleModel) {
      ModuleModel = moduleModel;
    }

    public ModuleViewModel(RelationItem relationItem) : base(relationItem) {
    }
  }
}