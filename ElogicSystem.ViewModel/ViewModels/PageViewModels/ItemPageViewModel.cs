using ElogicSystem.DataAccess;
using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {

  public class ItemPageViewModel : BaseViewModel, IDisposableViewModel {
    private IWindowFactory _windowFactory;

    public ObservableCollection<ItemViewModel> Items { get; private set; }

    public ItemViewModel SelectedItem { get; set; }

    public ICommand CreateNewItemCommand { get; set; }
    public ICommand ConfigureItemCommand { get; set; }
    public ICommand DeleteItemCommand { get; set; }

    private UnitOfWork _unitOfWork;

    private TemplateViewModel _allProducts;
    private TemplateViewModel _allProductsAndModules;

    public ItemPageViewModel(Action onClose, IWindowFactory windowFactory) : base(onClose) {
      _windowFactory = windowFactory;

      LoadDB();

      CreateNewItemCommand = new RelayCommand(o => CreateNewItem(), o => true);
      ConfigureItemCommand = new RelayCommand(o => ConfigureItem(), o => SelectedItem != null);
      DeleteItemCommand = new RelayCommand(o => DeleteItem(), o => SelectedItem != null);
    }

    public void RemoveItemFromCollection(ItemViewModel item) {
      if (item != null) {
        Items.Remove(item);
        _unitOfWork.ItemRepository.Delete(item.ItemModel);
        _unitOfWork.Save();
      }
    }

    private void CreateNewItem() {
      IWindowService createItemView = _windowFactory.GetWindowService(WindowType.CreateItemView);

      CreateItemViewModel createItemViewModel = new CreateItemViewModel(createItemView.Close, Items, _windowFactory, _unitOfWork);

      createItemView.OpenAsDialog(createItemViewModel);

      if (!createItemViewModel.NeedToBeAdded) {
        RemoveItemFromCollection(createItemViewModel.ItemToBeAdded);
        _unitOfWork.Dispose();
        LoadDB();
      }
    }

    private void ConfigureItem() {
      if (SelectedItem != null) {
        switch (SelectedItem) {
          case ProductViewModel p:
            ConfigureProduct();
            break;

          case ModuleViewModel m:
            ConfigureContainer(SelectedItem as ContainerViewModel, _allProducts);
            break;

          case BlockViewModel b:
            ConfigureContainer(SelectedItem as ContainerViewModel, _allProductsAndModules);
            break;

          default:
            break;
        }
        _unitOfWork.Dispose();
        LoadDB();
      }
    }

    private void ConfigureContainer(ContainerViewModel container, TemplateViewModel source) {
      IWindowService containerBuilderView = _windowFactory.GetWindowService(WindowType.ContainerBuilderView);

      ContainerBuilderViewModel containerBuilderViewModel = new ContainerBuilderViewModel(containerBuilderView.Close, _windowFactory, source, container, _unitOfWork);
      containerBuilderView.OpenAsDialog(containerBuilderViewModel);
    }

    private void ConfigureProduct() {
      IWindowService configureItemView = _windowFactory.GetWindowService(WindowType.ConfigureItemView);
      ConfigureItemViewModel configureItem = new ConfigureItemViewModel(configureItemView.Close, SelectedItem as ProductViewModel, _windowFactory, _unitOfWork);
      configureItemView.OpenAsDialog(configureItem);
    }

    private void DeleteItem() {
      if (SelectedItem != null) {
        RemoveItemFromCollection(SelectedItem);
      }
    }

    private void LoadDB() {
      _unitOfWork = new UnitOfWork(new ElogicSystemContext());

      List<Item> itemModels = _unitOfWork.ItemRepository.GetAll().ToList();

      List<Category> categoryModels = _unitOfWork.CategoryRepository.GetAll().ToList();
      List<Manufacturer> manufacturerModels = _unitOfWork.ManufacturerRepository.GetAll().ToList();

      Template allProductModels = new Template();
      allProductModels.TemplateItems.AddRange(itemModels.Select(x => new TemplateItem(allProductModels, x)).Where(x => x.Item is Product));

      Template allProductAndModuleModels = new Template();
      allProductAndModuleModels.TemplateItems.AddRange(itemModels.Select(x => new TemplateItem(allProductModels, x)).Where(x => x.Item is Product || x.Item is Module));

      _allProducts = new TemplateViewModel(allProductModels);
      _allProductsAndModules = new TemplateViewModel(allProductAndModuleModels);

      Items = new ObservableCollection<ItemViewModel>(itemModels.Select(x => ItemViewModelFactory.GetItemViewModel(x)));
    }

    public void SaveAndDispose() {
      _unitOfWork?.Save();
      _unitOfWork?.Dispose();
    }
  }
}