using ElogicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  /// <summary>
  /// Represents a class that provides the ability to complete
  /// one or more operations on the database. Provides repositories and
  /// methods for save and dispose.
  /// </summary>
  public class UnitOfWork : IUnitOfWork {
    private readonly ElogicSystemContext _context;
    private ICustomerRepository _customerRepository;
    private IItemRepository _itemRepository;
    private IPanelRepository _panelRepository;
    private IQuotationRepository _quotationRepository;
    private ITemplateRepository _templateRepository;
    private ICategoryRepository _categoryRepository;
    private IManufacturerRepository _manufacturerRepository;

    public ICustomerRepository CustomerRepository {
      get {
        if (_customerRepository == null) {
          _customerRepository = new CustomerRepository(_context);
        }
        return _customerRepository;
      }
    }

    public IItemRepository ItemRepository {
      get {
        if (_itemRepository == null) {
          _itemRepository = new ItemRepository(_context);
        }
        return _itemRepository;
      }
    }

    public IPanelRepository PanelRepository {
      get {
        if (_panelRepository == null) {
          _panelRepository = new PanelRepository(_context);
        }
        return _panelRepository;
      }
    }

    public IQuotationRepository QuotationRepository {
      get {
        if (_quotationRepository == null) {
          _quotationRepository = new QuotationRepository(_context);
        }
        return _quotationRepository;
      }
    }

    public ITemplateRepository TemplateRepository {
      get {
        if (_templateRepository == null) {
          _templateRepository = new TemplateRepository(_context);
        }
        return _templateRepository;
      }
    }

    public IManufacturerRepository ManufacturerRepository {
      get {
        if (_manufacturerRepository == null) {
          _manufacturerRepository = new ManufacturerRepository(_context);
        }
        return _manufacturerRepository;
      }
    }

    public ICategoryRepository CategoryRepository {
      get {
        if (_categoryRepository == null) {
          _categoryRepository = new CategoryRepository(_context);
        }
        return _categoryRepository;
      }
    }

    public UnitOfWork(ElogicSystemContext context) {
      _context = context;
    }

    public void Save() {
      _context.SaveChanges();
    }

    public void Dispose() {
      _context.Dispose();
    }
  }
}