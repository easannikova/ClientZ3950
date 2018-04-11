namespace ZClient.Abstract
{
    /// <summary>
    /// Connection Options can be attached to an Connection before sending 
    /// queries to the server. 
    /// </summary>
    /// <remarks>
    /// The Zoom::Net interfaces the following standard
    /// options: user, password, databaseName, which can be set directly
    /// on an object conforming to Zoom::Net::IConnection with the 
    /// User, Password and DatabaseName properties.
    /// </remarks>
    /// <remarks>
    /// All other standard options described at 
    /// http://zoom.z3950.org/api/zoom-1.4.html#3.8 are
    /// implemented using the Zoom::Net::IConnectionOptions
    /// </remarks>
    /// <remarks>
    /// See possible options and option values at 
    /// http://www.indexdata.dk/yaz/doc/zoom.tkl#zoom.connections
    /// </remarks>
    /// <remarks>
    /// See also
    /// Zoom.Net::IResultSetOptionsCollection interface, 
    /// where similar options can be set after searching.
    /// </remarks>
    public interface IConnectionOptionsCollection
    {
        /// <summary>
        /// Getting and setting option values
        /// </summary>
        /// <remarks>
        /// See possible options and option values at 
        ///  http://www.indexdata.dk/yaz/doc/zoom.tkl#zoom.connections
        /// </remarks>
        string this[string key] { get; set; }
    }
}
