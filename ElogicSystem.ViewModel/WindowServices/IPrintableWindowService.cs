using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElogicSystem.ViewModel {
  public interface IPrintableWindowService : IWindowService {
    void PrintPDFDocument();

  }
}
