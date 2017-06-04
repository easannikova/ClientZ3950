using System.Configuration;

namespace ZClient.Abstract
{
    public class QueryFactory
    {
        public static IPrefixQuery CreatePrefix(string query)
        {
            var typeName = Config.Get("IPrefixQueryClass");
            if (typeName == string.Empty)
            {
                var message
                    = "\n\nZoom.Net was unable to create a PrefixQuery "
                      + "class that implements\n"
                      + "Zoom.Net.IPrefixQuery as it couldn't find the name "
                      + "of the class you want to\n"
                      + "use. This is usually because the Zoom.Net.Factory "
                      + "section is missing\n"
                      + "from the application configuration.\n\n";
                throw new ConfigurationErrorsException(message);
            }
            var queryObject
                = (IPrefixQuery)
                GenericFactory.Create(typeName, new object[] {query});
            return queryObject;
        }

        public static IPrefixQuery CreatePrefix(string format,
            params object[] args)
        {
            var query = string.Format(format, args);
            return CreatePrefix(query);
        }

        public static ICQLQuery CreateCQL(string query)
        {
            string typeName = Config.Get("ICQLQueryClass");
            if (typeName == string.Empty)
            {
                string message
                    = "\n\nZoom.Net was unable to create a CQLQuery "
                      + "class that implements\n"
                      + "Zoom.Net.ICQLQuery as it couldn't find the name "
                      + "of the class you want to\n"
                      + "use. This is usually because the Zoom.Net.Factory "
                      + "section is missing\n"
                      + "from the application configuration.\n\n";
                throw new ConfigurationErrorsException(message);
            }
            var queryObject
                = (ICQLQuery)
                GenericFactory.Create(typeName, new object[] {query});
            return queryObject;
        }

        public static ICQLQuery CreateCQL(string format, params object[] args)
        {
            var query = string.Format(format, args);
            return CreateCQL(query);
        }
    }
}