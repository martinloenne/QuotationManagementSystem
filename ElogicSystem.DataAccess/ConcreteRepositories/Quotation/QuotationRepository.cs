using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ElogicSystem.DataAccess {

  public class QuotationRepository : Repository<Quotation>, IQuotationRepository {

    public QuotationRepository(ElogicSystemContext context) : base(context) {
    }

    public IEnumerable<Quotation> GetByStatus(QuotationStatusType quotationStatusType) {
      return DbSet.Where(q => q.CurrentStatus.Type == quotationStatusType);
    }
  }
}