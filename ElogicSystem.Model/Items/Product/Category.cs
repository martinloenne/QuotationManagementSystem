using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  public class Category {
    public int ID { get; set; }
    public string Name { get; set; }

    /// <summary>
    /// Installation time in hours for products in this category.
    /// </summary>
    public double Time { get; set; }
  }
}