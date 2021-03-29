using ElogicSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.View {

  /// <summary>
  /// Represents a factory for classes implementing <see cref="IWindowService"/>.
  /// </summary>
  public class WindowFactory : IWindowFactory {

    /// <summary>
    /// Returns a new instance of a class implementing <see cref="IWindowService"/>
    /// where the internal window is determined by the specified window type.
    /// </summary>
    /// <param name="windowType">The window type that represents the internal window
    /// of the requested returned service.</param>
    /// <returns></returns>
    public IWindowService GetWindowService(WindowType windowType) {
      switch (windowType) {
        case WindowType.PanelBuilderView:
          return new WindowService(new PanelBuilderView());

        case WindowType.CancelDialogView:
          return new WindowService(new CancelDialogView());

        case WindowType.ConfigureQuotationView:
          return new WindowService(new ConfigureQuotationView());

        case WindowType.ConfigureCustomerView:
          return new WindowService(new ConfigureCustomerView());

        case WindowType.PrintQuotationView:
          return new PrintableWindowService(new PrintQuotationToPDFView());

        case WindowType.CreateItemView:
          return new WindowService(new CreateItemView());

        case WindowType.ConfigureItemView:
          return new WindowService(new ConfigureItemView());

        case WindowType.TemplateAssemblyView:
          return new WindowService(new TemplateAssemblyView());

        case WindowType.ContainerBuilderView:
          return new WindowService(new ContainerBuilderView());

        default:
          throw new ArgumentException("The window type could not be found.");
      }
    }
  }
}