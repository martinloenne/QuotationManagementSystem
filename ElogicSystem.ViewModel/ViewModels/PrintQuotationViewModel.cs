using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElogicSystem.ViewModel {

  public class PrintQuotationViewModel : BaseViewModel {
    private IPrintableWindowService _printer;

    public QuotationViewModel Quotation { get; set; }

    public ICommand PrintPDFCommand { get; set; }

    public PrintQuotationViewModel(Action onClose, QuotationViewModel quotation, IPrintableWindowService printer) : base(onClose) {
      Quotation = quotation;
      _printer = printer;
      PrintPDFCommand = new RelayCommand(o => _printer.PrintPDFDocument(), o => true);
    }
  }
}