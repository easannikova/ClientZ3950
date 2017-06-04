using System.Configuration;

namespace ZClient.Abstract
{
    public class ConnectionFactory
    {
        public static IConnection Create(string host, int port)
        {
            var connectionTypeName
                = Config.Get("IConnectionClass");
            if (connectionTypeName == string.Empty)
            {
                var message
                    = "\n\nZoom.Net was unable to create a Connection class "
                      + "that implements\n"
                      + "Zoom.Net.IConnection as it couldn't find the name of "
                      + "the class you want to\n"
                      + "use. This is usually because the Zoom.Net.Factory "
                      + "section is missing\n"
                      + "from the application configuration.\n\n";

                throw new ConfigurationErrorsException(message);
            }
            var connection = (IConnection) GenericFactory.Create(connectionTypeName,
                new object[] {host, port});
            return connection;
        }

        public static IConnectionExtended CreateExtended(string host, int port)
        {
            string connectionTypeName
                = Config.Get("IConnectionExtendedClass");
            if (connectionTypeName == string.Empty)
            {
                var message
                    = "\n\nZoom.Net was unable to create a ConnectionExtended "
                      + "class that implements\n"
                      + "Zoom.Net.IConnectionExtended as it couldn't find the "
                      + "name of the class you want to\n"
                      + "use. This is usually because the Zoom.Net.Factory "
                      + "section is missing\n"
                      + "from the application configuration.\n\n";

                throw new ConfigurationErrorsException(message);
            }
            var connection = (IConnectionExtended)
                GenericFactory.Create(connectionTypeName,
                    new object[] {host, port});
            return connection;
        }
    }
}