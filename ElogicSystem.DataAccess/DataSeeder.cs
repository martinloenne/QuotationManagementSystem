using ElogicSystem.DataAccess;
using ElogicSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.DataAccess {

  public class DataSeeder {
    private UnitOfWork _unitOfWork;

    public DataSeeder() {
      ResetDB();
      _unitOfWork = new UnitOfWork(new ElogicSystemContext());
    }

    private void ResetDB() {
      ElogicSystemContext db = new ElogicSystemContext();
      db.Database.EnsureDeleted();
      db.Database.EnsureCreated();
    }

    public void SeedAll() {
      SeedCategories();
      SeedManufacturers();
      SeedProducts();
      SeedModules();
      SeedBlocks();
      SeedPanels();
      SeedCustomers();
      SeedQuotations();
      SeedTemplates();
      _unitOfWork.Save();
      _unitOfWork.Dispose();
    }

    public void SeedCategories() {
      Category c1 = new Category {
        Name = "Category 1",
        Time = 1
      };

      Category c2 = new Category {
        Name = "Category 2",
        Time = 2
      };

      Category c3 = new Category {
        Name = "Category 3",
        Time = 3
      };

      _unitOfWork.CategoryRepository.Add(c1);
      _unitOfWork.CategoryRepository.Add(c2);
      _unitOfWork.CategoryRepository.Add(c3);
      _unitOfWork.Save();
      ;
    }

    public void SeedManufacturers() {
      Manufacturer ma1 = new Manufacturer {
        Name = "Manufacturer1",
      };

      Manufacturer ma2 = new Manufacturer {
        Name = "Manufacturer2",
      };

      Manufacturer ma3 = new Manufacturer {
        Name = "Manufacturer3",
      };

      _unitOfWork.ManufacturerRepository.Add(ma1);
      _unitOfWork.ManufacturerRepository.Add(ma2);
      _unitOfWork.ManufacturerRepository.Add(ma3);
      _unitOfWork.Save();
    }

    public void SeedProducts() {
      Product p1 = new Product {
        Link = "www.product1.com",
        Description = "Product1",
        Price = 100,
        Time = 1,
        Manufacturer = _unitOfWork.ManufacturerRepository.GetByID(1),
        Category = _unitOfWork.CategoryRepository.GetByID(1)
      };
      ;

      Product p2 = new Product {
        Link = "www.product2.com",
        Description = "Product2",
        Price = 200,
        Time = 2,
        Manufacturer = _unitOfWork.ManufacturerRepository.GetByID(2),
        Category = _unitOfWork.CategoryRepository.GetByID(1)
      };

      Product p3 = new Product {
        Link = "www.product3.com",
        Description = "Product3",
        Price = 300,
        Time = 3,
        Manufacturer = _unitOfWork.ManufacturerRepository.GetByID(3),
        Category = _unitOfWork.CategoryRepository.GetByID(1)
      };

      Product p4 = new Product {
        Link = "www.product4.com",
        Description = "Product4",
        Price = 400,
        Time = 4,
        Manufacturer = _unitOfWork.ManufacturerRepository.GetByID(2),
        Category = _unitOfWork.CategoryRepository.GetByID(2)
      };

      Product p5 = new Product {
        Link = "www.product5.com",
        Description = "Product5",
        Price = 500,
        Time = 5,
        Manufacturer = _unitOfWork.ManufacturerRepository.GetByID(1),
        Category = _unitOfWork.CategoryRepository.GetByID(3)
      };

      Product p6 = new Product {
        Link = "www.product6.com",
        Description = "Product6",
        Price = 600,
        Time = 6,
        Manufacturer = _unitOfWork.ManufacturerRepository.GetByID(3),
        Category = _unitOfWork.CategoryRepository.GetByID(2)
      };

      Product p7 = new Product {
        Link = "www.product7.com",
        Description = "Product7",
        Price = 700,
        Time = 7,
        Manufacturer = _unitOfWork.ManufacturerRepository.GetByID(2),
        Category = _unitOfWork.CategoryRepository.GetByID(3)
      };

      _unitOfWork.ItemRepository.Add(p1);
      _unitOfWork.ItemRepository.Add(p2);
      _unitOfWork.ItemRepository.Add(p3);
      _unitOfWork.ItemRepository.Add(p4);
      _unitOfWork.ItemRepository.Add(p5);
      _unitOfWork.ItemRepository.Add(p6);
      _unitOfWork.ItemRepository.Add(p7);
      _unitOfWork.Save();
    }

    // ID starts from 8.
    public void SeedModules() {
      Module m1 = new Module {
        Description = "Module1",
        Price = 100,
        Time = 1
      };
      _unitOfWork.ItemRepository.Add(m1);

      m1.ContainerItems.Add(new ContainerItem(m1, _unitOfWork.ItemRepository.GetByID(1)));
      m1.ContainerItems.Add(new ContainerItem(m1, _unitOfWork.ItemRepository.GetByID(2)));

      m1.ContainerItems[0].Quantity = 4;
      m1.ContainerItems[1].Quantity = 55;

      Module m2 = new Module {
        Description = "Module2",
        Price = 200,
        Time = 2
      };
      _unitOfWork.ItemRepository.Add(m2);

      m2.ContainerItems.Add(new ContainerItem(m2, _unitOfWork.ItemRepository.GetByID(1)));
      m2.ContainerItems.Add(new ContainerItem(m2, _unitOfWork.ItemRepository.GetByID(6)));
      m2.ContainerItems.Add(new ContainerItem(m2, _unitOfWork.ItemRepository.GetByID(4)));
      m2.ContainerItems.Add(new ContainerItem(m2, _unitOfWork.ItemRepository.GetByID(3)));

      m2.ContainerItems[0].Quantity = 7;
      m2.ContainerItems[1].Quantity = 4;
      m2.ContainerItems[2].Quantity = 8;
      m2.ContainerItems[3].Quantity = 2;

      _unitOfWork.Save();

      ;
    }

    // ID starts from 6.
    public void SeedBlocks() {
      Block b1 = new Block {
        Description = "Block1",
        Price = 200,
        Time = 1
      };
      _unitOfWork.ItemRepository.Add(b1);

      b1.ContainerItems.Add(new ContainerItem(b1, _unitOfWork.ItemRepository.GetByID(5)));
      b1.ContainerItems.Add(new ContainerItem(b1, _unitOfWork.ItemRepository.GetByID(2)));

      b1.ContainerItems[0].Quantity = 4;
      b1.ContainerItems[1].Quantity = 55;

      Block b2 = new Block {
        Description = "Block2",
        Price = 100,
        Time = 1
      };
      _unitOfWork.ItemRepository.Add(b2);
      b2.ContainerItems.Add(new ContainerItem(b2, _unitOfWork.ItemRepository.GetByID(1)));
      b2.ContainerItems.Add(new ContainerItem(b2, _unitOfWork.ItemRepository.GetByID(9)));
      b2.ContainerItems.Add(new ContainerItem(b2, _unitOfWork.ItemRepository.GetByID(2)));
      b2.ContainerItems.Add(new ContainerItem(b2, _unitOfWork.ItemRepository.GetByID(5)));

      b2.ContainerItems[0].Quantity = 7;
      b2.ContainerItems[1].Quantity = 4;
      b2.ContainerItems[2].Quantity = 8;
      b2.ContainerItems[3].Quantity = 2;

      _unitOfWork.Save();
    }

    // ID starts from 1.
    public void SeedPanels() {
      Panel pl1 = new Panel() {
        Description = "Panel1",
        Type = PanelType.InsulationPlastic
      };

      pl1.PanelItems.Add(new PanelItem(pl1, _unitOfWork.ItemRepository.GetByID(2)));
      pl1.PanelItems[0].Quantity = 4;
      pl1.PanelItems.Add(new PanelItem(pl1, _unitOfWork.ItemRepository.GetByID(5)));
      pl1.PanelItems[1].Quantity = 2;
      pl1.PanelItems.Add(new PanelItem(pl1, _unitOfWork.ItemRepository.GetByID(6)));
      pl1.PanelItems[2].Quantity = 5;

      _unitOfWork.PanelRepository.Add(pl1);

      Panel pl2 = new Panel() {
        Description = "Panel2",
        Type = PanelType.InsulationPlastic
      };

      pl2.PanelItems.Add(new PanelItem(pl2, _unitOfWork.ItemRepository.GetByID(10)));
      pl2.PanelItems[0].Quantity = 4;
      pl2.PanelItems.Add(new PanelItem(pl2, _unitOfWork.ItemRepository.GetByID(7)));
      pl2.PanelItems[1].Quantity = 2;
      pl2.PanelItems.Add(new PanelItem(pl2, _unitOfWork.ItemRepository.GetByID(1)));
      pl2.PanelItems[2].Quantity = 6;

      _unitOfWork.PanelRepository.Add(pl2);

      _unitOfWork.Save();
    }

    // ID starts from 1
    private void SeedCustomers() {
      CustomerInfo customerInfo1 = new CustomerInfo {
        Name = "Customer 1",
        PhoneNumber = "+45 12 34 56 78",
        BillingAddress = "Address",
        BillingZipCode = "5000",
        Email = "Customer1@email.com",
        ShippingAddress = "Address",
        ShippingZipCode = "5000"
      };

      Customer c1 = new Customer();
      c1.CustomerInfo = customerInfo1;

      _unitOfWork.CustomerRepository.Add(c1);

      CustomerInfo customerInfo2 = new CustomerInfo {
        Name = "Customer 2",
        PhoneNumber = "+45 12 34 56 78",
        BillingAddress = "Address",
        BillingZipCode = "9000",
        Email = "Customer2@email.com",
        ShippingAddress = "Address",
        ShippingZipCode = "9000"
      };

      Customer c2 = new Customer();
      c2.CustomerInfo = customerInfo2;

      _unitOfWork.CustomerRepository.Add(c2);

      CustomerInfo customerInfo3 = new CustomerInfo {
        Name = "Customer 3",
        PhoneNumber = "+45 12 34 56 78",
        BillingAddress = "Address",
        BillingZipCode = "8100",
        Email = "Customer3@email.com",
        ShippingAddress = "Address",
        ShippingZipCode = "8100"
      };

      Customer c3 = new Customer();
      c3.CustomerInfo = customerInfo3;

      _unitOfWork.CustomerRepository.Add(c3);

      _unitOfWork.Save();
    }

    public void SeedQuotations() {
      Quotation q1 = new Quotation(new QuotationStatus(DateTime.Now, QuotationStatusType.Created)) {
        Description = "Quotation1",
        Deadline = new DateTime(2020, 3, 28),
        Customer = _unitOfWork.CustomerRepository.GetByID(1),
        Employee = "Name1"
      };

      q1.Panels.Add(_unitOfWork.PanelRepository.GetByID(1));
      q1.Panels.Add(_unitOfWork.PanelRepository.GetByID(2));

      _unitOfWork.QuotationRepository.Add(q1);

      Quotation q2 = new Quotation(new QuotationStatus(DateTime.Now, QuotationStatusType.Created)) {
        Description = "Quotation2",
        Deadline = new DateTime(2020, 2, 15),
        Customer = _unitOfWork.CustomerRepository.GetByID(2),
        Employee = "Name2"
      };

      _unitOfWork.QuotationRepository.Add(q2);

      _unitOfWork.Save();
    }

    // ID starts from 1.
    public void SeedTemplates() {
      Template t1 = new Template() {
        Description = "Template1",
      };

      t1.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(4)));
      t1.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(5)));
      t1.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(6)));
      t1.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(7)));
      t1.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(8)));
      t1.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(9)));
      t1.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(10)));

      _unitOfWork.TemplateRepository.Add(t1);

      Template t2 = new Template() {
        Description = "Template2",
      };

      t2.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(1)));
      t2.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(2)));
      t2.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(3)));
      t2.TemplateItems.Add(new TemplateItem(t1, _unitOfWork.ItemRepository.GetByID(11)));

      _unitOfWork.TemplateRepository.Add(t2);

      _unitOfWork.Save();
    }
  }
}