using System.Collections;
using ZClient.Abstract;

namespace ZClient.Logic
{
    public class ResultSetEnumerator : IEnumerator
    {
        internal ResultSetEnumerator(ResultSet resultSet)
        {
            _resultSet = resultSet;
        }

        private readonly ResultSet _resultSet;

        #region IEnumerator Members

        public void Reset()
        {
            _position = 0;
        }

        public object Current => ((IResultSet) _resultSet)[_position];

        public bool MoveNext()
        {
            _position++;

            if (_position < ((IResultSet) _resultSet).Size)
            {
                return true;
            }
            else
            {
                _position--;
                return false;
            }
        }

        private uint _position = uint.MaxValue;

        #endregion
    }
}