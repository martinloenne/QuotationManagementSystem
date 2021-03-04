using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  [Serializable]
  public class QuotationStatus {

    // PROPERTIES

    public DateTime Date { get; }
    public QuotationStatusType Type { get; }
    public string Employee { get; }

    // CONSTRUCTORS

    /// <summary>
    /// The status of a quotation
    /// </summary>
    /// <param name="date">The date on which the status is applied</param>
    /// <param name="type">The type of the quotation status</param>
    /// <param name="employee">The employee who applied the status</param>
    public QuotationStatus(DateTime date, QuotationStatusType type, string employee) {
      Date = date;
      Type = type;
      Employee = employee;
    }
  }
}