using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {

  public class DataGridItem : BaseNotify {
    public ItemViewModel ItemViewModel { get; set; }

    public bool Colorize { get; set; }

    public bool ShowExpand { get; set; }
    public bool ShowCollapse { get; set; }

    public ObservableCollection<DataGridItem> Children { get; private set; }

    public ICommand ExpandContainerCommand { get; }
    public ICommand CollapseContainerCommand { get; }
    public bool Hidden { get; set; }

    public int Level { get; set; }

    public DataGridItem(ItemViewModel itemViewModel) {
      Children = new ObservableCollection<DataGridItem>();

      Colorize = true;

      ItemViewModel = itemViewModel;

      ShowExpand = ItemViewModel.IsContainer;
      ShowCollapse = false;

      ExpandContainerCommand = new RelayCommand(ShowChildren, o => true);
      CollapseContainerCommand = new RelayCommand(HideAllChildren, o => true);
    }

    private void ShowChildren(object obj) {
      ShowExpand = false;
      ShowCollapse = true;
      foreach (DataGridItem child in Children) {
        child.Hidden = false;
      }
    }

    private void HideAllChildren(object obj) {
      ShowExpand = true;
      ShowCollapse = false;
      foreach (DataGridItem child in Children) {
        HideChildren(child);
      }
    }

    private void HideChildren(DataGridItem child) {
      child.Hidden = true;
      if (child.ItemViewModel.IsContainer) {
        child.ShowExpand = true;
        child.ShowCollapse = false;
        foreach (DataGridItem subChild in child.Children) {
          HideChildren(subChild);
        }
      }
    }
  }
}