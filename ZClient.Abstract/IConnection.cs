using System;

namespace ZClient.Abstract
{
    /// <summary>
    /// The Connection class supports methods for instantiation and
    /// searching of targets, together with the housekeeping and option 
    /// management methods provided on all classes.
    /// </summary>
    /// <remarks>
    /// Connection
    /// or an ``application association'', as the OSI terminology in the
    /// standard has it - represents an ongoing dialogue between the
    /// client (``origin'' in the standard) and server (``target''). A
    /// connection is forged by the act of creating a Connection object;
    /// and initialization is performed implicitly, so that the new object
    /// is ready to be used immediately. 
    /// </remarks>
    /// <remarks>
    /// Search and Scan operations are supported.
    /// </remarks>
    /// <remarks>
    /// IConnection creation without specifying an actual connection target
    /// is not supported.
    /// </remarks>
    /// <remarks>See also Zoom.Net::IConnectionExtended interface 
    /// and Zoom.Net::YazSharp::Connection implementation class</remarks>
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Submitting a Query to a Connection. The resultset is held 
        /// on the server.
        /// </summary>
        /// <param name="query">
        /// The query is either a PQF or a CQL query.
        /// </param>
        IResultSet Search(IQuery query);

        /// <summary>
        /// Submitting a Scan to a Connection. The scanset is held 
        /// on the server.
        /// </summary>
        /// <param name="query">
        /// The scan query is a subset of PQF, namely the 
        /// Attributes+Term part.
        /// </param>
        IScanSet Scan(IPrefixQuery query);

        /// <summary>
        /// Setting and getting the databaseName option
        /// </summary>
        string DatabaseName { get; set; }

        /// <summary>
        /// Setting and getting the username option
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Setting and getting the password option
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Setting and getting recordSyntax option
        /// </summary>
        RecordSyntax Syntax { get; set; }

        /// <summary>
        /// Other standard options described at 
        /// http://zoom.z3950.org/api/zoom-1.4.html#3.8 are
        /// implemented using the Zoom::Net::IConnectionOptions
        /// interface.
        /// </summary>
        /// <remarks>
        /// See the following info for all possible values:   
        /// http://www.indexdata.dk/yaz/doc/zoom.tkl#zoom.connections
        /// </remarks>
        IConnectionOptionsCollection Options { get; }
    }
}