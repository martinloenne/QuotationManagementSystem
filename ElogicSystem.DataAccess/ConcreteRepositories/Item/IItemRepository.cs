using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  public interface IItemRepository : IRepository<Item> {

    /// <summary>
    /// Gets all items which ID starts with the specified number.
    /// </summary>
    /// <param name="uncompleteID">The number the ID should start with.</param>
    /// <returns></returns>
    IEnumerable<Item> GetByIDStartsWith(int number);
  }
}