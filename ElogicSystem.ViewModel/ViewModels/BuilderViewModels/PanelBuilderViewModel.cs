using ElogicSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class PanelBuilderViewModel : BuilderViewModel {
    public ObservableCollection<TemplateViewModel> TemplateViewModels { get; set; }

    private TemplateViewModel _templateViewModel;

    public TemplateViewModel TemplateViewModel {
      get { return _templateViewModel; }
      set {
        _templateViewModel = value;
        SourceBillOfMaterial = TemplateViewModel;
        SourceHierarchyDataGrid = new HierarchyDataGrid(SourceBillOfMaterial.ItemViewModels);
      }
    }

    public PanelBuilderViewModel(Action onClose, IWindowFactory windowFactory, IBillOfMaterial sourceBillOfMaterial,
                                 IBillOfMaterial targetBillOfMaterial, UnitOfWork unitOfWork, ObservableCollection<TemplateViewModel> templates) :
                                 base(onClose, windowFactory, sourceBillOfMaterial, targetBillOfMaterial, unitOfWork) {
      TemplateViewModels = templates;

      foreach (TemplateViewModel templateViewModel in TemplateViewModels) {
        SyncSourceQuantities(templateViewModel.ItemViewModels);
      }
    }

    public override void ResetSourceQuantities() {
      foreach (TemplateViewModel templateViewModel in TemplateViewModels) {
        foreach (ItemViewModel itemViewModel in templateViewModel.ItemViewModels) {
          itemViewModel.Quantity = 0;
        }
      }
    }
  }
}