using System;

namespace ZClient.Abstract
{
    /// <summary>
    /// The Scan Set 
    /// represents a set of potential query terms which a server suggests
    /// may be useful. In some cases, the terms are accompanied by hit
    /// counts.
    /// </summary>
    /// Term supported. In Zoom::Net the return values of the Term
    /// operation are ScanTerm objects rather than strings, which is a
    /// logical extension of the similar functionality in Record sets
    /// and Record objects. This is not forseen in the Zoom specifications.
    /// <remarks>
    /// </remarks>
    /// <remarks>
    /// Display strings for use in client are not supported.
    /// </remarks>
    /// <remarks> 
    /// See also the following Class/Interface definitions
    /// Zoom.Net::IScanTerm and Zoom.Net::YazSharp::ScanTerm 
    /// </remarks>
    /// <remarks>
    /// See also http://www.indexdata.dk/yaz/doc/zoom.scan.tkl
    /// </remarks>
    public interface IScanSet : IDisposable
    {
        /// <summary>
        /// Getting the n-th Scan Term
        /// </summary>
        IScanTerm this[uint index] { get; }

        /// <summary>
        /// Getting the size of the Scan Set in number of terms
        /// </summary>
        uint Size { get; }
    }
}