using System;
using ZClient.Abstract;

namespace ZClient.Logic
{
    public class Package : IPackage
    {
        internal Package(IntPtr pack, ConnectionExtended connection, string type)
        {
            _type = type;
            _connection = connection;
            _package = pack;
        }

        IPackageOptionsCollection IPackage.Options => new PackageOptionsCollection(_package);

        void IPackage.Send()
        {
            Yaz.ZOOM_package_send(_package, _type);
        }

        private readonly string _type;
        private ConnectionExtended _connection;
        private IntPtr _package;

        void IDisposable.Dispose()
        {
            if (IntPtr.Zero != _package)
            {
                Yaz.ZOOM_package_destroy(_package);
                _connection = null;
                _package = IntPtr.Zero;
            }
        }
    }
}