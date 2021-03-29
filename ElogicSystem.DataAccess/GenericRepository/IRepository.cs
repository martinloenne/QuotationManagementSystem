using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  /// <summary>
  /// Defines a way to perform basic CRUD operations on an entity.
  /// </summary>
  /// <typeparam name="TEntity">The entity for which operations should be perfomed on.</typeparam>
  public interface IRepository<TEntity> {

    TEntity GetByID(int ID);

    IEnumerable<TEntity> GetAll();

    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);
  }
}