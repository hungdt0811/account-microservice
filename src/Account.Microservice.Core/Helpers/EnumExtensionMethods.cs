using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Account.Microservice.Core.Helpers;
public static class EnumExtensionMethods
{
  public static IEnumerable<T> GetValues<T>()
  {
    return Enum.GetValues(typeof(T)).Cast<T>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public static List<KeyValuePair<string, int>> GetEnumList<T>()
  {
    var list = new List<KeyValuePair<string, int>>();
    foreach (var e in Enum.GetValues(typeof(T)))
    {
      list.Add(new KeyValuePair<string, int>(((Enum)e).GetDescription()!, (int)e));
    }
    return list;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public static List<KeyValuePair<string, string>> GetEnumList2<T>()
  {
    var list = new List<KeyValuePair<string, string>>();
    foreach (var e in Enum.GetValues(typeof(T)))
    {
      list.Add(new KeyValuePair<string, string>(((Enum)e).GetDescription()!, ((Enum)e).ToString()));
    }
    return list;
  }


  public static Dictionary<string, int> GetDictionaryEnum<TEnum>()
  {
    return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToDictionary(x => x!.ToString()!, x => x!.To<int>());
  }

  /// <summary>
  /// Gets an attribute on an enum field value
  /// </summary>
  /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
  /// <param name="enumVal">The enum value</param>
  /// <returns>The attribute of type T that exists on the enum value</returns>
  /// <example>string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;</example>
  public static T? GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
  {
    var type = enumVal.GetType();
    var memInfo = type.GetMember(enumVal.ToString());
    var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
    return attributes.Length > 0 ? (T)attributes[0] : null;
  }

  public static string GetEnumDescription(Enum value)
  {
    var fi = value.GetType().GetField(value.ToString());

    var attributes =
        (DescriptionAttribute[])fi!.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false);

    return attributes.Length > 0 ? attributes[0].Description : value.ToString();
  }

  public static string GetEnumDescription<TEnum>(int value)
  {
    return GetEnumDescription((Enum)(object)(TEnum)(object)value);
  }

  public static List<string>? GetEnumDescriptions<T>() where T : struct
  {
    Type t = typeof(T);
    return !t.IsEnum ? null : Enum.GetValues(t).Cast<Enum>().Select(x => x.GetDescription()).ToList();
  }


  public static string GetDisplayName(this Enum value)
  {
    try
    {
      var type = value.GetType();
      if (!type.IsEnum) throw new ArgumentException(string.Format("Type '{0}' is not Enum", type));

      var members = type.GetMember(value.ToString());
      if (members.Length == 0) throw new ArgumentException(string.Format("Member '{0}' not found in type '{1}'", value, type.Name));

      var member = members[0];
      var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
      if (attributes.Length == 0) throw new ArgumentException(string.Format("'{0}.{1}' doesn't have DisplayAttribute", type.Name, value));

      var attribute = (DisplayAttribute)attributes[0];

      var temp = attribute.GetName();

      if (string.IsNullOrEmpty(temp)) { temp = value.ToString(); }

      return temp;
    }
    catch (System.Exception)
    {

      return string.Empty;
    }

  }

  public static string GetDescription(this Enum value)
  {
    try
    {
      var fi = value.GetType().GetField(value.ToString());

      DescriptionAttribute[] attributes =
          (DescriptionAttribute[])fi!.GetCustomAttributes(
              typeof(DescriptionAttribute),
              false);

      if (attributes.Length > 0)
        return attributes[0].Description;
      else
        return value.ToString();
    }
    catch (System.Exception)
    {
      return string.Empty;
    }

  }

}
