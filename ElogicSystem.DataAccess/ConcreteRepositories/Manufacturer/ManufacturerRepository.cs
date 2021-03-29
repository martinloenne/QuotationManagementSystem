using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository {

    public ManufacturerRepository(ElogicSystemContext context) : base(context) {
    }
  }
}