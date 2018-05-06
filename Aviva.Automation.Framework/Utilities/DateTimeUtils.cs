using System;

namespace Aviva.Automation.Framework
{
    public class DateTimeUtils
    {
        /// <summary>
        /// Utility to get current, past and future dates in mm/dd/yyyy format.
        /// </summary>
        /// <param name="numberOfDays">Number of days to be added or reduced (nevative value should be passed to get past date).</param>
        /// <returns>Required date in mm/dd/yyyy format.</returns>
        public static string GetDate(int numberOfDays = 0)
        {
            try
            {
                return DateTime.Now.AddDays(numberOfDays).ToString("MM/dd/yyyy");
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return null;
        }
    }
}
