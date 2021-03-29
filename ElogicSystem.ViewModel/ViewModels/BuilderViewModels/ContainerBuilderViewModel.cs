using ElogicSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class ContainerBuilderViewModel : BuilderViewModel {
    public bool NeedToBeAdded { get; set; }

    public ContainerBuilderViewModel(Action onClose, IWindowFactory windowFactory, IBillOfMaterial sourceBillOfMaterial, IBillOfMaterial targetBillOfMaterial, UnitOfWork unitOfWork) : base(onClose, windowFactory, sourceBillOfMaterial, targetBillOfMaterial, unitOfWork) {
      SyncSourceQuantities(SourceBillOfMaterial.ItemViewModels);
    }

    protected override void SaveTargetOnSaveCommand(object parameter) {
      _unitOfWork.Save();
      NeedToBeAdded = true;
      OnClose();
    }
  }
}