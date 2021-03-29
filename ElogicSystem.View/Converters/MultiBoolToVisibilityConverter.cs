using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ElogicSystem.View {

  public class MultiBoolToVisibilityConverter : IMultiValueConverter {

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
      bool visible = true;
      foreach (object value in values)
        if (value is bool)
          visible = visible && (bool)value;

      if (visible)
        return Visibility.Visible;
      else
        return Visibility.Hidden;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}