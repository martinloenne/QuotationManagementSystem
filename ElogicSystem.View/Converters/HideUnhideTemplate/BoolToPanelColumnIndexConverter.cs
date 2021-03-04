using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace ElogicSystem.View {

  /// <summary>
  /// Converts a bool from a view model, to the index number of the column for the panel data grid in the view.
  /// </summary>
  public class BoolToPanelColumnIndexConverter : IValueConverter {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      if((bool)value == true) {
        return 0;
      }
      else {
        return 2;
      }
    }

    // Not used, but is required by interface.
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}