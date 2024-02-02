using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Helpers;
public static class PasswordHelper
{
  const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
  const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  const string NUMBERS = "123456789";
  const string SPECIALS = @"!@£$%^&*()#€";
  /// <summary>
  /// Sinh một chuỗi mã CodeConfirm ngẫu nhiên chi chức năng xác thực email kích hoạt tài khoản
  /// </summary>
  /// <param name="useLowercase"></param>
  /// <param name="useUppercase"></param>
  /// <param name="useNumbers"></param>
  /// <param name="useSpecial"></param>
  /// <param name="passwordSize"></param>
  /// <returns></returns>
  public static string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial,
      int passwordSize)
  {
    char[] _password = new char[passwordSize];
    string charSet = ""; // Initialise to blank
    Random _random = new Random();
    int counter;

    // Build up the character set to choose from
    if (useLowercase)
      charSet += LOWER_CASE;

    if (useUppercase)
      charSet += UPPER_CAES;

    if (useNumbers)
      charSet += NUMBERS;

    if (useSpecial)
      charSet += SPECIALS;

    for (counter = 0; counter < passwordSize; counter++)
    {
      _password[counter] = charSet[_random.Next(charSet.Length - 1)];
    }

    return String.Join(null, _password);
  }

  /// <summary>
  /// Tạo mật khẩu ngẫu nhiên
  /// </summary>
  /// <param name="length"></param>
  /// <returns></returns>
  public static string GenerateRandomPassword(int length)
  {
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    Random random = new Random();

    string randomString = new string(Enumerable.Repeat(chars, length)
      .Select(s => s[random.Next(s.Length)]).ToArray());

    return HashPassword(randomString);
  }

  /// <summary>
  /// Mã hoá MD5
  /// </summary>
  /// <param name="password"></param>
  /// <returns></returns>
  public static string HashPassword(string password)
  {
    using (MD5 md5 = MD5.Create())
    {
      // Chuyển đổi mật khẩu thành mảng byte
      byte[] inputBytes = Encoding.ASCII.GetBytes(password);

      // Mã hoá mật khẩu
      byte[] hashBytes = md5.ComputeHash(inputBytes);

      // Chuyển đổi mảng byte thành chuỗi hex
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < hashBytes.Length; i++)
      {
        sb.Append(hashBytes[i].ToString("X2"));
      }

      return sb.ToString();
    }
  }

  /// <summary>
  /// So sánh mật khẩu mã hoá MD5
  /// </summary>
  /// <param name="enteredPassword"></param>
  /// <param name="hashedPassword"></param>
  /// <returns></returns>
  public static bool VerifyPassword(string enteredPassword, string hashedPassword)
  {
    return HashPassword(enteredPassword) == hashedPassword;
  }
}
