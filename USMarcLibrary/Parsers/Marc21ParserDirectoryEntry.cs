
namespace USMarcLibrary.Parsers
{
    /// <summary> Structure stores the basic information about a directory entry when parsing
    ///  a MARC21 record </summary>
    /// <remarks> This is only used while actually parsing the MARC21 record </remarks>
    internal struct Marc21ParserDirectoryEntry
    {
        /// <summary> Three ASCII numeric or ASCII alphabetic characters (upper case or lower case, but not both) that identify an associated variable field. </summary>
        public readonly short Tag;

        /// <summary> Length of the variable field, including indicators, subfield codes, data, and the field terminator.  </summary>
        public readonly short FieldLength;

        /// <summary> Starting character position of the variable field relative to the Base address of data (Leader/12-16) of the record. </summary>
        public readonly short StartingPosition;

        /// <summary> Creates a new instance of the MARC21_Parser_Directory_Entry structure  </summary>
        /// <param name="tagValue"> Three ASCII numeric or ASCII alphabetic characters (upper case or lower case, but not both) that identify an associated variable field.</param>
        /// <param name="lengthValue"> Length of the variable field, including indicators, subfield codes, data, and the field terminator. </param>
        /// <param name="startingPositionValue"> Starting character position of the variable field relative to the Base address of data (Leader/12-16) of the record. </param>
        public Marc21ParserDirectoryEntry( short tagValue, short lengthValue, short startingPositionValue )
        {
            Tag = tagValue;
            FieldLength = lengthValue;
            StartingPosition = startingPositionValue;
        }
    }
}