using System;
using System.Linq;

namespace Demo.Automation.Framework
{
    public class NumberUtils
    {
        /// <summary>
        /// Utility to get a randon Number of specified length.
        /// </summary>
        /// <param name="length">Length of the randon number to be generated.</param>
        /// <returns>Randon Number of specified length.</returns>
        public static double GetRandomNumber(int length)
        {
            try
            {
                Random random = new Random();
                const string chars = "0123456789";
                string randonNumberString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
                return double.Parse(randonNumberString);
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return 0;
        }

        public static double GetRandomNumber(int minNumber, int maxNumber)
        {
            try
            {
                Random rand = new Random();
                double randonNumber = rand.Next(minNumber, maxNumber);
                return randonNumber;
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return 0;
        }

        public static long GetRandomNumber(long min, long max)
        {
            try
            {
                Random rand = new Random();
                long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
                result = (result << 32);
                result = result | (long)rand.Next((Int32)min, (Int32)max);
                return result;
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return 0;
        }
    }
}
