using ElogicSystem.DataAccess;
using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class TemplateViewModel : BaseNotify, IBillOfMaterial {
    public Template TemplateModel { get; set; }

    public int ID {
      get { return TemplateModel.ID; }
    }

    public string Description {
      get { return TemplateModel.Description; }
      set { TemplateModel.Description = value; }
    }

    public ObservableCollection<ItemViewModel> ItemViewModels { get; private set; }
    public double Price { get; set; }
    public double Time { get; set; }

    public TemplateViewModel(Template templateModel) {
      TemplateModel = templateModel;

      ItemViewModels = new ObservableCollection<ItemViewModel>(templateModel.TemplateItems.Select(i => ItemViewModelFactory.GetItemViewModel(i)));
    }

    public void Add(ItemViewModel itemViewModel) {
      TemplateItem templateItem = new TemplateItem(TemplateModel, itemViewModel.ItemModel);
      TemplateModel.TemplateItems.Add(templateItem);
      ItemViewModels.Add(ItemViewModelFactory.GetItemViewModel(templateItem));
    }

    public void Remove(int index) {
      TemplateModel.TemplateItems.RemoveAt(index);
      ItemViewModels.RemoveAt(index);
    }

    public void CalculateInfo() {
    }
  }
}