using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Helpers;
public static class CommonHelper
{
  public static T To<T>(object value)
  {
    //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
    return (T)To(value, typeof(T));
  }
  public static object To(object value, Type destinationType)
  {
    return To(value, destinationType, CultureInfo.InvariantCulture)!;
  }
  public static object? To(object value, Type destinationType, CultureInfo culture)
  {
    if (value == null)
      return null;

    var sourceType = value.GetType();

    var destinationConverter = TypeDescriptor.GetConverter(destinationType);
    if (destinationConverter.CanConvertFrom(value.GetType()))
      return destinationConverter.ConvertFrom(null, culture, value)!;

    var sourceConverter = TypeDescriptor.GetConverter(sourceType);
    if (sourceConverter.CanConvertTo(destinationType))
      return sourceConverter.ConvertTo(null, culture, value, destinationType)!;

    if (destinationType.IsEnum && value is int)
      return Enum.ToObject(destinationType, (int)value);

    if (!destinationType.IsInstanceOfType(value))
      return Convert.ChangeType(value, destinationType, culture);

    return value;
  }
  public static string EnsureNotNull(string? str)
  {
    return str ?? string.Empty;
  }
  public static int EnsureNotNull(int? num)
  {
    return num ?? 0;
  }
  public static string EnsureMaximumLength(string str, int maxLength, string? postfix = null)
  {
    if (String.IsNullOrEmpty(str))
      return str;

    if (str.Length > maxLength)
    {
      var pLen = postfix == null ? 0 : postfix.Length;

      var result = str.Substring(0, maxLength - pLen);
      if (!String.IsNullOrEmpty(postfix))
      {
        result += postfix;
      }
      return result;
    }

    return str;
  }
  public static string MapPath(string path)
  {
    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
    return Path.Combine(baseDirectory, path);
  }
  public static string RoundAbs(double value)
  {
    return String.Format("{0:#,0.0}", Math.Round(Math.Abs(value), 1, MidpointRounding.AwayFromZero)).Replace(".0", "");
  }

  public static string StripHTML(string input)
  {
    return Regex.Replace(input, @"<[^>]+>|&nbsp;|&reg;|&lt;|&gt;|&amp;|&quot;|&apos;|&cent;|&pound;|&yen;|&euro;|&copy;", String.Empty);
  }

  public static string ConvertDateRemain(DateTime endDateInput, DateTime dateNowUtc)
  {
    string result = "";


    double daysRemain = 0;
    double hoursRemain = 0;
    //if (endDateInput.Year < dateNowUtc.Year)
    //{
    //  return result;
    //}

    daysRemain = (endDateInput - dateNowUtc).TotalDays;
    if (daysRemain < 0)
    {
      daysRemain = 0;
      hoursRemain = 0;
    }
    else
    {
      hoursRemain = (endDateInput.Hour - dateNowUtc.Hour);
      hoursRemain = (daysRemain - Math.Floor(daysRemain)) * 24;
    }
    result = Math.Floor(daysRemain).ToString() + "日" + Math.Floor(hoursRemain).ToString() + "時間";
    //daysRemain = endDateInput.Day - dateNowUtc.Day >= 0 ? endDateInput.Day - dateNowUtc.Day : 0;

    //hoursRemain = endDateInput.Hour - dateNowUtc.Hour >= 0 && endDateInput.Day - dateNowUtc.Day >= 0 ? endDateInput.Hour - dateNowUtc.Hour : 0;

    //if (endDateInput.Hour - dateNowUtc.Hour < 0 && endDateInput.Day - dateNowUtc.Day > 0)
    //{
    //  daysRemain -= 1;
    //}

    return result;
  }

  public static string ChangeAlias(this string alias)
  {
    if (alias == null)
    {
      return "";
    }
    var str = alias;
    str = str.ToLower();
    str = Regex.Replace(str, @"à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ", "a");
    str = Regex.Replace(str, @"è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ", "e");
    str = Regex.Replace(str, @"ì|í|ị|ỉ|ĩ", "i");
    str = Regex.Replace(str, @"ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ", "o");
    str = Regex.Replace(str, @"ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ", "u");
    str = Regex.Replace(str, @"ỳ|ý|ỵ|ỷ|ỹ", "y");
    str = Regex.Replace(str, @"đ", "d");
    str = Regex.Replace(str, @"!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\&|\#|\[|\]|~|\$|_|`|´|-|{|}|\||\\", " ");
    str = Regex.Replace(str, @"""", " ");
    str = Regex.Replace(str, @" +", " ");
    str = str.Trim();
    return str.NonUnicode();
  }

  public static string NonUnicode(this string input)
  {
    var ConvertToUnsign_rg = new Regex("\\p{IsCombiningDiacriticalMarks}+");
    var temp = input.Normalize(NormalizationForm.FormD);
    return ConvertToUnsign_rg.Replace(temp, string.Empty).Replace("đ", "d").Replace("Đ", "D");
  }

  //public static string CreateSlug(this string input)
  //{

  //}

  private static readonly string[] VietnameseSigns = new string[]
  {
    "aAeEoOuUiIdDyY",
    "áàạảãâấầậẩẫăắằặẳẵ",
    "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
    "éèẹẻẽêếềệểễ",
    "ÉÈẸẺẼÊẾỀỆỂỄ",
    "óòọỏõôốồộổỗơớờợởỡ",
    "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
    "úùụủũưứừựửữ",
    "ÚÙỤỦŨƯỨỪỰỬỮ",
    "íìịỉĩ",
    "ÍÌỊỈĨ",
    "đ",
    "Đ",
    "ýỳỵỷỹ",
    "ÝỲỴỶỸ"
  };
  public static string CreateSlug(string str)
  {
    for (int i = 1; i < VietnameseSigns.Length; i++)
    {
      for (int j = 0; j < VietnameseSigns[i].Length; j++)
        str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
    }
    return str;
  }
}


