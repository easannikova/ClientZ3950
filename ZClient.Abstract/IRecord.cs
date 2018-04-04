//Third comment added
using System;

namespace ZClient.Abstract
{
    /// <summary>
    /// IRecord represents a single record or document
    /// in a result set obtained by querying a server.
    /// </summary>
    /// <remarks>
    /// See also http://www.indexdata.dk/yaz/doc/zoom.records.tkl
    /// </remarks>
    public interface IRecord : IDisposable
    {
        /// <summary>
        /// Gets the literal byte content of this record
        /// </summary>
        byte[] Content { get; }

        /// <summary>
        /// Gets the Syntax description of this record
        /// </summary>
        RecordSyntax Syntax { get; }

        /// <summary>
        /// Gets the name of the database the record was returned from
        /// </summary>
        string Database { get; }

    }
}