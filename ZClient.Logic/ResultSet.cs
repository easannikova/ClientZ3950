using System;
using System.Collections;
using ZClient.Abstract;

namespace ZClient.Logic
{
    public class ResultSet : IResultSet
    {
        internal ResultSet(IntPtr resultSet, Connection connection)
        {
            _resultSet = resultSet;
            _size = Yaz.ZOOM_resultset_size(_resultSet);

            if (0 == _size)
                Console.Out.WriteLine("Yaz.ZOOM_resultset_size zero");

            _records = new Record[_size];
        }

        ~ResultSet()
        {
            ((IDisposable) this).Dispose();
        }

        IResultSetOptionsCollection IResultSet.Options => new ResultSetOptionsCollection(_resultSet);


        IRecord IResultSet.this[uint index]
        {
            get
            {
                if (_records[index] == null)
                {
                    var record = Yaz.ZOOM_resultset_record(_resultSet, index);
                    _records[index] = new Record(record, this);
                }
                return _records[index];
            }
        }

        IRecord IResultSet.this[int index]
        {
            get
            {
                var uindex = (uint) index;
                return ((IResultSet) this)[uindex];
            }
        }

        uint IResultSet.Size => _size;

        private IntPtr _resultSet;
        private readonly uint _size;
        private readonly Record[] _records;

        private bool _disposed = false;

        void IDisposable.Dispose()
        {
            if (!_disposed)
            {
                foreach (Record record in _records)
                    record?.Dispose();

                Yaz.ZOOM_resultset_destroy(_resultSet);
                _resultSet = IntPtr.Zero;
                _disposed = true;
            }
        }

        #region IList Members

        public bool IsReadOnly => true;

        object IList.this[int index]
        {
            get { return ((IResultSet) this)[(uint) index]; }
            set { throw new NotImplementedException("Underlying ResultSet is readonly"); }
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException("Underlying ResultSet is readonly");
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException("Underlying ResultSet is readonly");
        }

        public void Remove(object value)
        {
            throw new NotImplementedException("Underlying ResultSet is readonly");
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException("Underlying ResultSet is not searchable");
        }

        public void Clear()
        {
            throw new NotImplementedException("Underlying ResultSet is readonly");
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException("Underlying ResultSet is not searchable");
        }

        public int Add(object value)
        {
            throw new NotImplementedException("Underlying ResultSet is readonly");
        }

        public bool IsFixedSize => true;

        #endregion

        #region ICollection Members

        public bool IsSynchronized => false;

        public int Count => (int) ((IResultSet) this).Size;

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException("Underlying ResultSet is not copyable");
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException("Underlying ResultSet is not synchronised"); }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return new ResultSetEnumerator(this);
        }

        #endregion
    }
}