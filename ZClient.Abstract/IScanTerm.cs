namespace ZClient.Abstract
{
    /// <summary>
    /// Scan Term interface.
    /// </summary>
    /// <remarks>
    /// In Zoom.Net the return values of the Term
    /// operation are ScanTerm objects rather than strings, which is a
    /// logical extension of the similar functionality in Record sets
    /// and Record objects. This is not forseen in the Zoom specifications.
    /// </remarks>
    /// <remarks>      
    /// Field 'freq' is implemented in the Occurrences operation on
    ///  IScanTerm.
    /// </remarks>
    /// <remarks>
    ///  Fields 'display', 'attrs', 'alt' and 'other' are not
    ///  supported.
    /// </remarks>
    public interface IScanTerm
    {
        /// <summary>
        /// Getting the actual term indexed
        /// </summary>
        string Term { get; }

        /// <summary>
        /// Getting the hit occurencies of this term
        /// </summary>
        int Occurences { get; }
    }
}