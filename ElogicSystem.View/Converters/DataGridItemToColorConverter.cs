using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ElogicSystem.ViewModel;

namespace ElogicSystem.View {

  public class DataGridItemToColorConverter : IValueConverter {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      byte level = (byte)(255 - ((int)value * 20));

      return new SolidColorBrush(Color.FromRgb(level, level, level));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}