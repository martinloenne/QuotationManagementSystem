using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ElogicSystem.Model;

namespace ElogicSystem.DataAccess {

  /// <summary>
  /// Represents a class that is able to perform basic CRUD operations on an entity.
  /// </summary>
  /// <typeparam name="TEntity">The entity for which operations should be perfomed on</typeparam>
  public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class {
    protected DbSet<TEntity> DbSet { get; }
    protected ElogicSystemContext Context { get; }

    public Repository(ElogicSystemContext context) {
      DbSet = context.Set<TEntity>();
      Context = context;
    }

    public virtual void Add(TEntity entity) {
      DbSet.Add(entity);
    }

    public virtual void Delete(TEntity entity) {
      DbSet.Remove(entity);
    }

    public virtual TEntity GetByID(int ID) {
      return DbSet.Find(ID);
    }

    public virtual IEnumerable<TEntity> GetAll() {
      return DbSet;
    }

    public void AddRange(IEnumerable<TEntity> entities) {
      DbSet.AddRange(entities);
    }

    public void DeleteRange(IEnumerable<TEntity> entities) {
      DbSet.RemoveRange(entities);
    }
  }
}