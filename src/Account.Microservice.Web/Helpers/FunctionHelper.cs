using System.Text.RegularExpressions;

namespace Account.Microservice.Web.Helpers;

public static class FunctionHelper
{
  public static bool ValidatePassword(string pw)
  {
    var lowercase = new Regex("[a-z]+");
    var uppercase = new Regex("[A-Z]+");
    var digit = new Regex("(\\d)+");
    //var symbol = new Regex("(\\W)+");
    //return (lowercase.IsMatch(pw) && uppercase.IsMatch(pw) && digit.IsMatch(pw) && symbol.IsMatch(pw));
    return (lowercase.IsMatch(pw) && uppercase.IsMatch(pw) && digit.IsMatch(pw));
  }
}
