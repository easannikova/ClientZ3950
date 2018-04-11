using System;
using ZClient.Abstract;

namespace ZClient.Logic
{
    public class ConnectionOptionsCollection : IConnectionOptionsCollection, IDisposable
    {
        internal ConnectionOptionsCollection()
        {
            ZoomOptions = Yaz.ZOOM_options_create();
        }

        public void Dispose()
        {
            Yaz.ZOOM_options_destroy(ZoomOptions);
            ZoomOptions = IntPtr.Zero;
        }

        internal IntPtr CreateConnection()
        {
            return Yaz.ZOOM_connection_create(ZoomOptions);
        }

        public string this[string key]
        {
            get { return Yaz.ZOOM_options_get(ZoomOptions, key); }
            set { Yaz.ZOOM_options_set(ZoomOptions, key, value); }
        }

        internal IntPtr ZoomOptions;
    }
}