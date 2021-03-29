using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;
using System.Configuration;

namespace ElogicSystem.DataAccess {

  /// <summary>
  /// The data context of the application. Sets up the database and provide functionality to
  /// access the database.
  /// </summary>
  public class ElogicSystemContext : DbContext {
    private string _connectionString;

    // Relationship entities.
    public DbSet<ContainerItem> ContainerItems { get; set; }

    public DbSet<PanelItem> PanelItems { get; set; }
    public DbSet<TemplateItem> TemplateItems { get; set; }

    // Subclasses of Item.
    public DbSet<Product> Products { get; set; }

    public DbSet<Module> Modules { get; set; }
    public DbSet<Block> Blocks { get; set; }

    public DbSet<Panel> Panels { get; set; }
    public DbSet<Template> Templates { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Quotation> Quotations { get; set; }

    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ElogicSystemContext() {
      _connectionString = "Data Source=.\\ElogicSystemDB.db;";
    }

    /// <summary>
    /// Configure Db context. Sets the connection string.
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseLazyLoadingProxies().UseSqlite(_connectionString);
    }

    /// <summary>
    /// Configure database schema.
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      // Define compostite key for RelationItem entities.
      modelBuilder.Entity<ContainerItem>().HasKey(c => new { c.ContainerID, c.ItemID });
      modelBuilder.Entity<TemplateItem>().HasKey(t => new { t.TemplateID, t.ItemID });
      modelBuilder.Entity<PanelItem>().HasKey(p => new { p.PanelID, p.ItemID });

      // Define relations.
      modelBuilder.Entity<Customer>().OwnsOne(q => q.CustomerInfo); // Move to same table

      // Make item table for derived classes of class Item.
      modelBuilder.Entity<Item>().ToTable("Items");

      // Ignore TemplateItem quantity as should not be saved.
      modelBuilder.Entity<TemplateItem>().Ignore("Quantity");
    }
  }
}