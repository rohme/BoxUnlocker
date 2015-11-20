using System;
using System.Windows.Data;

namespace BoxUnlocker.Converter
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object iValue, Type iTargetType, object iParameter, System.Globalization.CultureInfo iCulture)
        {
            return converter(iValue);
        }
        public object ConvertBack(object iValue, Type iTargetType, object iParameter, System.Globalization.CultureInfo iCulture)
        {
            return converter(iValue);
        }
        private bool converter(object iValue)
        {
            if (!(iValue is bool)) throw new InvalidOperationException("ターゲットはboolを指定すること");
            return !(bool)iValue;
        }
    }
}
