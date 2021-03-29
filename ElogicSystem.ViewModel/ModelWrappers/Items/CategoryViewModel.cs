using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class CategoryViewModel : BaseNotify {
    public Category CategoryModel { get; set; }

    public string Name {
      get { return CategoryModel.Name; }
      set { CategoryModel.Name = value; }
    }

    public double Time {
      get { return CategoryModel.Time; }
      set { CategoryModel.Time = value; }
    }

    public CategoryViewModel(Category categoryModel) {
      CategoryModel = categoryModel;
    }
  }
}