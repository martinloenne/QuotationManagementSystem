using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class HierarchyDataGrid : BaseNotify {
    public ObservableCollection<DataGridItem> DataGridItems { get; set; }

    public HierarchyDataGrid(ObservableCollection<ItemViewModel> itemViewModels) {
      DataGridItems = new ObservableCollection<DataGridItem>();

      foreach (ItemViewModel parent in itemViewModels) {
        DataGridItem gridItemParent = new DataGridItem(parent);
        gridItemParent.Level = 0;
        DataGridItems.Add(gridItemParent);

        AddChildren(parent, gridItemParent, 1);
      }
    }

    private void AddChildren(ItemViewModel parent, DataGridItem gridItemParent, int level) {
      if (parent is ContainerViewModel) {
        foreach (ItemViewModel child in (parent as ContainerViewModel).ItemViewModels) {
          DataGridItem gridItemChild = new DataGridItem(child);
          gridItemChild.Level = level;
          gridItemChild.Hidden = true;

          gridItemParent.Children.Add(gridItemChild);
          DataGridItems.Add(gridItemChild);

          if (child is ContainerViewModel) {
            AddChildren(child, gridItemChild, gridItemChild.Level + 1);
          }
        }
      }
    }

    private void RemoveAllChildren(DataGridItem gridItemParent) {
      if (gridItemParent.ItemViewModel is ContainerViewModel) {
        foreach (DataGridItem child in gridItemParent.Children) {
          RemoveAllChildren(child);
          DataGridItems.Remove(child);
        }
      }
    }

    public void Remove(ItemViewModel item) {
      DataGridItem gridItemToRemove = DataGridItems.First(x => x.ItemViewModel.ID == item.ID && x.Level == 0);

      RemoveAllChildren(gridItemToRemove);
      DataGridItems.Remove(gridItemToRemove);
    }

    public void Add(ItemViewModel item) {
      DataGridItem gridItemToAdd = new DataGridItem(item);
      gridItemToAdd.Level = 0;
      DataGridItems.Add(gridItemToAdd);

      AddChildren(item, gridItemToAdd, 1);
    }
  }
}