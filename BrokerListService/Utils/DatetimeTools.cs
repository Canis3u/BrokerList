using System.Globalization;
using System;

namespace BrokerListService.Utils
{
    public class DatetimeTools
    {
        public static DateTime? GetDatetimeFromZHTWFormat(string datetimeString)
        {
            CultureInfo culture = new CultureInfo("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            DateTime date;
            if (String.IsNullOrEmpty(datetimeString)) return null;
            if (!datetimeString.Contains('/')) // format: yyyyMMdd
            {
                if (datetimeString.Length < 8) datetimeString = "0" + datetimeString;
                try
                {
                    date = DateTime.ParseExact(datetimeString, "yyyyMMdd", culture);
                }
                catch (Exception)
                {
                    throw new Exception($"{datetimeString}, date Format Error.");
                }
            }
            else // format: yyyy/MM/dd
            {
                try
                {
                    date = DateTime.Parse(datetimeString, culture);
                }
                catch (Exception)
                {
                    throw new Exception($"{datetimeString}, date Format Error.");
                }
            }
            return date;
        }
        public static string GetZHTWDatetimeString(DateTime datetime)
        {
            CultureInfo culture = new CultureInfo("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return datetime.ToString("yyy/MM/dd",culture);
        }
    }
}
