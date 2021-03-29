using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ElogicSystem.DataAccess;
using System.ComponentModel;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// Represents a view model enabling the binding view to build <see cref="IBillOfMaterial"/>.
  /// </summary>
  public class BuilderViewModel : BaseViewModel {
    private readonly IWindowFactory _windowFactory;

    protected UnitOfWork _unitOfWork;

    public HierarchyDataGrid SourceHierarchyDataGrid { get; set; }

    public HierarchyDataGrid TargetHierarchyDataGrid { get; set; }

    // View Models
    public IBillOfMaterial SourceBillOfMaterial { get; set; }

    public IBillOfMaterial TargetBillOfMaterial { get; set; }

    public bool IsSourceViewHidden { get; set; }

    public int SelectedIndexInSource { get; set; }

    public int SelectedIndexInTarget { get; set; }

    // Current selected item in the binding view.
    protected DataGridItem _selectedItemInSource;

    public DataGridItem SelectedItemInSource {
      get { return _selectedItemInSource; }
      set {
        _selectedItemInSource = value;
        if (_selectedItemInSource != null) {
          SelectedIndexInTarget = SynchronizeSelectedItems(_selectedItemInSource, TargetHierarchyDataGrid);
        }
      }
    }

    // Current selected item in the binding view.
    protected DataGridItem _selectedItemInTarget;

    public DataGridItem SelectedItemInTarget {
      get { return _selectedItemInTarget; }
      set {
        _selectedItemInTarget = value;
        if (_selectedItemInTarget != null) {
          SelectedIndexInSource = SynchronizeSelectedItems(_selectedItemInTarget, SourceHierarchyDataGrid);
        }
      }
    }

    public ICommand AddItemToTargetByButtonCommand { get; set; }
    public ICommand RemoveItemFromTargetByButtonCommand { get; set; }
    public ICommand ShowCancelDialogCommand { get; }
    public ICommand SaveTargetBillOfMaterialCommand { get; }
    public ICommand HideSourceViewCommand { get; }
    public ICommand ShowSourceViewCommand { get; }

    public ICommand AddSelectedItemInSource { get; }
    public ICommand AddSelectedItemInTarget { get; }

    public BuilderViewModel(Action onClose, IWindowFactory windowFactory, IBillOfMaterial sourceBillOfMaterial, IBillOfMaterial targetBillOfMaterial, UnitOfWork unitOfWork) : base(onClose) {
      // Get window.
      _windowFactory = windowFactory;
      _unitOfWork = unitOfWork;

      SourceBillOfMaterial = sourceBillOfMaterial;
      TargetBillOfMaterial = targetBillOfMaterial;

      SyncSourceQuantities(TargetBillOfMaterial.ItemViewModels);

      SourceHierarchyDataGrid = new HierarchyDataGrid(SourceBillOfMaterial.ItemViewModels);
      TargetHierarchyDataGrid = new HierarchyDataGrid(TargetBillOfMaterial.ItemViewModels);

      AddItemToTargetByButtonCommand = new RelayCommand(o => AddByButton(), o => CanAddByButton());
      RemoveItemFromTargetByButtonCommand = new RelayCommand(o => RemoveByButton(), o => CanRemoveByButton());

      AddSelectedItemInSource = new RelayCommand(o => AddRemoveSelectedItemIfValid(_selectedItemInSource), o => _selectedItemInSource != null);
      AddSelectedItemInTarget = new RelayCommand(o => AddRemoveSelectedItemIfValid(_selectedItemInTarget), o => _selectedItemInTarget != null);

      ShowCancelDialogCommand = new RelayCommand(CancelDialog, o => true);
      HideSourceViewCommand = new RelayCommand(o => IsSourceViewHidden = true, o => IsSourceViewHidden == false);
      ShowSourceViewCommand = new RelayCommand(o => IsSourceViewHidden = false, o => IsSourceViewHidden == true);
      SaveTargetBillOfMaterialCommand = new RelayCommand(SaveTargetOnSaveCommand, o => true);

      CalculateInfo();
    }

    private bool CanAddByButton() {
      return _selectedItemInSource?.Level == 0;
    }

    private bool CanRemoveByButton() {
      return _selectedItemInSource?.Level == 0 && _selectedItemInTarget?.ItemViewModel.Quantity > 0;
    }

    private void CancelDialog(object obj) {
      IWindowService cancelDialog = _windowFactory.GetWindowService(WindowType.CancelDialogView);
      cancelDialog.OpenAsDialog(new CancelDialogViewModel(cancelDialog.Close, OnClose));
    }

    protected virtual void SaveTargetOnSaveCommand(object parameter) {
      _unitOfWork.Save();
      OnClose();
    }

    protected virtual void AddByButton() {
      if (_selectedItemInSource != null) {
        _selectedItemInSource.ItemViewModel.Quantity++;
        AddRemoveSelectedItemIfValid(_selectedItemInSource);
      }
    }

    protected virtual void RemoveByButton() {
      if (_selectedItemInSource != null) {
        _selectedItemInSource.ItemViewModel.Quantity--;
        AddRemoveSelectedItemIfValid(_selectedItemInSource);
      }
    }

    private void AddRemoveSelectedItemIfValid(DataGridItem selectedItem) {
      if (selectedItem.Level == 0) {
        int index = GetIndexByID(TargetBillOfMaterial.ItemViewModels, selectedItem.ItemViewModel);

        if (index == -1) {
          if (selectedItem.ItemViewModel.Quantity > 0) {
            TargetBillOfMaterial.Add(selectedItem.ItemViewModel);
            TargetHierarchyDataGrid.Add(selectedItem.ItemViewModel);
          }
        }
        else {
          if (selectedItem.ItemViewModel.Quantity > 0) {
            TargetBillOfMaterial.ItemViewModels[index].Quantity = selectedItem.ItemViewModel.Quantity;
          }
          else {
            if (TargetHierarchyDataGrid.DataGridItems.Count == 1) {
              SelectedIndexInSource = -1;
            }
            TargetBillOfMaterial.Remove(index);
            TargetHierarchyDataGrid.Remove(selectedItem.ItemViewModel);
          }
        }
      }
      CalculateInfo();
    }

    private int SynchronizeSelectedItems(DataGridItem selected, HierarchyDataGrid otherDataGrid) {
      return otherDataGrid.DataGridItems.IndexOf(otherDataGrid.DataGridItems.Where(x => x.ItemViewModel.ID == selected.ItemViewModel.ID && x.Level == selected.Level).FirstOrDefault());
    }

    protected int GetIndexByID(ObservableCollection<ItemViewModel> collection, ItemViewModel item) {
      return collection.IndexOf(collection.Where(x => x.ID == item.ID).FirstOrDefault());
    }

    protected virtual void CalculateInfo() => TargetBillOfMaterial.CalculateInfo();

    protected virtual void SyncSourceQuantities(ObservableCollection<ItemViewModel> sourceItemViewModels) {
      foreach (ItemViewModel itemViewModel in sourceItemViewModels) {
        int index = GetIndexByID(TargetBillOfMaterial.ItemViewModels, itemViewModel);
        if (index != -1) {
          itemViewModel.Quantity = TargetBillOfMaterial.ItemViewModels[index].Quantity;
        }
      }
    }

    public virtual void ResetSourceQuantities() {
      foreach (ItemViewModel itemViewModel in SourceBillOfMaterial.ItemViewModels) {
        itemViewModel.Quantity = 0;
      }
    }
  }
}