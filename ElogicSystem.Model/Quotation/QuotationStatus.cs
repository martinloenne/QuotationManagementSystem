using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  public class QuotationStatus {
    public int ID { get; private set; }
    public virtual DateTime Date { get; set; }
    public virtual QuotationStatusType Type { get; set; }
    public string EmployeeName { get; set; }

    /// <summary>
    /// The status of a quotation.
    /// </summary>
    /// <param name="date">The date on which the status is applied</param>
    /// <param name="type">The type of the quotation status</param>
    public QuotationStatus(DateTime date, QuotationStatusType type) {
      Date = date;
      Type = type;
    }

    /// <summary>
    /// Do not use - required by EF to load entity.
    /// </summary>
    protected QuotationStatus() { // Cannot be private.
    }
  }
}