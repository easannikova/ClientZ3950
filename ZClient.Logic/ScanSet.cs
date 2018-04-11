using System;
using ZClient.Abstract;

namespace ZClient.Logic
{
    public class ScanSet : IScanSet
    {
        internal ScanSet(IntPtr scanSet, Connection connection)
        {
            _connection = connection;
            _scanSet = scanSet;
        }

        public IScanTerm this[uint index]
        {
            get
            {
                int occ, length;
                var term = Yaz.ZOOM_scanset_term(_scanSet, index, out occ, out length);
                return new ScanTerm(term, occ);
            }
        }

        public uint Size => Yaz.ZOOM_scanset_size(_scanSet);

        private Connection _connection;
        private IntPtr _scanSet;

        private bool _disposed = false;

        public void Dispose()
        {
            if (!_disposed)
            {
                Yaz.ZOOM_scanset_destroy(_scanSet);
                _connection = null;
                _scanSet = IntPtr.Zero;
                _disposed = true;
            }
        }
    }
}