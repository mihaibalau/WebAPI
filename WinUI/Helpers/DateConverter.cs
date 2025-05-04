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
        public object Convert(object _value, Type _target_type, object _parameter, string _language)
        {
            if (_value is DateTime date_time)
            {
                return new DateTimeOffset(date_time);
            }
            return DateTimeOffset.Now;
        }
        public object ConvertBack(object _value, Type _target_type, object _parameter, string _language)
        {
            if (_value is DateTimeOffset date_time_offset)
            {
                return date_time_offset.DateTime;
            }
            return DateTime.Now;
        }
    }
}