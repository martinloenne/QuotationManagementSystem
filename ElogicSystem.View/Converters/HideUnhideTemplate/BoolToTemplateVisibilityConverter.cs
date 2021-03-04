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
  /// Converts a bool from a view model to a visibility that determines whether the template datagrid
  /// is visible or not in the view.
  /// </summary>
  public class BoolToTemplateVisibilityConverter : IValueConverter {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      if((bool)value == true) {
        return Visibility.Collapsed;
      }
      else {
        return Visibility.Visible;
      }
    }

    // Not used, but is required by interface.
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}