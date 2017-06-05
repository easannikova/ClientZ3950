using System;
using ZClient.Abstract;

namespace ZClient.Logic
{
    public class Connection : IConnection
    {
        private readonly string _host;
        private readonly int _port;
        private readonly ConnectionOptionsCollection _options;
        protected IntPtr ZConnection;
        private bool _disposed;
        private bool _connected;

        public IConnectionOptionsCollection Options => _options;


        protected Connection()
        {
        }

        public Connection(string host, int port)
        {
            _host = host;
            _port = port;

            _options = new ConnectionOptionsCollection();
            ZConnection = Yaz.ZOOM_connection_create(_options.ZoomOptions);

            var errorCode = Yaz.ZOOM_connection_errcode(ZConnection);
            CheckErrorCodeAndThrow(errorCode);
        }

        private void CheckErrorCodeAndThrow(int errorCode)
        {
            string message;
            switch (errorCode)
            {
                case Yaz.ZoomErrorNone:
                    break;

                case Yaz.ZoomErrorConnect:
                    message = $"Connection could not be made to {_host}:{_port}";
                    throw new ConnectionUnavailableException(message);

                case Yaz.ZoomErrorInvalidQuery:
                    message = "The query requested is not valid or not supported";
                    throw new InvalidQueryException(message);

                case Yaz.ZoomErrorInit:
                    message = $"Server {_host}:{_port} rejected our init request";
                    throw new InitRejectedException(message);

                case Yaz.ZoomErrorTimeout:
                    message = $"Server {_host}:{_port} timed out handling our request";
                    throw new ConnectionTimeoutException(message);

                case Yaz.ZoomErrorMemory:
                case Yaz.ZoomErrorEncode:
                case Yaz.ZoomErrorDecode:
                case Yaz.ZoomErrorConnectionLost:
                case Yaz.ZoomErrorInternal:
                case Yaz.ZoomErrorUnsupportedProtocol:
                case Yaz.ZoomErrorUnsupportedQuery:
                    message = Yaz.ZOOM_connection_errmsg(ZConnection);
                    throw new ZoomImplementationException("A fatal error occurred in Yaz: " + errorCode + " - " +
                                                          message);

                default:
                    var code = (Bib1Diagnostic) errorCode;
                    throw new Bib1Exception(code, Enum.GetName(typeof(Bib1Diagnostic), code));
            }
        }

        public IResultSet Search(IQuery query)
        {
            EnsureConnected();
            var yazQuery = Yaz.ZOOM_query_create();
            ResultSet resultSet;

            try
            {
                // branching out to right YAZ-C call
                if (query is ICQLQuery)
                    Yaz.ZOOM_query_cql(yazQuery, query.QueryString);
                else if (query is IPrefixQuery)
                    Yaz.ZOOM_query_prefix(yazQuery, query.QueryString);
                else
                    throw new NotImplementedException();

                var yazResultSet = Yaz.ZOOM_connection_search(ZConnection, yazQuery);
                // error checking C-style
                var errorCode = Yaz.ZOOM_connection_errcode(ZConnection);

                if (errorCode != Yaz.ZoomErrorNone)
                    Yaz.ZOOM_resultset_destroy(yazResultSet);

                CheckErrorCodeAndThrow(errorCode);

                // everything ok, create result set
                resultSet = new ResultSet(yazResultSet, this);
            }
            finally
            {
                // deallocate yazQuery also when exceptions
                Yaz.ZOOM_query_destroy(yazQuery);
            }
            return resultSet;
        }

        public IScanSet Scan(IPrefixQuery query)
        {
            EnsureConnected();
            var yazScanSet = Yaz.ZOOM_connection_scan(ZConnection, query.QueryString);

            var errorCode = Yaz.ZOOM_connection_errcode(ZConnection);
            if (errorCode != Yaz.ZoomErrorNone)
            {
                Yaz.ZOOM_scanset_destroy(yazScanSet);
            }
            CheckErrorCodeAndThrow(errorCode);

            var scanSet = new ScanSet(yazScanSet, this);
            return scanSet;
        }


        protected void EnsureConnected()
        {
            if (!_connected)
                Connect();
        }

        public void Connect()
        {
            Yaz.ZOOM_connection_connect(ZConnection, _host, _port);
            var errorCode = Yaz.ZOOM_connection_errcode(ZConnection);
            CheckErrorCodeAndThrow(errorCode);
            _connected = true;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Yaz.ZOOM_connection_destroy(ZConnection);

                //Yaz.yaz_log(Yaz.LogLevel.LOG, "Connection Disposed");
                ZConnection = IntPtr.Zero;
                _disposed = true;
            }
        }

        public RecordSyntax Syntax
        {
            get
            {
                var syntax = (RecordSyntax) Enum.Parse(typeof(RecordSyntax), Options["preferredRecordSyntax"]);
                return syntax;
            }
            set { Options["preferredRecordSyntax"] = Enum.GetName(typeof(RecordSyntax), value); }
        }

        public string DatabaseName
        {
            get { return Options["databaseName"]; }
            set { Options["databaseName"] = value; }
        }

        public string Username
        {
            get { return Options["user"]; }
            set { Options["user"] = value; }
        }

        public string Password
        {
            get { return Options["password"]; }
            set { Options["password"] = value; }
        }
    }
}