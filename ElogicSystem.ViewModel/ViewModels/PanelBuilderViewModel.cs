using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// Represents a view model enabling the binding view to build <see cref="Panel"/>.
  /// </summary>
  public class PanelBuilderViewModel : BaseViewModel {

    // Models
    private Template _templateModel;

    private readonly IWindowFactory _windowFactory;
    private IWindowService _cancelDialog; // Fjernes hvis muligt, da vindue bør instantieres når behov

    // View Models
    public TemplateViewModel TemplateViewModel { get; set; }
    public PanelViewModel PanelViewModel { get; set; }
    public ObservableCollection<TemplateViewModel> TemplateViewModels { get; set; }

    public bool IsTemplateGridHidden { get; set; }

    public Item SelectedItemInPanelGrid { get; set; }

    // Current selected item in the binding view.
    private Item _selectedItemInTemplateGrid;

    public Item SelectedItemInTemplateGrid {
      get { return _selectedItemInTemplateGrid; }
      set {
        if(_selectedItemInTemplateGrid != null) {
          AddRemoveSelectedItemIfValid();
        }
        _selectedItemInTemplateGrid = value;
      }
    }

    public ICommand AddItemByButtonToPanelCommand { get; }
    public ICommand RemoveItemFromPanelCommand { get; }
    public ICommand ShowCancelDialogCommand { get; }
    public ICommand CloseCancelDialogCommand { get; }
    public ICommand ClosePanelBuilderFromDialogViewCommand { get; }
    public ICommand SavePanelCommand { get; }
    public ICommand HideTemplateViewCommand { get; }
    public ICommand ShowTemplateViewCommand { get; }

    public PanelBuilderViewModel(Action onClose, IWindowFactory windowFactory, PanelViewModel panelViewModel) : base(onClose) {
      // Get window.
      _windowFactory = windowFactory;

      //TODO: undgå at lave vinduet på forhånd..

      // Instantiate viewmodels. (TEST, CALL DATABASE HERE)
      _templateModel = new Template(1, "");
      TemplateViewModel = new TemplateViewModel(_templateModel);
      PanelViewModel = panelViewModel;
      TemplateViewModels = new ObservableCollection<TemplateViewModel>();

      // Instantiate commands.
      AddItemByButtonToPanelCommand = new RelayCommand(o => PanelViewModel.AddItem(SelectedItemInTemplateGrid),
        o => true);

      RemoveItemFromPanelCommand = new RelayCommand(o => PanelViewModel.RemoveItem(SelectedItemInPanelGrid),
        o => true);

      ClosePanelBuilderFromDialogViewCommand = new RelayCommand(o => { _cancelDialog.Close(); OnClose(); },
        o => true);

      ShowCancelDialogCommand = new RelayCommand(ShowCancelPanelBuild, o => true);
      CloseCancelDialogCommand = new RelayCommand(o => _cancelDialog.Close(), o => true);
      HideTemplateViewCommand = new RelayCommand(o => IsTemplateGridHidden = true, o => IsTemplateGridHidden == false);
      ShowTemplateViewCommand = new RelayCommand(o => IsTemplateGridHidden = false, o => IsTemplateGridHidden == true);
      SavePanelCommand = new RelayCommand(SavePanelOnSaveCommand, o => true);

      // Populate template list. (TEST, SHOULD BE POPULATED FROM DATABASE CALL)

      for(int i = 0; i < 10; i++) {
        Product product = new Product(100*i, "produktbeskrivelse", 0, i, 2*i, new Category(), new Manufacturer());
        TemplateViewModel.AddItem(product, 0);
      }

      for(int i = 0; i < 10; i++) {
        TemplateViewModels.Add(TemplateViewModel);
        TemplateViewModels[i].ID = i;
      }
    }

    private void ShowCancelPanelBuild(object obj) {
      _cancelDialog = _windowFactory.GetWindowService(WindowType.PanelBuilderCancelDialogView);
      _cancelDialog.OpenAsDialog(this);
    }

    // Saves the panel
    private void SavePanelOnSaveCommand(object parameter) {
      // Implement storing the panel to the database here..
      OnClose();
    }

    private void AddRemoveSelectedItemIfValid() {
      if(PanelViewModel.Contains(_selectedItemInTemplateGrid)) {
        AddRemoveExistingItemIfValid();
      }
      else if(_selectedItemInTemplateGrid.Quantity > 0) {
        PanelViewModel.AddItem(_selectedItemInTemplateGrid, 0);
      }
    }

    private void AddRemoveExistingItemIfValid() {
      Item itemInPanel = PanelViewModel.Get(_selectedItemInTemplateGrid);

      if(_selectedItemInTemplateGrid.Quantity == 0) {
        PanelViewModel.RemoveItem(itemInPanel);
      }
      else {
        PanelViewModel.AddItem(_selectedItemInTemplateGrid, 0);
      }
    }
  }
}