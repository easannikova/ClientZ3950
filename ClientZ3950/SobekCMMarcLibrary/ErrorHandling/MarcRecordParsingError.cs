#region Using directives

using System;
using static System.String;

#endregion

namespace USMarcLibrary.ErrorHandling
{
    /// <summary> Enumeration for the basic error types which need to be attached
    /// to a MARC record during parsing (mostly from a MARC21 exchange file)  </summary>
    public enum MarcRecordParsingErrorTypeEnum : byte
    {
        /// <summary> Unknown type of error occurred </summary>
        Unknown = 0,

        /// <summary> A directory and field length mismatch was discovered and could not be handled </summary>
        DirectoryFieldMismatchUnhandled,

        /// <summary> The end of stream was unexpectedly encountered while parsing a record  </summary>
        UnexpectedEndOfStreamEncountered,

        /// <summary> The directory for a MARC21 record appears invalid </summary>
        InvalidDirectoryEncountered
    }

    /// <summary> Class stores basic error or error information which may 
    /// occur during processing </summary>
    public class MarcRecordParsingError : IEquatable<MarcRecordParsingError>
    {
        /// <summary> Any additional information about an error </summary>
        /// <remarks> This is different then the generic text for the Error; this is 
        /// ADDITIONAL information which may be saved for Error analysis </remarks>
        public readonly string ErrorDetails;

        /// <summary> Type of this error </summary>
        public readonly MarcRecordParsingErrorTypeEnum ErrorType;

        /// <summary> Constructor for a new instance of the MARC_Record_Parsing_Error class </summary>
        /// <param name="errorType"> Type of this error </param>
        /// <param name="errorDetails"> Any additional information about an error </param>
        public MarcRecordParsingError(MarcRecordParsingErrorTypeEnum errorType, string errorDetails)
        {
            this.ErrorType = errorType;
            this.ErrorDetails = errorDetails;
        }

        /// <summary> Constructor for a new instance of the MARC_Record_Parsing_Error class </summary>
        /// <param name="errorType"> Type of this error </param>
        public MarcRecordParsingError(MarcRecordParsingErrorTypeEnum errorType)
        {
            this.ErrorType = errorType;
            ErrorDetails = Empty;
        }

        #region IEquatable<MARC_Record_Parsing_Error> Members

        /// <summary> Tests to see if this Error type is identical to another
        /// Error type </summary>
        /// <param name="other"> Other Error to check for type match </param>
        /// <returns> TRUE if the two Errors are the same type, otherwise FALSE </returns>
        public bool Equals(MarcRecordParsingError other)
        {
            return other != null && ErrorType == other.ErrorType;
        }

        #endregion
    }
}