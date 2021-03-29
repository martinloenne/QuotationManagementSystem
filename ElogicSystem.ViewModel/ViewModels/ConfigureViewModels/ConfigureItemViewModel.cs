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

  public class ConfigureItemViewModel : BaseViewModel {
    private IWindowFactory _windowFactory;

    public ProductViewModel ProductToConfigure { get; }

    public ICommand SaveProductCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public CategoryViewModel SelectedCategory { get; set; }

    public ManufacturerViewModel SelectedManufacturer { get; set; }

    public ObservableCollection<CategoryViewModel> Categories { get; set; }
    public ObservableCollection<ManufacturerViewModel> Manufacturers { get; set; }

    public int SelectedCategoryIndex { get; set; }

    public int SelectedManufacturerIndex { get; set; }

    private UnitOfWork _unitOfWork;

    public ConfigureItemViewModel(Action onClose, ProductViewModel itemToConfigure, IWindowFactory windowFactory, UnitOfWork unitOfWork) : base(onClose) {
      _unitOfWork = unitOfWork;

      _windowFactory = windowFactory;
      ProductToConfigure = itemToConfigure;

      LoadDB();

      SaveProductCommand = new RelayCommand(o => SaveChangesToItem(), o => true);
      CancelCommand = new RelayCommand(o => CancelDialog(), o => true);
    }

    private void SaveChangesToItem() {
      ProductToConfigure.Category = SelectedCategory;
      ProductToConfigure.Manufacturer = SelectedManufacturer;
      _unitOfWork.Save();
      OnClose();
    }

    private void CancelDialog() {
      IWindowService cancelDialog = _windowFactory.GetWindowService(WindowType.CancelDialogView);
      cancelDialog.OpenAsDialog(new CancelDialogViewModel(cancelDialog.Close, OnClose));
    }

    private void LoadDB() {
      List<Category> categoryModels = _unitOfWork.CategoryRepository.GetAll().ToList();
      List<Manufacturer> manufacturerModels = _unitOfWork.ManufacturerRepository.GetAll().ToList();

      Categories = new ObservableCollection<CategoryViewModel>(categoryModels.Select(x => new CategoryViewModel(x)));
      Manufacturers = new ObservableCollection<ManufacturerViewModel>(manufacturerModels.Select(x => new ManufacturerViewModel(x)));

      SelectedCategoryIndex = Categories.IndexOf(Categories.Where(x => x.Name == ProductToConfigure.Category.Name).FirstOrDefault());
      SelectedManufacturerIndex = Manufacturers.IndexOf(Manufacturers.Where(x => x.Name == ProductToConfigure.Manufacturer.Name).FirstOrDefault());
    }
  }
}