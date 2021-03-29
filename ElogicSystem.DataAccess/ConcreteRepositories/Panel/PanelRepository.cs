using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  public class PanelRepository : Repository<Panel>, IPanelRepository {

    public PanelRepository(ElogicSystemContext context) : base(context) {
    }
  }
}