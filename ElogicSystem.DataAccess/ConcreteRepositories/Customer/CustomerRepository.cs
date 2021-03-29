using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ElogicSystem.DataAccess {

  public class CustomerRepository : Repository<Customer>, ICustomerRepository {

    public CustomerRepository(ElogicSystemContext dbContext) : base(dbContext) {
    }

    public IEnumerable<Customer> GetByActivity(bool isActive) {
      return DbSet.Where(c => c.IsActive == isActive);
    }
  }
}