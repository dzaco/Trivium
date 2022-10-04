using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using Trivium.Models;

namespace Trivium.Views.Helpers
{
    public class HexConverter : IValueConverter
    {
        private string lastValidValue = string.Empty;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ret = null;

            if (value != null && value is string)
            {
                var valueAsString = (string)value;
                var parts = valueAsString.ToCharArray();
                var formatted = parts.Select((p, i) => (++i) % 2 == 0 ? String.Concat(p.ToString(), " ") : p.ToString());
                ret = String.Join(String.Empty, formatted).Trim();
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object ret = null;
            if (value != null && value is string)
            {
                var combobox = parameter as ComboBox;
                var length = (int)combobox.SelectedValue / 8;
                var valueAsString = ((string)value).Replace(" ", String.Empty).ToUpper();
                if ((decimal)valueAsString.Length / 2 > length)
                    return lastValidValue;

                ret = lastValidValue = IsHex(valueAsString) ? valueAsString : lastValidValue;
            }

            return ret;
        }

        private bool IsHex(string text)
        {
            var reg = new Regex("^[0-9A-Fa-f]*$");
            return reg.IsMatch(text);
        }
    }
}