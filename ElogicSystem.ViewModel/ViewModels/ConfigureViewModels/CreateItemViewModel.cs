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

  public class CreateItemViewModel : BaseViewModel {
    private ObservableCollection<ItemViewModel> _items;
    private IWindowFactory _windowFactory;

    private int _selectedTypeIndex;

    public bool CreateProductVisible { get; set; }

    public ItemViewModel ItemToBeAdded { get; set; }

    public ProductViewModel NewProduct { get; set; }

    public int SelectedTypeIndex {
      get { return _selectedTypeIndex; }
      set {
        if (value == 0) {
          CreateNewProduct();
        }
        else if (value == 1) {
          CreateNewContainer(new ModuleViewModel(new Module()), _allProducts);
        }
        else if (value == 2) {
          CreateNewContainer(new BlockViewModel(new Block()), _allProductsAndModules);
        }
        _selectedTypeIndex = value;
      }
    }

    public ICommand SaveProductCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public ObservableCollection<CategoryViewModel> Categories { get; set; }

    public ObservableCollection<ManufacturerViewModel> Manufacturers { get; set; }

    public CategoryViewModel SelectedCategory { get; set; }
    public ManufacturerViewModel SelectedManufacturer { get; set; }

    public bool NeedToBeAdded { get; set; }

    public int NewProductID { get; set; }

    private UnitOfWork _unitOfWork;

    private TemplateViewModel _allProductsAndModules;
    private TemplateViewModel _allProducts;

    public CreateItemViewModel(Action onClose, ObservableCollection<ItemViewModel> items, IWindowFactory windowFactory, UnitOfWork unitOfWork) : base(onClose) {
      _items = items;
      _windowFactory = windowFactory;
      _unitOfWork = unitOfWork;

      LoadDB();

      SelectedTypeIndex = -1;
      SaveProductCommand = new RelayCommand(o => SaveProduct(), o => true);
      CancelCommand = new RelayCommand(o => CancelDialog(), o => true);
    }

    private void CancelDialog() {
      IWindowService cancelDialog = _windowFactory.GetWindowService(WindowType.CancelDialogView);
      cancelDialog.OpenAsDialog(new CancelDialogViewModel(cancelDialog.Close, OnClose));
    }

    private void SaveProduct() {
      NeedToBeAdded = true;

      NewProduct.Category = SelectedCategory;
      NewProduct.Manufacturer = SelectedManufacturer;

      OnClose();
    }

    private void CreateNewProduct() {
      CreateProductVisible = true;

      NewProduct = new ProductViewModel(new Product());
      NewProduct.Category = Categories[0];
      NewProduct.Manufacturer = Manufacturers[0];

      _unitOfWork.ItemRepository.Add(NewProduct.ItemModel);
      _unitOfWork.Save();

      _items.Add(NewProduct);

      NewProductID = NewProduct.ID;

      ItemToBeAdded = NewProduct;
    }

    private void CreateNewContainer(ContainerViewModel container, TemplateViewModel source) {
      _unitOfWork.ItemRepository.Add(container.ContainerModel);
      _unitOfWork.Save();
      _items.Add(container);

      ItemToBeAdded = container;

      IWindowService containerBuilderView = _windowFactory.GetWindowService(WindowType.ContainerBuilderView);

      ContainerBuilderViewModel containerBuilderViewModel = new ContainerBuilderViewModel(containerBuilderView.Close, _windowFactory, source, container, _unitOfWork);
      containerBuilderView.OpenAsDialog(containerBuilderViewModel);

      OnClose();
    }

    private void LoadDB() {
      List<Category> categoryModels = _unitOfWork.CategoryRepository.GetAll().ToList();
      List<Manufacturer> manufacturerModels = _unitOfWork.ManufacturerRepository.GetAll().ToList();

      Categories = new ObservableCollection<CategoryViewModel>(categoryModels.Select(x => new CategoryViewModel(x)));
      Manufacturers = new ObservableCollection<ManufacturerViewModel>(manufacturerModels.Select(x => new ManufacturerViewModel(x)));

      List<Item> itemModels = _unitOfWork.ItemRepository.GetAll().ToList();

      Template allProductModels = new Template();
      allProductModels.TemplateItems.AddRange(itemModels.Select(x => new TemplateItem(allProductModels, x)).Where(x => x.Item is Product));

      Template allProductAndModuleModels = new Template();
      allProductAndModuleModels.TemplateItems.AddRange(itemModels.Select(x => new TemplateItem(allProductModels, x)).Where(x => x.Item is Product || x.Item is Module));

      _allProducts = new TemplateViewModel(allProductModels);
      _allProductsAndModules = new TemplateViewModel(allProductAndModuleModels);
    }
  }
}