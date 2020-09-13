using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Common
{
    public static class FilterParameter
    {
        public static Guid GuidPars(string gui)
        {
            Guid result = Guid.Empty;
            Guid.TryParse(gui, out result);
            return result;
        }
        public static string DecimalSql(decimal Value)
        {
            string value = Value.ToString();
            if (value.Length == 0)
                return "0";
            else
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                System.Globalization.CultureInfo ci = (CultureInfo)culture;
                string sp = ci.NumberFormat.CurrencyDecimalSeparator;
                return value.Replace(",", sp);
            }
        }

        public static decimal ToDecimal(this string value)
        {
            if (value == "")
                return 0;
            if (value == null)
                return 0;
            if (value == "&nbsp;")
                return 0;
            decimal number;
            string tempValue = value;

            var punctuation = value.Where(x => char.IsPunctuation(x)).Distinct();
            int count = punctuation.Count();

            NumberFormatInfo format = CultureInfo.InvariantCulture.NumberFormat;
            switch (count)
            {
                case 0:
                    break;
                case 1:
                    tempValue = value.Replace(",", ".");
                    break;
                case 2:
                    if (punctuation.ElementAt(0) == '.')
                        tempValue = value.SwapChar('.', ',');
                    break;
                default:
                    throw new InvalidCastException();
            }

            number = decimal.Parse(tempValue, format);
            return number;
        }

        public static string SwapChar(this string value, char from, char to)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            StringBuilder builder = new StringBuilder();

            foreach (var item in value)
            {
                char c = item;
                if (c == from)
                    c = to;
                else if (c == to)
                    c = from;

                builder.Append(c);
            }
            return builder.ToString();
        }
        public static string DateTimeToMinSQL(DateTime dateTime)
        {
            return dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day + " 00:00:00.000";
        }

        public static string DateTimeToMaxSQL(DateTime dateTime)
        {
            return dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day + " 23:59:59.000";
        }
        public static DateTime? DateMinFilter(DateTime? dateTime)
        {
            if (dateTime == null)
                return null;
            if (dateTime == DateTime.MinValue)
                return null;
            return DateTime.Parse(dateTime.Value.Year + "-" + dateTime.Value.Month + "-" + dateTime.Value.Day + " 00:00:00.000");
        }

        public static DateTime? DateMaxFilter(DateTime? dateTime)
        {
            if (dateTime == null)
                return null;
            if (dateTime == DateTime.MaxValue)
                return null;
            return DateTime.Parse(dateTime.Value.Year + "-" + dateTime.Value.Month + "-" + dateTime.Value.Day + " 23:59:59.000");
        }


        public static DateTime? DateFilter(string value)
        {
            DateTime dateOk;
            string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
                    "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy","dd.MM.yyyy", "dd.M.yyyy", "d.M.yyyy", "d.MM.yyyy",
                    "dd.MM.yy", "dd.M.yy", "d.M.yy", "d.MM.yy","MM/dd/yyyy","MM.dd.yyyy"};
            DateTime.TryParseExact(value, formats, null, System.Globalization.DateTimeStyles.None, out dateOk);
            return dateOk;
        }

        public static DateTime? DateFilterForSQL(string value)
        {
            if (value == null)
                return null;
            else if (value == DateTime.MinValue.ToString())
                return null;
            else if (value == string.Empty)
                return null;
            else
            {
                return Convert.ToDateTime(DateTime.Parse(value).ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        public static String StringFilter(string value)
        {
            if (value == "")
                return null;
            if (value == "&nbsp;")
                return null;
            if (value == string.Empty)
                return null;
            return (value);
        }

        public static String GuidFilter(string value)
        {
            if (value == "&nbsp;")
                return null;
            if (value == Guid.Empty.ToString())
                return null;
            if (value == "")
                return null;
            try
            {
                return Guid.Parse(value).ToString();
            }
            catch
            {
                return null;
            }
        }

        public static short? ShortFilter(string value)
        {
            if (value == "0")
                return null;
            if (value == string.Empty)
                return null;
            return Convert.ToInt16(value);
        }
        public static decimal? DecimalFilter(string value)
        {
            if (value == string.Empty)
                return null;
            return Convert.ToDecimal(value);
        }

        public static int? IntFilter(string value)
        {
            if (value == string.Empty)
                return null;
            if (value == "&nbsp;")
                return null;
            return Convert.ToInt32(value);
        }

        public static bool? BoolFilter(string value)
        {
            if (value == "")
                return false;
            if (value == string.Empty)
                return false;
            if (value == "&nbsp;")
                return false;
            return bool.Parse(value);
        }

        public static short? EnumFilter(int value)
        {
            if (value == 0)
                return null;
            return Convert.ToInt16(value);
        }

        public static int SetEnum(short? value)
        {
            if (value == null)
                return 0;
            return (int)value;
        }

        public static bool BoolIsNull(bool? value)
        {
            if (value == null)
                return false;
            return (bool)value;
        }

        public static decimal? DecimalIsNull(decimal? value)
        {
            if (value == null)
                return 0;
            return value;
        }

        public static DateTime? DateTimeIsNull(DateTime? value)
        {
            if (value == null)
                return DateTime.MinValue;
            return value;
        }

        public static string StringIsNull(string value)
        {
            if (value == null)
                return "";
            return value;
        }

        public static int? IntIsNull(int? value)
        {
            if (value == null)
                return 0;
            return value;
        }

        public static decimal StringToDecimal(string value)
        {
            if (value == "&nbsp;")
                return 0;
            if (value == "")
                return 0;
            if (value == null)
                return 0;
            return decimal.Parse(value);
        }

        public static float StringToFloat(string value)
        {
            if (value == "&nbsp;")
                return 0;
            if (value == "")
                return 0;
            if (value == null)
                return 0;
            return float.Parse(value);
        }

        public static short StringToShort(string value)
        {
            if (value == "&nbsp;")
                return 0;
            if (value == "")
                return 0;
            if (value == null)
                return 0;
            return short.Parse(value);
        }

        public static int StringToInt(string value)
        {
            if (value == "&nbsp;")
                return 0;
            if (value == "")
                return 0;
            if (value == null)
                return 0;
            return int.Parse(value);
        }

        public static DateTime? StringToDate(string day, string mount, string year)
        {
            if (day == null)
                return null;
            else
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                System.Globalization.CultureInfo ci = (CultureInfo)culture;
                string sp = ci.DateTimeFormat.DateSeparator;

                return DateTime.Parse(year + sp + mount + sp + day, ci);
            }
        }

        public static string ConvertToShortDate(DateTime? date)
        {
            DateTime? ddate = date;
            string retdate = null;

            if (date != null)
                ddate = FilterParameter.DateFilter(date.ToString());

            if (ddate != null)
                retdate = String.Format("{0:dd/MM/yyyy}", ddate).Replace(" 00:00:00", "");

            return retdate;
        }
        public static string ToSafeString(this object obj)
        {
            return (obj ?? string.Empty).ToString();
        }
        public static Decimal ToSafeDecimal(this object obj)
        {
            decimal result = Decimal.Zero;

            if (obj != null)
            {
                var nds = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                var ngs = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator[0];

                var objAsStr = obj.ToString();

                var nds_cnt = objAsStr.Count(x => x == nds);
                var ngs_cnt = objAsStr.Count(x => x == ngs);

                var nds_ind = objAsStr.LastIndexOf(nds);
                var ngs_ind = objAsStr.LastIndexOf(ngs);

                if ((nds_cnt * ngs_cnt > 0 && nds_cnt > ngs_cnt) || (nds_cnt == 1 && ngs_cnt == 1 && ngs_ind > nds_ind) || (nds_ind < 0 && (objAsStr.Length - (ngs_ind + 1)) % 3 > 0)) //decimalSeperator ile groupSeperator ters kullanılmış. Değiştiriyoruz.
                {
                    objAsStr = objAsStr.Replace(ngs.ToString(), "|$|").Replace(nds, ngs).Replace("|$|", nds.ToString());
                }

                Decimal.TryParse(objAsStr, out result);
            }

            return result;
        }

        public static short ToSafeShort(this object obj)
        {
            return Convert.ToInt16((obj ?? 0));
        }
        public static string ToSafeGuidStr(this object obj)
        {
            return (obj ?? Guid.Empty).ToString();
        }
        public static Guid ToSafeGuid(this object obj)
        {
            return (Guid)(obj ?? null);
        }
        public static DateTime ToSafeDateTime(this object obj)
        {
            return (DateTime)(obj ?? null);
        }
        public static int? ToSafeInt(this object obj)
        {
            return (int?)(obj ?? null);
        }
        public static int ToSafeIntNotNull(this object obj)
        {
            return Convert.ToInt32(obj ?? 0);
        }
        public static long? ToSafeLong(this object obj)
        {
            return (long?)(obj ?? null);
        }
        public static long ToSafeLongNotNull(this object obj)
        {
            return (long)(obj ?? 0);
        }
        public static bool ToSafeBool(this object obj)
        {
            return (bool)(obj ?? false);
        }
    }
}
