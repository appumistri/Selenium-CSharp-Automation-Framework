using NUnit.Framework;
using System;

namespace Aviva.Automation.Framework
{
    /// <summary>
    /// This class defines custom methods that inverts the Assertion status, which can be used in the test scripts.
    /// </summary>
    public static class AssertExtensions
    {
        /// <summary>
        /// Inverts the Assert status based on the given boolean input.
        /// </summary>
        /// <param name="flag"><see cref="bool"/> True or false.</param>
        /// <param name="message"> Message to be included in the assert. This is an optional parameter.</param>
        public static void InvertAssert(bool flag, string message = "")
        {
            try
            {
                if (message.Trim().Length == 0)
                {
                    message = "Test is explicitly inverted using AssertInvert from test script.";
                }
                if (flag)
                {
                    Assert.Fail(message);

                }
                else
                {
                    Assert.Pass(message);
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Inverts the Assert status based on the given expected and actual inputs.
        /// </summary>
        /// <param name="expected"> Expected value.</param>
        /// <param name="actual"> Actual value.</param>
        /// <param name="message"> Message to be included in the assert. This is an optional parameter.</param>
        public static void InvertAssert(object expected, object actual, string message = "")
        {
            try
            {
                if (message.Trim().Length == 0)
                {
                    message = "Test is explicitly inverted using AssertInvert from test script.";
                }
                if (expected.Equals(actual))
                {
                    Assert.Fail(message);
                }
                else
                {
                    Assert.Pass(message);
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Inverts the Assert status if <paramref name="superText"/> contains (or) does not contains <paramref name="subText"/>.
        /// </summary>
        /// <param name="superText"> Text where <paramref name="subText"/> shoul be looked up.</param>
        /// <param name="subText"> Text which is to be looked up.</param>
        /// <param name="message"> Message to be included in the assert. This is an optional parameter.</param>
        public static void InvertAssert(string superText, string subText, string message = "")
        {
            try
            {
                if (message.Trim().Length == 0)
                {
                    message = "Test is explicitly inverted using AssertInvert from test script.";
                }
                if (superText.Contains(subText))
                {
                    Assert.Fail(message);
                }
                else
                {
                    Assert.Pass(message);
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Passes a test script without performing any checks.
        /// </summary>
        /// <param name="message"> Message to be included in the assert. This is an optional parameter.</param>
        public static void AssertPass(string message = "")
        {
            try
            {
                if (message.Trim().Length == 0)
                {
                    message = "Test status is marked success using AssertInvert from test script.";
                }
                Assert.Pass(message);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }
    }
}
