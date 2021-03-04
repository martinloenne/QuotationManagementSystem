using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  /// <summary>
  /// The different status types a Quotation can be.
  /// </summary>
  public enum QuotationStatusType {
    Created,
    Sent,
    Lost,
    Confirmed,
    Canceled
  }
}