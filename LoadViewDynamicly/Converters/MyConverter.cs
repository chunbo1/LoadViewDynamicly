using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace LoadViewDynamicly.Converters
{
    public class CommonReportNumberFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String column = value[0] as String;

            //if ("market_prc_nat".Equals(column) || "fx_rate".Equals(column))
            if (!string.IsNullOrEmpty(column))
            {
                if (column.Contains("_pct") || column.Contains("volatility"))
                    return "P2";
                else if (column.Contains("CurrentDay") || column.Contains("PrevDay") || column.Contains("Diff") || column.Contains("varInPercent") || column.Contains("Last") || column.Contains("f_last") || column.Contains("Bid") || column.Contains("f_bid") || column.Contains("Ask") || column.Contains("f_ask"))
                    return "N4";
                else if (column.Contains("_prc") || column.Contains("_rate") || column.Contains("_strike") || column.Contains("_factor"))
                    return "N4";
                else if (column.ToUpper().Contains("PRICE") || column.ToUpper().Contains("BID") || column.ToUpper().Contains("ASK") || column.ToUpper().Contains("MID") || column.ToUpper().Contains("VAL1") || column.ToUpper().Contains("VAL2"))
                    return "N4";
                else
                    return "N2";
            }
            else
                return "N0";
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }//CommonReportNumberFormatConverter


    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String format = "N0";
            if (parameter != null)
                format = parameter.ToString();

            double d;
            if (value is double)
            {
                d = (double)value;
                if (double.IsNaN(d))
                    return null;
                else
                    return d.ToString(format);
            }
            else if (value != null && double.TryParse(value.ToString(), out d))
            {
                return d.ToString(format);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double d;
            if (double.TryParse(value.ToString(), out d))
                return d;
            else
                return null;
        }
    }//NumberConverter


    public class PermissionToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isPermissioned = (bool)value;
            if (isPermissioned)
                return new SolidColorBrush(Colors.Green);
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

   
}
