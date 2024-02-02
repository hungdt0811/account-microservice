using System.Reflection;

namespace Account.Microservice.Web.Helpers;

public static class ModelMappingExtensions
{
  public static T? Map<T>(this object fromSource) where T : class, new()
  {

    if (fromSource == null) return default;

    var ret = new T();
    var sourceProps = fromSource.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
    var destinationProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

    foreach (var desProp in destinationProps)
    {
      var sourceProp = sourceProps.FirstOrDefault(m => m.Name == desProp.Name);

      if (sourceProp != null)
      {
        try
        {
          desProp.SetValue(ret, sourceProp.GetValue(fromSource, null), null);
        }
        catch (System.Exception ex)
        {
          throw new System.Exception(desProp.Name + desProp.PropertyType, ex);
        }

      }
    }
    return ret;
  }
}
