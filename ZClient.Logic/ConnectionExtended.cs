using ZClient.Abstract;

namespace ZClient.Logic
{
    public class ConnectionExtended : Connection, IConnectionExtended
    {
        public ConnectionExtended(string host, int port) : base(host, port)
        {
        }

        public IPackage Package(string type)
        {
            EnsureConnected();

            var options = Yaz.ZOOM_options_create();
            var yazPackage = Yaz.ZOOM_connection_package(ZoomConnection, options);
            var pack = new Package(yazPackage, this, type);

            return pack;
        }
    }
}