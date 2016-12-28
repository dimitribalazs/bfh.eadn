using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common
{
    public static class Common
    {
        /// <summary>
        /// Get an entry from the app.config or web.config
        /// </summary>
        /// <typeparam name="T">T is the expected return type. Must be value type or string</typeparam>
        /// <param name="itemName">Name of the key in config file</param>
        /// <returns>Value converted to type T</returns>
        public static T GetConfigValue<T>(string itemName)
        {
            string configValue = ConfigurationManager.AppSettings.Get(itemName);
            if(string.IsNullOrEmpty(configValue))
            {
                throw new KeyNotFoundException($"no key with name {itemName}");
            }
            return (T)Convert.ChangeType(configValue, typeof(T));
        }
    }
}
