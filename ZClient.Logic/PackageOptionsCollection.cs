using System;
using ZClient.Abstract;

namespace ZClient.Logic
{
    public class PackageOptionsCollection : IPackageOptionsCollection, IDisposable
    {
        internal PackageOptionsCollection(IntPtr pack)
        {
            Package = pack;
        }

        public void Dispose()
        {
            Package = IntPtr.Zero;
        }

        public string this[string key]
        {
            get { return Yaz.ZOOM_package_option_get(Package, key); }
            set { Yaz.ZOOM_package_option_set(Package, key, value); }
        }

        internal IntPtr Package;
    }
}