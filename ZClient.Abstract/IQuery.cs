
namespace ZClient.Abstract
{
    /// <summary>
    /// IQuery 
    /// represents a set of search criteria which will be submitted to a
    /// server via a connection. There are various subclasses/interfaces
    ///  of the Query
    /// class, representing different query syntaxes. 
    /// </summary>
    /// <remarks>
    /// CQL Query C# classes/interfaces are wrappers; the queries are sent 
    /// to the server unprocessed. See 
    /// http://www.indexdata.dk/yaz/doc/zoom.query.tkl
    /// </remarks>
    /// <remarks>
    ///  CQL client side query format is supported. 
    /// </remarks>
    /// <remarks>
    ///  CCL client side query format is not supported. 
    /// </remarks>
    /// <remarks>See also Zoom.Net::ICQLQuery 
    /// and Zoom.Net::IPrefixQuery</remarks>
    public interface IQuery
    {
        /// <summary>
        /// Setting and getting the query string
        /// </summary>
        string QueryString { get; set; }
    }
}