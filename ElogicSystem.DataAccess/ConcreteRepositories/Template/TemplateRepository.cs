using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  public class TemplateRepository : Repository<Template>, ITemplateRepository {

    public TemplateRepository(ElogicSystemContext context) : base(context) {
    }

  }
}