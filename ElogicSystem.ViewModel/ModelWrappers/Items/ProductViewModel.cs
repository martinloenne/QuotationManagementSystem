using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class ProductViewModel : ItemViewModel {
    public Product ProductModel { get; }

    private CategoryViewModel _categoryViewModel;

    public CategoryViewModel Category {
      get { return _categoryViewModel; }
      set {
        _categoryViewModel = value;
        ProductModel.Category = value.CategoryModel;
      }
    }

    private ManufacturerViewModel _manufacturerViewModel;

    public ManufacturerViewModel Manufacturer {
      get { return _manufacturerViewModel; }
      set {
        _manufacturerViewModel = value;
        ProductModel.Manufacturer = value.ManufacturerModel;
      }
    }

    public string Link {
      get { return ProductModel.Link; }
      set { ProductModel.Link = value; }
    }

    public ProductViewModel(Product productModel) : base(productModel) {
      ProductModel = productModel;
      Category = new CategoryViewModel(ProductModel.Category);
      Manufacturer = new ManufacturerViewModel(ProductModel.Manufacturer);
    }

    public ProductViewModel(RelationItem relationItem) : base(relationItem) {
    }
  }
}