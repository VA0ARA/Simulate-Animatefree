using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace FrameWork;

public static class PersianDateExtensionMethods
{
        private static CultureInfo? _Culture;
        public static CultureInfo GetPersianCulture()
        {
            if (_Culture == null)
            {
                _Culture = new CultureInfo("fa-IR");
                DateTimeFormatInfo formatInfo = _Culture.DateTimeFormat;
                formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
                formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
                var monthNames = new[]
                {
                    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
                    "اسفند",
                    ""
                };
                formatInfo.AbbreviatedMonthNames =
                    formatInfo.MonthNames =
                    formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
                formatInfo.AMDesignator = "ق.ظ";
                formatInfo.PMDesignator = "ب.ظ";
                formatInfo.ShortDatePattern = "yyyy/MM/dd";
                formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
                formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
                System.Globalization.Calendar cal = new PersianCalendar();

                FieldInfo? fieldInfo = _Culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo != null)
                    fieldInfo.SetValue(_Culture, cal);

                FieldInfo? info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (info != null)
                    info.SetValue(formatInfo, cal);

                _Culture.NumberFormat.NumberDecimalSeparator = "/";
                _Culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
                _Culture.NumberFormat.NumberNegativePattern = 0;
            }
            return _Culture;
        }
                public static string ToPersianString(this DateTime date, string format = "yyyy/MM/dd")
        {
            return date.ToString(format, GetPersianCulture());
        }
//var date1 = DateTime.Now.ToPeString("yyyy/MM/dd");
//var date2 = DateTime.Now.ToPeString("dddd, dd MMMM,yyyy");


//var date1 = DateTime.Now.ToPersianString("yyyy/MM/dd");
// Output: 1391/12/13

//var date2 = DateTime.Now.ToPersianString("dddd, dd MMMM,yyyy");
// Output: یکشنبه, 13 اسفند,1391

////Output:
////1391/12/13
////یکشنبه, 13 اسفند,1391
}
