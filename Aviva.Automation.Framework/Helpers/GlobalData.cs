using System;
using System.Collections.Generic;
using System.Threading;

namespace Aviva.Automation.Framework
{
    public class GlobalData
    {
        /// <summary>
        /// Dictionary to store all run time data in the form of key-value pair {string Key, string value}
        /// </summary>
        private static ThreadLocal<Dictionary<string, string>> _globalDataDictionary = new ThreadLocal<Dictionary<string, string>>();

        /// <summary>
        /// Method to store a key value pair into globaldata.
        /// </summary>
        /// <param name="Key"> Key</param>
        /// <param name="value"> value to be stored</param>
        public static void Set(string Key, string value)
        {
            try
            {
                GetDictionary().Add(Key, value);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Method to get stored value from globaldata.
        /// </summary>
        /// <param name="Key"> Key whose value is to be returned</param>
        ///<returns>Value of the key specified(if the key is stored in globaldata already, otherwise returns empty string)</returns>
        public static string Get(string Key)
        {
            try
            {
                if (GetDictionary().ContainsKey(Key))
                {
                    return GetDictionary()[Key];
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return string.Empty;
        }

        /// <summary>
        /// Method to determine if globaldata contains a perticular key.
        /// </summary>
        /// <param name="Key"> Key that has to be looked up</param>
        /// <returns>return true if the key is present within globaldata, false otherwise.</returns>
        public static bool ContainsKey(string Key)
        {
            try
            {
                return GetDictionary().ContainsKey(Key);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return false;
        }

        /// <summary>
        /// Method to get the current global data dictionary (Create a new one if one is not alreassiged to the current thread)
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetDictionary()
        {
            try
            {
                if (!_globalDataDictionary.IsValueCreated)
                {
                    _globalDataDictionary.Value = new Dictionary<string, string>();
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return _globalDataDictionary.Value;
        }
    }
}
