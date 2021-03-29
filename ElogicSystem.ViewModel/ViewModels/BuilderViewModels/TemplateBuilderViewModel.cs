using ElogicSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class TemplateBuilderViewModel : BuilderViewModel {

    public bool CanAddItem {
      get { return CalculateCanAddItem(); }
    }

    public bool CanRemoveItem {
      get { return CalculateCanRemoveItem(); }
    }

    public TemplateBuilderViewModel(Action onClose, IWindowFactory windowFactory, IBillOfMaterial sourceBillOfMaterial,
                                    IBillOfMaterial targetBillOfMaterial, UnitOfWork unitOfWork) :
                                    base(onClose, windowFactory, sourceBillOfMaterial, targetBillOfMaterial, unitOfWork) {
      AddItemToTargetByButtonCommand = new RelayCommand(o => AddByButton(), o => CalculateCanAddItem());
      RemoveItemFromTargetByButtonCommand = new RelayCommand(o => RemoveByButton(), o => CalculateCanRemoveItem());
    }

    protected override void AddByButton() {
      if (CalculateCanAddItem()) {
        TargetBillOfMaterial.Add(_selectedItemInSource.ItemViewModel);
        TargetHierarchyDataGrid.Add(_selectedItemInSource.ItemViewModel);
      }
    }

    protected override void RemoveByButton() {
      if (CalculateCanRemoveItem()) {
        int index = GetIndexByID(TargetBillOfMaterial.ItemViewModels, _selectedItemInSource.ItemViewModel);
        TargetBillOfMaterial.Remove(index);
        TargetHierarchyDataGrid.Remove(_selectedItemInSource.ItemViewModel);
      }
    }

    private bool CalculateCanAddItem() {
      int index = GetIndexByID(TargetBillOfMaterial.ItemViewModels, _selectedItemInSource.ItemViewModel);
      if (index == -1) {
        return true;
      }
      else {
        return false;
      }
    }

    private bool CalculateCanRemoveItem() {
      int index = GetIndexByID(TargetBillOfMaterial.ItemViewModels, _selectedItemInSource.ItemViewModel);
      if (index != -1) {
        return true;
      }
      else {
        return false;
      }
    }
  }
}