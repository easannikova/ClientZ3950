using System.Collections.Specialized;
using System.Configuration;

namespace ZClient.Abstract
{
    internal class Config
    {
        public static string Get(string key)
        {
            var settings = (NameValueCollection) ConfigurationManager.GetSection("Zoom.Net.Factory");

            if (settings == null)
            {
                var message = "\n\nZoom.Net was unable to load the "
                              + "'Zoom.Net.Factory' settings\n"
                              + "This is usually because the Zoom.Net.Factory section "
                              + "is missing \n"
                              + "from the application configuration.";

                throw new ConfigurationErrorsException(message);
            }
            var setting = settings[key];
            if (string.IsNullOrEmpty(setting))
            {
                var message
                    = "\n\nZoom.Net was unable to resolve the "
                      + "'Zoom.Net.Factory' setting\n'"
                      + key + "'\n"
                      + "This is usually because the Zoom.Net.Factory section "
                      + "of the application\n"
                      + "configuration is missing this key.";

                throw new ConfigurationErrorsException(message);
            }


            return setting;
        }
    }
}