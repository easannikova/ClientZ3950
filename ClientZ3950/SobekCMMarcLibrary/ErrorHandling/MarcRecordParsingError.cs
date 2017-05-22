#region License and Copyright

//          SobekCM MARC Library ( Version 1.2 )
//          
//          Copyright (2005-2012) Mark Sullivan. ( Mark.V.Sullivan@gmail.com )
//          
//          This file is part of SobekCM MARC Library.
//          
//          SobekCM MARC Library is free software: you can redistribute it and/or modify
//          it under the terms of the GNU Lesser Public License as published by
//          the Free Software Foundation, either version 3 of the License, or
//          (at your option) any later version.
//            
//          SobekCM MARC Library is distributed in the hope that it will be useful,
//          but WITHOUT ANY WARRANTY; without even the implied warranty of
//          MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//          GNU Lesser Public License for more details.
//            
//          You should have received a copy of the GNU Lesser Public License
//          along with SobekCM MARC Library.  If not, see <http://www.gnu.org/licenses/>.


#endregion

#region Using directives

using System;
using static System.String;

#endregion

namespace SobekCMMarcLibrary.ErrorHandling
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