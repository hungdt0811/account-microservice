using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Account.Microservice.Core.Helpers;
public static class ConvertHelper
{
  /// <summary>
  /// Converts a value to a destination type.
  /// </summary>
  /// <param name="value">The value to convert.</param>
  /// <param name="destinationType">The type to convert the value to.</param>
  /// <returns>The converted value.</returns>
  public static object To(this object value, Type destinationType)
  {
    return To(value, destinationType, CultureInfo.InvariantCulture);
  }

  /// <summary>
  /// Converts a value to a destination type.
  /// </summary>
  /// <param name="value">The value to convert.</param>
  /// <param name="destinationType">The type to convert the value to.</param>
  /// <param name="culture">Culture</param>
  /// <returns>The converted value.</returns>
  public static object To(this object value, Type destinationType, CultureInfo culture)
  {
    if (value != null)
    {
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
    }
    return value!;
  }


  /// <summary>
  /// Chuyển đổi 1 giá trị thành 1 kiểu khác
  /// </summary>
  /// <param name="value">Giá trị cần chuyển đổi</param>
  /// <typeparam name="T">Kiểu dữ liệu mới cần chuyển đổi</typeparam>
  /// <returns>Giá trị đã chuyển đổi</returns>
  public static T To<T>(this object value)
  {
    return (T)To(value, typeof(T));
  }

  /// <summary>
  /// Convert enum for front-end
  /// </summary>
  /// <param name="str">Input string</param>
  /// <returns>Converted string</returns>
  public static string ConvertEnum(string str)
  {
    if (String.IsNullOrEmpty(str)) return String.Empty;
    string result = String.Empty;
    foreach (var c in str)
      if (c.ToString() != c.ToString().ToLower())
        result += " " + c.ToString();
      else
        result += c.ToString();

    //ensure no spaces (e.g. when the first letter is upper case)
    result = result.TrimStart();
    return result;
  }

  public static int AsInt(this object item, int defaultInt = default(int))
  {
    if (item == null)
      return defaultInt;

    int result;
    if (!int.TryParse(item.ToString(), out result))
      return defaultInt;

    return result;
  }
  public static long AsLong(this object item, long defaultInt = default(long))
  {
    if (item == null)
      return defaultInt;

    long result;
    if (!long.TryParse(item.ToString(), out result))
      return defaultInt;

    return result;
  }

  // transform object into double data type

  public static double AsDouble(this object item, double defaultDouble = default(double))
  {
    if (item == null)
      return defaultDouble;

    double result;
    if (!double.TryParse(item.ToString(), out result))
      return defaultDouble;

    return result;
  }
  public static decimal AsDecimal(this object item, decimal defaultDecimal = default(decimal))
  {
    if (item == null)
      return defaultDecimal;

    decimal result;
    if (!decimal.TryParse(item.ToString(), out result))
      return defaultDecimal;

    return result;
  }
  public static short AsShort(this object item, short defaultShort = default(short))
  {
    if (item == null)
      return defaultShort;

    short result;
    if (!short.TryParse(item.ToString(), out result))
      return defaultShort;

    return result;
  }
  public static byte AsByte(this object item, byte defaultByte = default(byte))
  {
    if (item == null)
      return defaultByte;

    byte result;
    if (!byte.TryParse(item.ToString(), out result))
      return defaultByte;

    return result;
  }
  // transform object into string data type

  public static string AsString(this object item, string? defaultString = null)
  {
    if (item == null || item.Equals(System.DBNull.Value))
      return defaultString!;

    return item.ToString()!.Trim();
  }

  // transform object into DateTime data type.

  public static DateTime AsDateTime(this object item, DateTime defaultDateTime = default(DateTime))
  {
    if (item == null || string.IsNullOrEmpty(item.ToString()))
      return defaultDateTime;

    DateTime result;
    if (!DateTime.TryParse(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", item), out result))
      return defaultDateTime;

    return result;
  }
  public static DateTime AsDateTime(this string item, string fomat, DateTime defaultDateTime = default(DateTime))
  {
    if (item == null || string.IsNullOrEmpty(item))
      return defaultDateTime;
    try
    {
      var result = DateTime.ParseExact(item, fomat, CultureInfo.InvariantCulture);
      return result;
    }
    catch (System.Exception)
    {
      return defaultDateTime;
    }

  }

  // transform object into bool data type

  public static bool AsBool(this object item, bool defaultBool = default(bool))
  {
    if (item == null)
      return defaultBool;

    return new List<string>() { "yes", "y", "true" }.Contains(item.ToString()!.ToLower());
  }

  // transform string into byte array

  public static byte[]? AsByteArray(this string s)
  {
    if (string.IsNullOrEmpty(s))
      return null;

    return Convert.FromBase64String(s);
  }

  // transform object into base64 string.

  public static string? AsBase64String(this object item)
  {
    if (item == null)
      return null;

    return Convert.ToBase64String((byte[])item);
  }

  // transform object into Guid data type

  public static Guid AsGuid(this object item)
  {
    try { return new Guid(item.ToString()!); }
    catch { return Guid.Empty; }
  }



  public static float AsFloat(this object item, float defaultDecimal = default(float))
  {
    if (item == null)
      return defaultDecimal;

    float result;
    if (!float.TryParse(item.ToString(), out result))
      return defaultDecimal;

    return result;
  }

  public static bool IsNumber(string source)
  {
    if (!string.IsNullOrEmpty(source))
    {
      if (source.IndexOf("-") == 0) source.Remove(0, 1);

      char[] cs = source.ToCharArray();
      foreach (char c in cs)
      {
        if (!char.IsDigit(c)) return false;
      }
      return true;
    }
    return false;
  }
  public static int ToInteger(this object source)
  {
    if (IsNumber(source.ToString()!))
    {
      try
      {
        return int.Parse(source.ToString()!);
      }
      catch { }
    }
    return 0;
  }
}
