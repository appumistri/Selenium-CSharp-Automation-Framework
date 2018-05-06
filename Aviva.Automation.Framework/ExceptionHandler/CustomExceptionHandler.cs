using NUnit.Framework;
using System;

namespace Aviva.Automation.Framework
{
    public class CustomExceptionHandler
    {
        /// <summary>
        /// Customized method to throw exception.
        /// </summary>
        /// <param name="e"> Exception object</param>
        /// <param name="errorMessage"> Custom error message</param>
        /// <param name="terminateTest"> Set to fase if user want only to log the exception and do not want the test to be terminated. By default set to true.</param>
        public static void CustomException(Exception e, string errorMessage = "", bool terminateTest = true)
        {
            string exceptionMsg = e.GetType().Name.ToLower();
            string customMsg = ExceptionHandler.ResourceManager.GetString(exceptionMsg);
            string stackTrace = string.Empty;
            customMsg = string.IsNullOrEmpty(customMsg) ? e.GetType().ToString() : customMsg;

            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = "::: " + e.Message;
                if (string.IsNullOrEmpty(errorMessage))
                {
                    stackTrace = string.IsNullOrEmpty(e.StackTrace) ? "::: " + e.InnerException : "::: " + e.StackTrace;
                }
            }
            else
            {
                errorMessage = "::: " + e.Message + " - " + errorMessage;
            }

            if (terminateTest)
            {
                Assert.Fail(customMsg + errorMessage + stackTrace);
            }
        }

        /// <summary>
        /// This method will return the customized exception message as a string, stored in ExceptionHandler.resx
        /// This is currently used by BDD layer, (AfterStep annotation) to retrieve the custom exception message upon failure and append it in Extent Reports
        /// </summary>
        /// <param name="exception">Pass the Exception object for which custom message is required</param>
        /// <returns>Custom exception message fetched from ExceptionHandler.resx</returns>
        public static string GetCustomExceptionMessage(Exception exception)
        {
            string exceptionMsg = exception.GetType().Name.ToLower();
            string customMsg = ExceptionHandler.ResourceManager.GetString(exceptionMsg);
            if (customMsg == null || customMsg.Equals(""))
            {
                return exception.Message;
            }
            else
            {
                return customMsg;
            }
        }
    }
}
