using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  public class ItemRepository : Repository<Item>, IItemRepository {

    public ItemRepository(ElogicSystemContext dbContext) : base(dbContext) {
    }

    /// <summary>
    /// Gets all Items which ID starts with the specified number.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public IEnumerable<Item> GetByIDStartsWith(int number) {
      return DbSet.Where(i => i.ID.ToString().StartsWith(number.ToString()));
    }
  }
}