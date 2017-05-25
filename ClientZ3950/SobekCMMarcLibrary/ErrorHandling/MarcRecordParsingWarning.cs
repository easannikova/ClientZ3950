#region Using directives

using System;

#endregion

namespace USMarcLibrary.ErrorHandling
{
    /// <summary> Enumeration for the basic warning types which need to be attached
    /// to a MARC record during parsing (mostly from a MARC21 exchange file)  </summary>
    public enum MarcRecordParsingWarningTypeEnum : byte
    {
        /// <summary> Unknown type of warning (most likely not used, 
        /// as this would likely be an error instead ) </summary>
        Unknown = 0,

        /// <summary> A directory and field length mismatch was discovered, but it 
        /// appears that it was able to be handled </summary>
        DirectoryFieldMismatchHandled,

        /// <summary> Indicates that an alternate character set appears to have been present
        /// in a MARC8 character-encoded record </summary>
        AlternateCharacterSetPresent

    }

    /// <summary> Class stores basic warning or error information which may 
    /// occur during processing </summary>
    public class MarcRecordParsingWarning : IEquatable<MarcRecordParsingWarning>
    {
        /// <summary> Any additional information about a warning </summary>
        /// <remarks> This is different then the generic text for the warning; this is 
        /// ADDITIONAL information which may be saved for warning analysis </remarks>
        public readonly string WarningDetails;

        /// <summary> Type of this warning </summary>
        public readonly MarcRecordParsingWarningTypeEnum WarningType;

        /// <summary> Constructor for a new instance of the MARC_Record_Parsing_Warning class </summary>
        /// <param name="warningType"> Type of this warning </param>
        /// <param name="warningDetails"> Any additional information about a warning </param>
        public MarcRecordParsingWarning(MarcRecordParsingWarningTypeEnum warningType, string warningDetails)
        {
            WarningType = warningType;
            WarningDetails = warningDetails;
        }

        /// <summary> Constructor for a new instance of the MARC_Record_Parsing_Warning class </summary>
        /// <param name="warningType"> Type of this warning </param>
        public MarcRecordParsingWarning(MarcRecordParsingWarningTypeEnum warningType)
        {
            WarningType = warningType;
            WarningDetails = String.Empty;
        }

        #region IEquatable<MARC_Record_Parsing_Warning> Members

        /// <summary> Tests to see if this warning type is identical to another
        /// warning type </summary>
        /// <param name="other"> Other warning to check for type match </param>
        /// <returns> TRUE if the two warnings are the same type, otherwise FALSE </returns>
        public bool Equals(MarcRecordParsingWarning other)
        {
            return other != null && WarningType == other.WarningType;
        }

        #endregion
    }
}