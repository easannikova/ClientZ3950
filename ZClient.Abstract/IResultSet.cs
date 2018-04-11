using System;
using System.Collections;

namespace ZClient.Abstract
{
    /// <summary>
    /// The Result Set
    /// represents a set of records, held on a server, which have been
    /// identified by searching (that is, submitting a Query to a
    /// Connection). 
    /// </summary>
    /// <remarks>
    /// The Result Set class supports methods for discovering the number
    /// of its records, and fetching records either one by one or all at
    /// once. 
    /// </remarks>
    /// <remarks>
    /// Result Sets essentially are a collection of Records, which can be
    /// fetched individually by the '[]' operator.
    /// </remarks>
    /// <remarks>
    /// The size operation is supported. 
    /// </remarks>
    /// <remarks>
    /// See also
    /// Zoom::Net.IResultSetOptionsCollection interface, and
    /// Zoom.Net::YazSharp::ResultSet implementation class.
    /// </remarks>
    public interface IResultSet : IDisposable, IList
    {
        /// <summary>
        /// Getting the IResultSetOptionsCollection options
        /// </summary>
        IResultSetOptionsCollection Options { get; }

        /// <summary>
        /// Fetching a record
        /// </summary>
        IRecord this[uint index] { get; }

        /// <summary>
        /// Fetching a record
        /// </summary>
        new IRecord this[int index] { get; }

        /// <summary>
        /// Get size of Result Set in number of Records
        /// </summary>
        uint Size { get; }
    }
}