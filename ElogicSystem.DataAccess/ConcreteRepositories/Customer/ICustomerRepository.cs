using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.DataAccess {
  public interface ICustomerRepository : IRepository<Customer> {
    IEnumerable<Customer> GetByActivity(bool isActive);
  }
}
