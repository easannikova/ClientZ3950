
namespace ZClient.Abstract
{
    /// <summary>
    /// The ConnectionExtended class supports methods for instantiation and
    /// searching of targets, together with the housekeeping and option 
    /// management methods provided on all classes. It allows for extended 
    /// services
    /// </summary>
    /// <remarks>
    /// The interface IConnectionExtended can be used to search and scan
    /// exactly as IConnection interface implementations, but 
    /// extends it with a Package  Zoom.Net::IPackage factory call. 
    /// </remarks>
    /// <remarks>
    /// Search and Scan operations are supported.
    /// </remarks>
    /// <remarks>
    /// IConnectionExtended creation without specifying an actual 
    /// connection target is not supported.
    /// </remarks>
    /// <remarks>
    /// The exact range of Extended Services implemented on top of the
    /// ZOOM YAZ C API, including package types and package options, is
    /// described at http://www.indexdata.dk/yaz/doc/zoom.ext.tkl 
    /// </remarks>
    /// <remarks>
    /// See also Zoom.Net::IConnection interface 
    /// and Zoom.Net::YazSharp::ConnectionExtended implementation class.
    /// </remarks>
    /// <remarks>
    /// See also http://www.indexdata.dk/yaz/doc/zoom.ext.tkl
    /// </remarks>
    public interface IConnectionExtended : IConnection
    {
        /// <summary>
        /// The factory call constructs Packages, of type 'itemorder', 
        /// 'update', or of one of the extention types 'create', 
        /// 'drop', 'commit'. 
        /// These packages can be configures through setting appropriate      
        /// Zoom.Net::IPackageOptionsCollection Options.
        /// </summary>
        IPackage Package(string type);
    }
}