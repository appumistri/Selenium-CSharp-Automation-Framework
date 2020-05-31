
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Demo.Automation.Framework
{
    public class SeleniumVerify
    {
        /// <summary>
        /// To assert axact page title.
        /// </summary>
        /// <param name="ExpectedPageTitle"> Exact page title.</param>
        public void ExactPageTitle(string ExpectedPageTitle)
        {
            IWebDriver Driver = Application.driver;
            string ActualPageTitle = Driver.Title;
            Assert.True(ActualPageTitle.Equals(ExpectedPageTitle), "Mismatch in page title. Actual page title : " + ActualPageTitle + ", Expected page title : " + ExpectedPageTitle);
        }

        /// <summary>
        /// To assert partial page title.
        /// </summary>
        /// <param name="PartialPageTitle"> Partial page title.</param>
        public void PartialPageTitle(string PartialPageTitle)
        {
            IWebDriver Driver = Application.driver;
            string ActualPageTitle = Driver.Title;
            Assert.True(ActualPageTitle.Contains(PartialPageTitle), "Mismatch in page title. Actual page title : " + ActualPageTitle + ", Expected page title : " + PartialPageTitle);
        }

        /// <summary>
        /// Verifies message on browser alert.
        /// </summary>
        /// <param name="msg"> message that has to be verified.</param>
        public void VerifyBrowserAlertMsg(string msg, WaitType waitType)
        {
            try
            {
                SeleniumWaits seleniumWaits = new SeleniumWaits();
                SeleniumActions seleniumActions = new SeleniumActions();
                seleniumWaits.WaitForAlertToBePresent(waitType);
                string alertMsg = seleniumActions.GetAlertText();
                Assert.AreEqual(alertMsg, msg);
                seleniumActions.AcceptAlert();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Could not find browser level alert message because of an Exception: ");
            }
        }

        /// <summary>
        /// Asserts if a there is any empty string in string array.
        /// </summary>
        /// <param name="array"> string array</param>
        /// <returns></returns>
        public bool IsEmptyStringInArray(string[] array)
        {
            bool result = false;
            try
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (string.IsNullOrEmpty(array[i]))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return result;
        }

        /// <summary>
        /// Verifies wheather a PDF resource contain the specified text or not.
        /// </summary>
        /// <param name="textToVerify"> Text that has to verified.</param>
        /// <param name="pdfLink"> URL fo PDF resource (Optional). Takes current url if not specified. If you are passing this parameter then you need not pass {pdfFilePath}.</param>
        /// <param name="pdfFilePath">Absolute file path of pdf resource (Optional). If you are passing this parameter then you need not pass {pdfLink}.</param>
        /// <returns>True if text specified is present in the pdf document, false otherwise.</returns>
        public bool IsPdfContainsText(string textToVerify, string pdfLink = "", string pdfFilePath = "")
        {
            string filePath;
            try
            {
                SeleniumWaits seleniumWaits = new SeleniumWaits();
                seleniumWaits.WaitForPageLoad();

                if (string.IsNullOrEmpty(pdfFilePath))
                {
                    filePath = (new FileUtils()).DownloadFile(pdfLink);
                }
                else
                {
                    filePath = pdfFilePath;
                }

                PdfReader reader = new PdfReader(filePath);
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                reader.Close();
                Console.WriteLine(text);
                return text.ToString().Contains(textToVerify);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return false;
        }

        /// <summary>
        /// Reusable method to verify the file exist in the file path or not, if not wait for 1 minute(polling every 3 sec) and check the existence.
        /// </summary>
        /// <param name="AbsoluteFilePath"> Absolute filepath.</param>
        /// <returns>Returns true if file exists, false otherwise.</returns>
        public bool IsFileExists(string AbsoluteFilePath)
        {
            bool result = false;
            int i = 0;
            try
            {
                do
                {
                    result = File.Exists(AbsoluteFilePath);
                    if (!result)
                    {
                        Thread.Sleep(1000);
                    }
                    i++;
                } while (!result && i < 20);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return result;
        }

        /// <summary>
        /// Checks is an URl is accessible.
        /// </summary>
        /// <param name="url">URL that has to be checked for accessibility.</param>
        /// <returns><see cref="bool"/> Returns true if url is accessible, otherwise false.</returns>
        public bool IsURLAccessible(string url)
        {
            bool result = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //request.Timeout = 15000;
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            result = true;
                        }
                    }
                }
                catch (WebException)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return result;
        }
    }
}
