namespace ZClient.Abstract
{
    /// <summary>
    /// Result Set Options can be attached to an Result Set 
    /// residing on the server.
    /// </summary>
    /// <remarks>
    /// See possible options and option values at 
    /// http://www.indexdata.dk/yaz/doc/zoom.resultsets.tkl
    /// </remarks>
    /// <remarks>
    /// See also
    /// Zoom.Net::IConnectionOptionsCollection interface, 
    /// where similar options can be set before searching.
    /// </remarks>
    public interface IResultSetOptionsCollection
    {
        /// <summary>
        /// Getting and setting option values
        /// </summary>
        /// <remarks>
        /// See possible options and option values at 
        /// http://www.indexdata.dk/yaz/doc/zoom.resultsets.tkl
        /// </remarks>
        string this[string key] { get; set; }
    }
}