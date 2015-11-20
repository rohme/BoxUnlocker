using System;
using System.Windows.Data;

namespace BoxUnlocker.Converter
{
    [ValueConversion(typeof(int), typeof(string))]
    public class MumRemainCountConverter : IValueConverter
    {
        public object Convert(object iValue, Type iTargetType, object iParameter, System.Globalization.CultureInfo iCulture)
        {
            int? val = iValue as int?;
            if (val == null || val == 0)
                return string.Empty;
            else
                return string.Format("残り{0}回", val);
        }
        public object ConvertBack(object iValue, Type iTargetType, object iParameter, System.Globalization.CultureInfo iCulture)
        {
            throw new NotImplementedException();
        }
    }
}
