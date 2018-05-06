using System;
using System.Linq;

namespace Aviva.Automation.Framework
{
    public class StringUtils
    {
        /// <summary>
        /// Utility to get random string of specified length.
        /// </summary>
        /// <param name="length"> Length of the random string to be generated.</param>
        /// <returns>Random string of specified length.</returns>
        public static string GetRandomString(int length)
        {
            try
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return null;
        }

        public static string GetRandomAlphaNumericString(int length)
        {
            try
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return null;
        }

        public static string GetRandomAlphaNumericString()
        {
            try
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, 8)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return null;
        }
    }
}
