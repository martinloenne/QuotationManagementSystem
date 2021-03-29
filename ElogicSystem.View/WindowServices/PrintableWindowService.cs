using ElogicSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElogicSystem.View {

  public class PrintableWindowService : IPrintableWindowService {
    private Window _window;

    public PrintableWindowService(Window window) {
      _window = window;
    }

    /// <summary>
    ///  Opens the internal window using the specified view model as its data context.
    /// </summary>
    /// <param name="viewModel">The view model that should be set as data context for the internal window.</param>
    public void Open(BaseViewModel viewModel) {
      _window.DataContext = viewModel;
      _window.Show();
    }

    /// <summary>
    /// Opens the internal window as a dialog using the specified view model as its data context.
    /// </summary>
    /// <param name="baseViewModel">The view model that should be set as data context for the internal window.</param>
    public void OpenAsDialog(BaseViewModel viewModel) {
      _window.DataContext = viewModel;
      _window.ShowDialog();
    }

    /// <summary>
    /// Closes the internal window.
    /// </summary>
    public void Close() {
      _window.Close();
    }

    public void PrintPDFDocument() {
      if (_window is PrintQuotationToPDFView) {
        PrintQuotationToPDFView temp = _window as PrintQuotationToPDFView;
        PrintDialog print = new PrintDialog();

        if (print.ShowDialog() == true) {
          print.PrintVisual(temp.PrintPDF, "Invoice");
        }
      }
    }
  }
}