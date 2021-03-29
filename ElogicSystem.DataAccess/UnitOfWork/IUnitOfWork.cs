using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  /// <summary>
  /// Defines a way to complete one or more operations on a database.
  /// </summary>
  public interface IUnitOfWork {
    ICustomerRepository CustomerRepository { get; }
    IItemRepository ItemRepository { get; }
    IPanelRepository PanelRepository { get; }
    IQuotationRepository QuotationRepository { get; }
    ITemplateRepository TemplateRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IManufacturerRepository ManufacturerRepository { get; }

    void Save();

    void Dispose();
  }
}