using System;
using System.Runtime.InteropServices;

namespace ZClient.Logic
{
    internal class Yaz
    {
        private const string YazLibrary = "yaz5";

        #region connections

        /// <summary>
        /// create connection, connect to host, if portnum is 0,
        /// then port is read from host string (e.g. myhost:9821) 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="portnum"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_connection_new(string host, int portnum);

        /// <summary>
        /// create connection, don't connect, apply options
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_connection_create(IntPtr options);

        /// <summary>
        /// connect given existing connection
        /// </summary>
        /// <param name="c"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_connection_connect(IntPtr c, string host, int port);

        /// <summary>
        /// destroy connection (close connection also)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_connection_destroy(IntPtr c);

        /// <summary>
        /// get option for connection
        /// </summary>
        /// <param name="c"></param>
        /// <param name="key"></param>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_connection_option_get")]
        private static extern IntPtr ZOOM_connection_option_get_IntPtr(IntPtr c, string key);

        public static string ZOOM_connection_option_get(IntPtr c, string key)
        {
            return Marshal.PtrToStringAnsi(ZOOM_connection_option_get_IntPtr(c, key));
        }

        /// <summary>
        /// set option for connection
        /// </summary>
        /// <param name="c"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_connection_option_set(IntPtr c, string key, string value);

        /// <summary>
        /// set option for connection
        /// </summary>
        /// <param name="c"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_connection_option_setl(IntPtr c, string key, string value, int length);

        /// <summary>
        /// returns error code
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ZOOM_connection_errcode(IntPtr c);

        /// <summary>
        /// returns error message
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_connection_errmsg")]
        private static extern IntPtr ZOOM_connection_errmsg_IntPtr(IntPtr c);

        public static string ZOOM_connection_errmsg(IntPtr c)
        {
            return Marshal.PtrToStringAnsi(ZOOM_connection_errmsg_IntPtr(c));
        }

        /// <summary>
        /// returns additional info
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_connection_addinfo")]
        private static extern IntPtr ZOOM_connection_addinfo_IntPtr(IntPtr c);

        public static string ZOOM_connection_addinfo(IntPtr c)
        {
            return Marshal.PtrToStringAnsi(ZOOM_connection_addinfo_IntPtr(c));
        }

        /// <summary>
        /// translates error code into human-readable string
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_diag_str")]
        private static extern IntPtr ZOOM_diag_str_IntPtr(int error);

        public static string ZOOM_diag_str(int error)
        {
            return Marshal.PtrToStringAnsi(ZOOM_diag_str_IntPtr(error));
        }

        public const int ZoomErrorNone = 0;
        public const int ZoomErrorConnect = 10000;
        public const int ZoomErrorMemory = 10001;
        public const int ZoomErrorEncode = 10002;
        public const int ZoomErrorDecode = 10003;
        public const int ZoomErrorConnectionLost = 10004;
        public const int ZoomErrorInit = 10005;
        public const int ZoomErrorInternal = 10006;
        public const int ZoomErrorTimeout = 10007;
        public const int ZoomErrorUnsupportedProtocol = 10008;
        public const int ZoomErrorUnsupportedQuery = 10009;
        public const int ZoomErrorInvalidQuery = 10010;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ZOOM_connection_last_event(IntPtr c);

        public const int ZoomEventNone = 0;
        public const int ZoomEventConnect = 1;
        public const int ZoomEventSendData = 2;
        public const int ZoomEventRecvData = 3;
        public const int ZoomEventTimeout = 4;
        public const int ZoomEventUnknown = 5;
        public const int ZoomEventSendApdu = 6;
        public const int ZoomEventRecvApdu = 7;
        public const int ZoomEventRecvRecord = 8;
        public const int ZoomEventRecvSearch = 9;

        #endregion

        #region result sets

        /// <summary>
        /// create result set given a search
        /// </summary>
        /// <param name="c"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_connection_search(IntPtr c, IntPtr q);

        /// <summary>
        /// create result set given PQF query
        /// </summary>
        /// <param name="c"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_connection_search_pqf(IntPtr c, string q);

        /// <summary>
        /// destroy result set
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_resultset_destroy(IntPtr r);

        /// <summary>
        /// get result set option
        /// </summary>
        /// <param name="r"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_resultset_option_get")]
        private static extern IntPtr ZOOM_resultset_option_get_IntPtr(IntPtr r, string key);

        public static string ZOOM_resultset_option_get(IntPtr r, string key)
        {
            return Marshal.PtrToStringAnsi(ZOOM_resultset_option_get_IntPtr(r, key));
        }

        /// <summary>
        /// set result set option
        /// </summary>
        /// <param name="r"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_resultset_option_set")]
        private static extern IntPtr ZOOM_resultset_option_set_IntPtr(IntPtr r, string key, string value);

        public static string ZOOM_resultset_option_set(IntPtr r, string key, string value)
        {
            return Marshal.PtrToStringAnsi(ZOOM_resultset_option_set_IntPtr(r, key, value));
        }

        /// <summary>
        /// return size of result set (alias hit count AKA result count)
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern uint ZOOM_resultset_size(IntPtr r);

        /// <summary>
        /// retrieve records
        /// </summary>
        /// <param name="r"></param>
        /// <param name="recs"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_resultset_records(IntPtr r, IntPtr recs, uint start, uint count);

        /// <summary>
        /// return record object at pos. Returns 0 if unavailable
        /// </summary>
        /// <param name="r"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_resultset_record(IntPtr r, uint position);

        /// <summary>
        /// like ZOOM_resultset_record - but never blocks
        /// </summary>
        /// <param name="r"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_resultset_record_immediate(IntPtr r, uint position);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_resultset_cache_reset(IntPtr r);

        #endregion

        #region records

        /// <summary>
        /// get record information, in a form given by type
        /// </summary>
        /// <param name="record"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_record_get_string")]
        private static extern IntPtr ZOOM_record_get_string_IntPtr(IntPtr record, string type, ref int length);


        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_record_get")]
        private static extern IntPtr ZOOM_record_get_IntPtr(IntPtr record, string type, ref int length);

        /// <summary>
        /// get record information, in a form given by type
        /// </summary>
        /// <param name="record"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static unsafe byte[] ZOOM_record_get_bytes(IntPtr record, string type, ref int length)
        {
            IntPtr ipRecord = ZOOM_record_get_IntPtr(record, type, ref length);
            byte* pRecord = (byte*) ipRecord.ToPointer();
            byte[] recordBytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                recordBytes[i] = *pRecord;
                pRecord++;
            }

            return recordBytes;
        }

        public static string ZOOM_record_get_string(IntPtr record, string type, ref int length)
        {
            return Marshal.PtrToStringAnsi(ZOOM_record_get_IntPtr(record, type, ref length));
        }

        /// <summary>
        /// destroy record
        /// </summary>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_record_destroy_IntPtr(IntPtr record);

        /// <summary>
        /// return copy of record
        /// </summary>
        /// <param name="sourceRecord"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_record_clone(IntPtr sourceRecord);

        #endregion

        #region queries

        /// <summary>
        /// create search object
        /// </summary>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_query_create();

        /// <summary>
        /// destroy it
        /// </summary>
        /// <param name="s"></param>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_query_destroy(IntPtr s);

        /// <summary>
        /// CQL
        /// </summary>
        /// <param name="s"></param>
        /// <param name="query"></param>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ZOOM_query_cql(IntPtr s, string query);

        /// <summary>
        /// PQF
        /// </summary>
        /// <param name="s"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ZOOM_query_prefix(IntPtr s, string query);

        /// <summary>
        /// specify sort criteria for search
        /// </summary>
        /// <param name="s"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ZOOM_query_sortby(IntPtr s, string criteria);

        #endregion

        #region scan

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="startterm"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_connection_scan(IntPtr connection, string startterm);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_scanset_term")]
        private static extern IntPtr ZOOM_scanset_term_IntPtr(IntPtr scan, uint position, out int occ, out int length);

        public static string ZOOM_scanset_term(IntPtr scan, uint position, out int occ, out int length)
        {
            return Marshal.PtrToStringAnsi(ZOOM_scanset_term_IntPtr(scan, position, out occ, out length));
        }

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_scanset_display_term")]
        private static extern IntPtr ZOOM_scanset_display_term_IntPtr(IntPtr scan, uint position, int occ, int length);

        public static string ZOOM_scanset_display_term(IntPtr scan, uint position, int occ, int length)
        {
            return Marshal.PtrToStringAnsi(ZOOM_scanset_display_term_IntPtr(scan, position, occ, length));
        }

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern uint ZOOM_scanset_size(IntPtr scan);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern uint ZOOM_scanset_destroy(IntPtr scan);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern uint ZOOM_scanset_option_get(IntPtr scan, string key);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern uint ZOOM_scanset_option_set(IntPtr scan, string key, string value);

        #endregion

        #region Extended Services Packages

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_connection_package(IntPtr connection, IntPtr options);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_package_destroy(IntPtr package);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_package_send(IntPtr package, string type);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_package_option_get")]
        private static extern IntPtr ZOOM_package_option_get_IntPtr(IntPtr package, string key);

        public static string ZOOM_package_option_get(IntPtr package, string key)
        {
            return Marshal.PtrToStringAnsi(ZOOM_package_option_get_IntPtr(package, key));
        }

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_package_option_set(IntPtr package, string key, string value);

        #endregion

        #region sort

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_resultset_sort(IntPtr resultSet, string sortType, string sortSpec);

        #endregion

        #region options

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_options_create();

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_options_create_with_parent(IntPtr parentOptions);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr ZOOM_options_create_with_parent2(IntPtr parentOptions1, IntPtr parentOptions2);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi, EntryPoint = "ZOOM_options_get")]
        private static extern IntPtr ZOOM_options_get_IntPtr(IntPtr options, string key);

        public static string ZOOM_options_get(IntPtr options, string key)
        {
            return Marshal.PtrToStringAnsi(ZOOM_options_get_IntPtr(options, key));
        }

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_options_set(IntPtr options, string key, string value);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_options_setl(IntPtr options, string key, string value, int length);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_options_destroy(IntPtr options);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ZOOM_options_get_bool(IntPtr options, string name, int defa);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ZOOM_options_get_int(IntPtr options, string name, int defa);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void ZOOM_options_set_int(IntPtr options, string name, int defa);

        #endregion

        #region events

        /// <summary>
        /// poll for events on a number of connections. Returns positive
        /// integer if event occurred ; zero if none occurred and no more
        /// events are pending. The positive integer specifies the
        /// connection for which the event occurred.
        /// </summary>
        /// <param name="no"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ZOOM_event(int no, IntPtr connection);

        #endregion

        #region Yaz Functions

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void yaz_log_init(int level, string prefix, string name);

        [DllImport(YazLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern void yaz_log(LogLevel level, string message);

        public enum LogLevel
        {
            Fatal = 0x00000001,
            Debug = 0x00000002,
            Warn = 0x00000004,
            Log = 0x00000008
        }

        #endregion
    }
}