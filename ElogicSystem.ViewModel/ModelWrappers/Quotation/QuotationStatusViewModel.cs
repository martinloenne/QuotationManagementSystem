using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class QuotationStatusViewModel : BaseNotify {
    private QuotationStatus _quotationStatusModel;

    public DateTime Date {
      get { return _quotationStatusModel.Date; }
    }

    public QuotationStatusType Type {
      get { return _quotationStatusModel.Type; }
    }

    public string Employee {
      get { return _quotationStatusModel.Employee; }
    }

    public QuotationStatusViewModel(QuotationStatus quotationStatusModel) {
      _quotationStatusModel = quotationStatusModel;
    }
  }
}