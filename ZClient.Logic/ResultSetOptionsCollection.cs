using System;
using ZClient.Abstract;

namespace ZClient.Logic
{
    public class ResultSetOptionsCollection : IResultSetOptionsCollection, IDisposable
    {
        internal ResultSetOptionsCollection(IntPtr resultSet)
        {
            ResultSet = resultSet;
        }

        public void Dispose()
        {
            ResultSet = IntPtr.Zero;
        }

        public string this[string key]
        {
            get { return Yaz.ZOOM_resultset_option_get(ResultSet, key); }
            set { Yaz.ZOOM_resultset_option_set(ResultSet, key, value); }
        }

        internal IntPtr ResultSet;
    }
}