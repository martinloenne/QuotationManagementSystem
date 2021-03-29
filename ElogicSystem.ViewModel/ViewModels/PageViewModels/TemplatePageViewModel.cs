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

  public class TemplatePageViewModel : BaseViewModel, IDisposableViewModel {
    private IWindowFactory _windowFactory;

    private TemplateViewModel _allItemViewModels;

    public TemplateViewModel SelectedTemplate { get; set; }
    public ObservableCollection<TemplateViewModel> Templates { get; set; }

    public ICommand CreateNewTemplateCommand { get; set; }
    public ICommand ConfigureTemplateCommand { get; set; }
    public ICommand DeleteTemplateCommand { get; set; }

    private UnitOfWork _unitOfWork;

    public TemplatePageViewModel(Action onClose, IWindowFactory windowFactory) : base(onClose) {
      _windowFactory = windowFactory;

      LoadDB();

      CreateNewTemplateCommand = new RelayCommand(CreateNewTemplate, o => true);
      ConfigureTemplateCommand = new RelayCommand(ConfigureTemplate, o => SelectedTemplate != null);
      DeleteTemplateCommand = new RelayCommand(DeleteTemplate, o => SelectedTemplate != null);
    }

    private void CreateNewTemplate(object obj) {
      TemplateViewModel template = new TemplateViewModel(new Template());

      _unitOfWork.TemplateRepository.Add(template.TemplateModel);
      _unitOfWork.Save();

      Templates.Add(template);

      IWindowService templateBuilderView = _windowFactory.GetWindowService(WindowType.TemplateAssemblyView);

      TemplateBuilderViewModel templateBuilderViewModel = new TemplateBuilderViewModel(templateBuilderView.Close, _windowFactory, _allItemViewModels, template, _unitOfWork);

      templateBuilderView.OpenAsDialog(templateBuilderViewModel);

      if (template.ItemViewModels.Count == 0) {
        Templates.Remove(template);
        _unitOfWork.TemplateRepository.Delete(template.TemplateModel);
        _unitOfWork.Save();
        LoadDB();
      }
    }

    private void ConfigureTemplate(object obj) {
      if (SelectedTemplate != null) {
        IWindowService templateBuilderView = _windowFactory.GetWindowService(WindowType.TemplateAssemblyView);

        TemplateBuilderViewModel templateBuilderViewModel = new TemplateBuilderViewModel(templateBuilderView.Close, _windowFactory, _allItemViewModels, SelectedTemplate, _unitOfWork);

        templateBuilderView.OpenAsDialog(templateBuilderViewModel);
      }
      LoadDB();
    }

    private void DeleteTemplate(object obj) {
      if (SelectedTemplate != null) {
        _unitOfWork.TemplateRepository.Delete(SelectedTemplate.TemplateModel);
        _unitOfWork.Save();
        Templates.Remove(SelectedTemplate);
      }
    }

    private void LoadDB() {
      _unitOfWork?.Dispose();
      _unitOfWork = new UnitOfWork(new ElogicSystemContext());

      List<Template> templateModels = _unitOfWork.TemplateRepository.GetAll().ToList();
      Templates = new ObservableCollection<TemplateViewModel>(templateModels.Select(x => new TemplateViewModel(x)));

      List<Item> itemModels = _unitOfWork.ItemRepository.GetAll().ToList();

      Template allItemModels = new Template();
      allItemModels.TemplateItems.AddRange(itemModels.Select(x => new TemplateItem(allItemModels, x)));

      _allItemViewModels = new TemplateViewModel(allItemModels);
    }

    public void SaveAndDispose() {
      _unitOfWork?.Save();
      _unitOfWork?.Dispose();
    }
  }
}