using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ElogicSystem.View {

  /// <summary>
  /// Converts a bool from a view model to a visibility that determines whether the template datagrid
  /// is visible or not in the view. Returns the opposite bool of the <see cref="BoolToTemplateVisibilityConverter"./>
  /// </summary>
  internal class BoolToTemplateVisibilityInvertedConverter : IValueConverter {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      if ((bool)value == true) {
        return Visibility.Visible;
      }
      else {
        return Visibility.Collapsed;
      }
    }

    // Not used, but is required by interface.
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}