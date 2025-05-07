using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace WinUI.Helpers
{
    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type target_type, object parameter, string language)
        {
            if (value is DateTime date_time)
            {
                return new DateTimeOffset(date_time);
            }
            return DateTimeOffset.Now;
        }
        public object ConvertBack(object value, Type target_type, object parameter, string language)
        {
            if (value is DateTimeOffset date_time_offset)
            {
                return date_time_offset.DateTime;
            }
            return DateTime.Now;
        }
    }
}