using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  public class QuotationStatusViewModel : BaseNotify {
    public QuotationStatus QuotationStatusModel { get; private set; }

    public DateTime Date {
      get { return QuotationStatusModel.Date; }
    }

    public QuotationStatusType Type {
      get { return QuotationStatusModel.Type; }
    }

    public string Employee {
      get { return QuotationStatusModel.EmployeeName; }
    }

    public QuotationStatusViewModel(QuotationStatus quotationStatusModel) {
      QuotationStatusModel = quotationStatusModel;
    }
  }
}