using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  public class CategoryRepository : Repository<Category>, ICategoryRepository {

    public CategoryRepository(ElogicSystemContext context) : base(context) {
    }
  }
}