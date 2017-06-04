using System;
using ZClient.Abstract;

namespace ZClient.Logic
{
    public class Record : IRecord
    {
        internal Record(IntPtr record, ResultSet resultSet)
        {
            _record = record;
        }

        public byte[] Content
        {
            get
            {
                var syntax = "raw";
                var length = 99;
                var content = Yaz.ZOOM_record_get_bytes(_record, syntax, ref length);

                return content;
            }
        }

        public RecordSyntax Syntax
        {
            get
            {
                var length = 0;
                var syntax = Yaz.ZOOM_record_get_string(_record, "syntax", ref length);
                if (syntax == null)
                    return RecordSyntax.Unknown;

                return (RecordSyntax) Enum.Parse(typeof(RecordSyntax), syntax, true);
            }
        }

        public string Database
        {
            get
            {
                var length = 0;
                var database = Yaz.ZOOM_record_get_string(_record, "database", ref length);
                return database;
            }
        }

        private bool _disposed = false;

        public void Dispose()
        {
            if (!_disposed)
            {
                _record = IntPtr.Zero;
                _disposed = true;
            }
        }

        private IntPtr _record;
    }
}