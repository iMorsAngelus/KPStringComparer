using KPStringComparer.DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace KPStringComparer.PresentationLayer.Converter
{
    /// <summary>
    /// Converter for list box items.
    /// </summary>
    public class MainStringConverter : BaseConvertor<MainStringConverter>
    {
        /// <summary>
        /// This method converts value from terminal info to title.
        /// </summary>
        /// <param name="value">Value for convert.</param>
        /// <param name="targetType">Value type.</param>
        /// <param name="parameter">Parametr for converting.</param>
        /// <param name="culture">Regional standart info.</param>
        /// <returns>Char array of string to display.</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mainString = (List<ViewString>)value;
            
            return mainString;
        }
    }
}
